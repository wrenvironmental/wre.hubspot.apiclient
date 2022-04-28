using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;
using Flurl;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.apiclient.Interfaces;
using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Common;

public class HubspotClient<T> where T : class, IHubspotEntity
{
    private IHubspotClient? _client;

    public HubspotClient()
    {
        _client = null;
    }

    public void Init(IHubspotClient client)
    {
        _client = client;
    }

    public IHubspotClient? GetHubspotClient
    {
        get
        {
            if (_client == null)
                throw new InvalidOperationException("Client must be initiated");
            return _client;
        }
    }

    public virtual Task CreateAsync(T entity)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity);
        return _client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public virtual Task<HubspotStandardResponseModel<TReturn>> CreateAsync<TReturn>(T entity) where TReturn : class
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity);
        return _client.HttpClient().PostAsync<HubspotStandardResponseModel<TReturn>>(url, entity.SerializeToJson());
    }

    public virtual Task<HubspotStandardResponseListModel<TReturn>> CreateAsync<TReturn>(IEnumerable<T> entities) where TReturn : class
    {
        if (!entities.Any()) throw new InvalidOperationException("At least one entity must be provided");
        var url = $"{GetFullUrl(GetHubspotClient.EntityBaseUrl, entities.First())}".AppendPathSegment("/batch/create");
        return _client.HttpClient().PostAsync<HubspotStandardResponseListModel<TReturn>>(url, new HubspotStandardRequestListModel<T>(entities.ToList()).SerializeToJson());
    }

    public virtual Task UpdateAsync(T entity)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity, entity.Id ?? throw new ArgumentException(nameof(entity.Id)));
        return _client.HttpClient().PatchAsync(url, entity.SerializeToJson());
    }

    public virtual Task<HubspotStandardResponseModel<TReturn>> UpdateAsync<TReturn>(long id, T entity) where TReturn : class
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity, id);
        return _client.HttpClient().PatchAsync<HubspotStandardResponseModel<TReturn>>(url, entity.SerializeToJson());
    }

    public virtual Task DeleteAsync(T entity, bool throwException = false)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity, entity.Id ?? throw new ArgumentException(nameof(entity.Id)));
        return _client.HttpClient().DeleteAsync(url, throwException);
    }

    public virtual Task DeleteAsync(IEnumerable<T>? entities, bool throwException = false)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entities.First()).AppendPathSegment("/batch/archive");
        var deleteObjects = new HubspotStandardRequestListModel<long>(new List<long>(entities.Where(e => e.Id.HasValue).Select(e => e.Id.Value).AsEnumerable()));
        return _client.HttpClient().PostAsync(url, deleteObjects.SerializeToJson());
    }

    public virtual Task<HubspotStandardSearchReturnModel<TInput>> SearchAsync<TInput>(TInput entity, params Expression<Func<TInput, dynamic?>>[]? expressions) where TInput : IHubspotEntity
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity, true);
        var expCache = new ConcurrentDictionary<string, Delegate>();
        var dict = new Dictionary<string, dynamic>();

        foreach (var exp in expressions)
        {
            string? name = null;
            var body = exp.Body;

            if (body is UnaryExpression unaryExp)
                body = unaryExp.Operand;

            if (body is MemberExpression memberExp)
                name = memberExp.Member.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? memberExp.Member.Name;

            if (body is MethodCallExpression methodCallExp)
                name = methodCallExp.Method.Name;

            if (name == null)
                throw new InvalidExpressionException(
                    $"The expression '{exp.Body}' is invalid. You must supply an expression that references a property or a function of the type '{typeof(T)}'.");

            var key = typeof(T).FullName + "." + name;
            var func = (Func<TInput, dynamic>)expCache.GetOrAdd(key, _ => ((LambdaExpression)exp).Compile());
            dict[name] = func(entity);
        }

        return _client.HttpClient().PostAsync<HubspotStandardSearchReturnModel<TInput>>(url, GetSearchModel(dict).SerializeToJson());
    }

    private static HubspotStandardSearchModel GetSearchModel(Dictionary<string, dynamic> parameters)
    {
        return new HubspotStandardSearchModel(parameters);
    }

    protected static string GetFullUrl(string baseUrlPrefix, IHubspotEntity entity, bool isSearchUrl = false)
    {
        if (entity is IHubspotCustomEntity custom)
        {
            return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, custom.ObjectTypeId, isSearchUrl ? "search" : string.Empty)
                .SetQueryParam("hapikey", HubspotSettings.ApiToken);
        }
        return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, isSearchUrl ? "search" : string.Empty)
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, IHubspotEntity entity, long id)
    {
        if (entity is IHubspotCustomEntity custom)
        {
            return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, custom.ObjectTypeId, id.ToString())
                .SetQueryParam("hapikey", HubspotSettings.ApiToken);
        }
        return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, id.ToString())
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }
}