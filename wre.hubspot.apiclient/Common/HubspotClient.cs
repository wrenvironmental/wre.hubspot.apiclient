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

    public Task CreateAsync(T entity)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity.EntityUrlSuffix);
        return _client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public Task<HubspotStandardResponseModel<TReturn>> CreateAsync<TReturn>(T entity) where TReturn : class
    {
        var url = GetFullUrl(_client.EntityBaseUrl, entity.EntityUrlSuffix);
        return _client.HttpClient().PostAsync<HubspotStandardResponseModel<TReturn>>(url, entity.SerializeToJson());
    }

    public Task UpdateAsync(long id, T entity)
    {
        var url = GetFullUrl(_client.EntityBaseUrl, entity.EntityUrlSuffix, id);
        return _client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public Task<HubspotStandardResponseModel<TReturn>> UpdateAsync<TReturn>(long id, T entity) where TReturn : class
    {
        var url = GetFullUrl(_client.EntityBaseUrl, entity.EntityUrlSuffix, id);
        return _client.HttpClient().PatchAsync<HubspotStandardResponseModel<TReturn>>(url, entity.SerializeToJson());
    }

    public Task DeleteAsync(T entity, long id, bool throwException = false)
    {
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity.EntityUrlSuffix, id);
        return _client.HttpClient().DeleteAsync(url, throwException);
    }

    public Task<HubspotStandardSearchReturnModel<TInput>> SearchAsync<TInput>(TInput input, params Expression<Func<TInput, dynamic?>>[]? expressions) where TInput : IHubspotEntity
    {
        var url = GetFullUrl(_client.EntityBaseUrl, input.EntityUrlSuffix, "search");
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
                    string.Format("The expression '{0}' is invalid. You must supply an expression that references a property or a function of the type '{1}'.",
                        exp.Body, typeof(T)));

            var key = typeof(T).FullName + "." + name;
            var func = (Func<TInput, dynamic>)expCache.GetOrAdd(key, _ => ((LambdaExpression)exp).Compile());
            dict[name] = func(input);
        }

        return _client.HttpClient().PostAsync<HubspotStandardSearchReturnModel<TInput>>(url, GetSearchModel(dict).SerializeToJson());
    }

    private static HubspotStandardSearchModel GetSearchModel(Dictionary<string, dynamic> parameters)
    {
        return new HubspotStandardSearchModel(parameters);
    }

    private static string GetFullUrl(string baseUrlPrefix, string suffix)
    {
        return Url.Combine(baseUrlPrefix, suffix)
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, string suffix, long id)
    {
        return Url.Combine(baseUrlPrefix, suffix, id.ToString())
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, string suffix, string objectTypeId)
    {
        return Url.Combine(baseUrlPrefix, suffix, objectTypeId)
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, string suffix, string objectTypeId, long id)
    {
        return Url.Combine(baseUrlPrefix, suffix, objectTypeId, id.ToString())
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }
}