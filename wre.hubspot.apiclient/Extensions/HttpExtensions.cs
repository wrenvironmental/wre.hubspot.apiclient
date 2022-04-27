using System.Text.Json;
using System.Text.Json.Serialization;
using wre.hubspot.apiclient.Common;
using wre.hubspot.apiclient.Infrastructure;
using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Extensions;

public static class HttpExtensions
{
    public static async Task PostAsync(this HttpClient httpClient, string url, string data)
    {
        var response = await httpClient.PostAsync(url, new JsonContent(data));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            var settings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var errorModel = JsonSerializer.Deserialize<HubspotErrorModel>(content, settings);
            throw new HubspotApiException($"Error processing request. Url={response.RequestMessage?.RequestUri}", errorModel);
        }
    }

    public static async Task<TReturn> PostAsync<TReturn>(this HttpClient httpClient, string url, string data) where TReturn : class
    {
        var response = await httpClient.PostAsync(url, new JsonContent(data));
        var settings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            
            var errorModel = JsonSerializer.Deserialize<HubspotErrorModel>(content, settings);
            throw new HubspotApiException($"Error processing request. Url={response.RequestMessage?.RequestUri}", errorModel);
        }

        return string.IsNullOrEmpty(content) ? default! : JsonSerializer.Deserialize<TReturn>(content, settings) ?? throw new ArgumentException("");
    }

    public static async Task PatchAsync(this HttpClient httpClient, string url, string data)
    {
        var response = await httpClient.PatchAsync(url, new JsonContent(data));
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var errorModel = JsonSerializer.Deserialize<HubspotErrorModel>(content, settings);
            throw new HubspotApiException($"Error processing request. Url={response.RequestMessage?.RequestUri}", errorModel);
        }
    }

    public static async Task<TReturn> PatchAsync<TReturn>(this HttpClient httpClient, string url, string data) where TReturn : class
    {
        var response = await httpClient.PatchAsync(url, new JsonContent(data));
        var settings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {

            var errorModel = JsonSerializer.Deserialize<HubspotErrorModel>(content, settings);
            throw new HubspotApiException($"Error processing request. Url={response.RequestMessage?.RequestUri}", errorModel);
        }

        return string.IsNullOrEmpty(content) ? default! : JsonSerializer.Deserialize<TReturn>(content, settings) ?? throw new ArgumentException("");
    }

    public static async Task DeleteAsync(this HttpClient httpClient, string url, bool throwError = false)
    {
        var response = await httpClient.DeleteAsync(url);
        if (throwError && !response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            var errorModel = JsonSerializer.Deserialize<HubspotErrorModel>(content, settings);
            throw new HubspotApiException($"Error processing request. Url={response.RequestMessage?.RequestUri}", errorModel);
        }
    }
}