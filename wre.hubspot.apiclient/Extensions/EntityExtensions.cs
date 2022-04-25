using Flurl;
using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.Extensions;

public static class EntityExtensions
{
    public static Task CreateAsync<T>(this IHubspotClient client, T entity) where T : IHubspotEntity
    {
        var url = GetFullUrl(client.EntityBaseUrl, entity.EntityUrlSuffix);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    #region Custom Objects

    public static Task CreateAsync(this IHubspotClient client, IHubspotCustomEntity entity)
    {
        var url = GetFullUrl(client.EntityBaseUrl, entity.EntityUrlSuffix, entity.ObjectTypeId);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }
    public static Task<TReturn> CreateAsync<TReturn>(this IHubspotClient client, IHubspotCustomEntity entity) where TReturn : class
    {
        var url = GetFullUrl(client.EntityBaseUrl, entity.EntityUrlSuffix, entity.ObjectTypeId);
        return client.HttpClient().PostAsync<TReturn>(url, entity.SerializeToJson());
    }

    public static Task UpdateAsync(this IHubspotClient client, long id, IHubspotCustomEntity entity)
    {
        var url = GetFullUrl(client.EntityBaseUrl, entity.EntityUrlSuffix, entity.ObjectTypeId, id);
        return client.HttpClient().PostAsync(url, entity.SerializeToJson());
    }

    public static Task<TReturn> UpdateAsync<TReturn>(this IHubspotClient client, long id, IHubspotCustomEntity entity) where TReturn : class
    {
        var url = GetFullUrl(client.EntityBaseUrl, entity.EntityUrlSuffix, entity.ObjectTypeId, id);
        return client.HttpClient().PatchAsync<TReturn>(url, entity.SerializeToJson());
    }
    #endregion

    private static string GetFullUrl(string baseUrlPrefix, string suffix)
    {
        return Url.Combine(baseUrlPrefix, suffix)
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