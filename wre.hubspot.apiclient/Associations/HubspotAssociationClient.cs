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

    public HubspotAssociationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Init(this);
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }

    public string EntityBaseUrl => "crm/v3/associations";

    public override Task<HubspotStandardResponseModel<TReturn>> CreateAsync<TReturn>(T entity)
    {
        var request = new HubspotStandardRequestListModel<T>(new List<T> { entity });
        var url = GetFullUrl(GetHubspotClient.EntityBaseUrl, entity).AppendPathSegment("/batch/create");
        return _httpClient.PostAsync<HubspotStandardResponseModel<TReturn>>(url, request.SerializeToJson());
    }

    public override Task<HubspotStandardResponseListModel<TReturn>> CreateBatchAsync<TReturn>(IEnumerable<T> entities)
    {
        var entitiesArray = entities.ToList();
        if (entitiesArray.Count == 0) throw new InvalidOperationException("At least one entity must be provided");
        var url = $"{GetFullUrl(GetHubspotClient.EntityBaseUrl, entitiesArray.First())}".AppendPathSegment("/batch/create");
        var request = new HubspotStandardRequestListModel<T>(entitiesArray);
        return _httpClient.PostAsync<HubspotStandardResponseListModel<TReturn>>(url, request.SerializeToJson());
    }

    public override Task<HubspotStandardSearchReturnModel<TInput>> SearchAsync<TInput>(TInput entity, params Expression<Func<TInput, dynamic?>>[]? expressions)
    {
        throw new NotImplementedException("Associations don't have search implemented yet");
    }

    public override Task<HubspotStandardResponseModel<TReturn>> UpdateAsync<TReturn>(long id, T entity)
    {
        throw new NotImplementedException("Associations don't have update implemented yet");
    }

    public override Task<HubspotStandardResponseListModel<TReturn>> UpdateBatchAsync<TReturn>(IEnumerable<T> entities)
    {
        throw new NotImplementedException("Associations don't have batch update implemented yet");
    }

    public override Task DeleteAsync(T entity, bool throwException = false)
    {
        throw new NotImplementedException("Associations don't have delete implemented yet");
    }

    public override Task DeleteBatchAsync(IEnumerable<T> entities, bool throwException = false)
    {
        throw new NotImplementedException("Associations don't have delete batch implemented yet");
    }
}