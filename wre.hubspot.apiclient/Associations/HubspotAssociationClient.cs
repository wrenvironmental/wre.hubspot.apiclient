using Flurl;
using wre.hubspot.apiclient.Extensions;
using wre.hubspot.apiclient.Interfaces;
using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Associations;

public class HubspotAssociationClient : IHubspotClient
{
    private readonly HttpClient _httpClient;
    public string EntityBaseUrl => "crm/v3/associations";

    public HubspotAssociationClient(string baseUrl, string apiKey)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    public HubspotAssociationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }    

    public Task<HubspotStandardResponseModel<HubspotStandardAssociationResponseModel>> CreateAsync(HubspotAssociationEntity entity)
    {
        var request = new HubspotStandardRequestListModel<HubspotAssociationEntity>(new List<HubspotAssociationEntity> { entity });
        var url = entity.GetFullUrl(EntityBaseUrl).AppendPathSegment("/batch/create");
        return _httpClient.PostAsync<HubspotStandardResponseModel<HubspotStandardAssociationResponseModel>>(url, request.SerializeToJson());
    }

    public Task<HubspotStandardResponseListModel<HubspotStandardAssociationResponseModel>> CreateBatchAsync(IEnumerable<HubspotAssociationEntity> entities)
    {
        var entitiesArray = entities.ToList();
        if (entitiesArray.Count == 0) throw new InvalidOperationException("At least one entity must be provided");
        var url = $"{entitiesArray.First().GetFullUrl(EntityBaseUrl)}".AppendPathSegment("/batch/create");
        var request = new HubspotStandardRequestListModel<HubspotAssociationEntity>(entitiesArray);
        return _httpClient.PostAsync<HubspotStandardResponseListModel<HubspotStandardAssociationResponseModel>>(url, request.SerializeToJson());
    }

    public async Task<HubspotAssociationStandardResponseListModel> ListAsync<TInput, TAssociationType>(TInput entity, IHubspotAssociation associationEntity) where TInput : IHubspotEntity
    {
        var url = $"{entity.GetFullUrl(EntityBaseUrl.Replace("/associations", string.Empty))}"
                    .AppendPathSegment($"{entity.Id}/associations/{associationEntity.EntityName}");
        return await _httpClient.GetAsync<HubspotAssociationStandardResponseListModel>(url);
    }
}