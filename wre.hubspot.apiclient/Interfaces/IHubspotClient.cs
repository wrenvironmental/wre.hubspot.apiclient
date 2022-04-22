namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotClient
{
    HttpClient HttpClient();
    string EntityBaseUrlPrefix { get; }
}