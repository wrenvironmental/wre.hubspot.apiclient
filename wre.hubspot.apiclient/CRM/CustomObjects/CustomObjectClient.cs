﻿using wre.hubspot.apiclient.Interfaces;

namespace wre.hubspot.apiclient.CRM.CustomObjects;

public class CustomObjectClient : IHubspotClient
{
    private readonly HttpClient _httpClient;

    public CustomObjectClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public HttpClient HttpClient()
    {
        return _httpClient;
    }

    public IHubspotClient Set<T>(IHubspotCustomEntity entity) where T : IHubspotCustomEntity
    {
        return this;
    }

    public string EntityBaseUrlPrefix => "crm/v3";
}