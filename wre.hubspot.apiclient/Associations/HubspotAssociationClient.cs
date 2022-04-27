using System.Linq.Expressions;
using wre.hubspot.apiclient.Common;
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

    public override Task<HubspotStandardSearchReturnModel<TInput>> SearchAsync<TInput>(TInput entity, params Expression<Func<TInput, dynamic?>>[]? expressions)
    {
        throw new NotImplementedException("Associations don't have search implemented yet");
    }

    public override Task UpdateAsync(long id, T entity)
    {
        throw new NotImplementedException("Associations don't have update implemented yet");
    }

    public override Task<HubspotStandardResponseModel<TReturn>> UpdateAsync<TReturn>(long id, T entity)
    {
        throw new NotImplementedException("Associations don't have update implemented yet");
    }

    public override Task DeleteAsync(T entity, long id, bool throwException = false)
    {
        throw new NotImplementedException("Associations don't have delete implemented yet");
    }
}