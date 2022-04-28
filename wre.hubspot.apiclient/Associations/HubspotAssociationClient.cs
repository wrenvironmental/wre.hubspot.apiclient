using System.Linq.Expressions;
using Flurl;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.apiclient.Interfaces;
using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Associations;

public class HubspotAssociationClient<T> : HubspotClient<T>, IHubspotClient where T : class, IHubspotEntity
{
    private readonly HttpClient _httpClient;

    public HubspotAssociationClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        Init(this);
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }

    public string EntityBaseUrl => "crm/v3/associations";

    public override Task CreateAsync(T entity)
    {
        var request = new HubspotStandardRequestListModel<T>(new List<T>() { entity });
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity);
        return _httpClient.PostAsync(url, request.SerializeToJson());
    }

    public override Task<HubspotStandardResponseModel<TReturn>> CreateAsync<TReturn>(T entity)
    {
        throw new NotImplementedException("Single association with return is not implemented yet");
    }

    public override Task<HubspotStandardResponseListModel<TReturn>> CreateAsync<TReturn>(IEnumerable<T> entities)
    {
        if (!entities.Any()) throw new InvalidOperationException("At least one entity must be provided");
        var url = $"{GetFullUrl(GetHubspotClient.EntityBaseUrl, entities.First())}".AppendPathSegment("/batch/create");
        var request = new HubspotStandardRequestListModel<T>(entities.ToList());
        return _httpClient.PostAsync<HubspotStandardResponseListModel<TReturn>>(url, request.SerializeToJson());
    }

    public override Task<HubspotStandardSearchReturnModel<TInput>> SearchAsync<TInput>(TInput entity, params Expression<Func<TInput, dynamic?>>[]? expressions)
    {
        throw new NotImplementedException("Associations don't have search implemented yet");
    }

    public override Task UpdateAsync(T entity)
    {
        throw new NotImplementedException("Associations don't have update implemented yet");
    }

    public override Task<HubspotStandardResponseModel<TReturn>> UpdateAsync<TReturn>(long id, T entity)
    {
        throw new NotImplementedException("Associations don't have update implemented yet");
    }

    public override Task DeleteAsync(T entity, bool throwException = false)
    {
        throw new NotImplementedException("Associations don't have delete implemented yet");
    }

    public override Task DeleteAsync(IEnumerable<T>? entities, bool throwException = false)
    {
        throw new NotImplementedException("Associations don't have delete batch implemented yet");
    }
}