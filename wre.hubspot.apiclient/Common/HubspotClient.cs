using System.Collections.Concurrent;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Flurl;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.apiclient.Infrastructure;
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

    public virtual Task<HubspotStandardResponseModel<TReturn>> CreateAsync<TReturn>(T entity) where TReturn : class
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity);
        return _client.HttpClient().PostAsync<HubspotStandardResponseModel<TReturn>>(url, entity.SerializeToJson());
    }

    public virtual Task<HubspotStandardResponseListModel<TReturn>> CreateBatchAsync<TReturn>(IEnumerable<T> entities) where TReturn : class
    {
        var entitiesArray = entities.ToList();
        if (entitiesArray.Count == 0) throw new InvalidOperationException("At least one entity must be provided");
        var url = $"{GetFullUrl(GetHubspotClient.EntityBaseUrl, entitiesArray.First())}".AppendPathSegment("/batch/create");
        return _client.HttpClient().PostAsync<HubspotStandardResponseListModel<TReturn>>(url, new HubspotStandardRequestListModel<T>(entitiesArray).SerializeToJson());
    }

    public virtual Task<HubspotStandardResponseModel<TReturn>> UpdateAsync<TReturn>(long id, T entity) where TReturn : class
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity, id);
        return _client.HttpClient().PatchAsync<HubspotStandardResponseModel<TReturn>>(url, entity.SerializeToJson());
    }

    public virtual Task<HubspotStandardResponseListModel<TReturn>> UpdateBatchAsync<TReturn>(IEnumerable<T> entities) where TReturn : class
    {
        var entitiesArray = new List<T>(entities);
        if (entitiesArray.Count == 0) throw new InvalidOperationException("At least one entity must be provided");
        var url = $"{GetFullUrl(GetHubspotClient.EntityBaseUrl, entitiesArray.First())}".AppendPathSegment("/batch/update");
        return _client.HttpClient().PostAsync<HubspotStandardResponseListModel<TReturn>>(url, new HubspotStandardRequestListModel<T>(entitiesArray).SerializeToJson());
    }

    public virtual Task DeleteAsync(T entity, bool throwException = false)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity, entity.Id ?? throw new ArgumentException(nameof(entity.Id)));
        return _client.HttpClient().DeleteAsync(url, throwException);
    }

    public virtual Task DeleteBatchAsync(IEnumerable<T> entities, bool throwException = false)
    {
        var entitiesArray = entities.ToList();
        if (entitiesArray.Count == 0) throw new InvalidOperationException("At least one entity must be provided");
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entitiesArray.First()).AppendPathSegment("/batch/archive");
        var deleteObjects = new HubspotStandardRequestListModel<long>(new List<long>(entitiesArray.Where(e => e.Id.HasValue).Select(e => e.Id ?? 0).AsEnumerable()));
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
            return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, custom.ObjectTypeId, isSearchUrl ? "search" : string.Empty);
        }
        return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, isSearchUrl ? "search" : string.Empty);
    }

    private static string GetFullUrl(string baseUrlPrefix, IHubspotEntity entity, long id)
    {
        if (entity is IHubspotCustomEntity custom)
        {
            return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, custom.ObjectTypeId, id.ToString());
        }
        return Url.Combine(baseUrlPrefix, entity.EntityUrlSuffix, id.ToString());
    }
}