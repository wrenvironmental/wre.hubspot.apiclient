using Flurl;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class EntityExtensions
{
    public static Task CreateAsync<T>(this IHubspotClient client, T entity) where T : IHubspotEntity
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.CreateUrl);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public static Task<TReturn> CreateAsync<T, TReturn>(this IHubspotClient client, T entity) where T : IHubspotEntity where TReturn : class
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.CreateUrl);
        return client.HttpClient().PostAsync<TReturn>(url, entity.SerializeToJson());
    }

    public static Task UpdateAsync<T>(this IHubspotClient client, long id, T entity) where T : IHubspotEntity
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.UpdateUrl, id);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public static Task<TReturn> UpdateAsync<T, TReturn>(this IHubspotClient client, long id, T entity) where T : IHubspotEntity where TReturn : class
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.UpdateUrl, id);
        return client.HttpClient().PatchAsync<TReturn>(url, entity.SerializeToJson());
    }

    #region Custom Objects

    public static Task CreateAsync(this IHubspotClient client, IHubspotCustomEntity entity)
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.CreateUrl, entity.ObjectTypeId);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }
    public static Task<TReturn> CreateAsync<TReturn>(this IHubspotClient client, IHubspotCustomEntity entity) where TReturn : class
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.CreateUrl, entity.ObjectTypeId);
        return client.HttpClient().PostAsync<TReturn>(url, entity.SerializeToJson());
    }

    public static Task UpdateAsync(this IHubspotClient client, long id, IHubspotCustomEntity entity)
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.UpdateUrl, entity.ObjectTypeId, id);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public static Task<TReturn> UpdateAsync<TReturn>(this IHubspotClient client, long id, IHubspotCustomEntity entity) where TReturn : class
    {
        var url = GetFullUrl(client.EntityBaseUrlPrefix, entity.UpdateUrl, entity.ObjectTypeId, id);
        return client.HttpClient().PatchAsync<TReturn>(url, entity.SerializeToJson());
    }
    #endregion

    private static string GetFullUrl(string baseUrlPrefix, string absoluteUrl)
    {
        return Url.Combine(baseUrlPrefix, absoluteUrl)
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, string absoluteUrl, long id)
    {
        return Url.Combine(baseUrlPrefix, absoluteUrl, id.ToString())
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, string absoluteUrl, string objectTypeId)
    {
        return Url.Combine(baseUrlPrefix, absoluteUrl, objectTypeId)
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }

    private static string GetFullUrl(string baseUrlPrefix, string absoluteUrl, string objectTypeId, long id)
    {
        return Url.Combine(baseUrlPrefix, absoluteUrl, objectTypeId, id.ToString())
            .SetQueryParam("hapikey", HubspotSettings.ApiToken);
    }
}