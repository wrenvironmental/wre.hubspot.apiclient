using System.Net;
using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Common;

public class HubspotApiException : Exception
{
    private readonly HttpResponseMessage? _response;
    public readonly HttpStatusCode? HttpStatusCode;
    public readonly HubspotResponseError? HubspotError;

    public HubspotApiException(string errorMessage, HubspotResponseError? hubspotError, HttpResponseMessage response) : base(errorMessage)
    {
        HubspotError = hubspotError;
        _response = response;
        HttpStatusCode = response.StatusCode;
    }

    public HubspotApiException(string errorMessage) : base(errorMessage) { }

    public override string Message
    {
        get
        {
            if (_response == null) return base.Message;
            var requestModel = _response.RequestMessage?.Content?.ReadAsStringAsync().Result;
            return base.Message + Environment.NewLine +
                    $"Url: {_response.RequestMessage?.RequestUri}" + Environment.NewLine +
                    $"Request Model: {requestModel}" + Environment.NewLine +
                    $"HttpCode: {(int)_response.StatusCode}" + Environment.NewLine +
                    $"Content: {_response.Content.ReadAsStringAsync().Result}" + Environment.NewLine;
        }
    }
}