using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Common;

public class HubspotApiException : Exception
{
    private readonly HttpResponseMessage? _response;
    public HubspotErrorModel? _hubspotError { get; }

    public HubspotApiException(string errorMessage, HubspotErrorModel? hubspotError, HttpResponseMessage response) : base(errorMessage)
    {
        this._hubspotError = hubspotError;
        this._response = response;
    }

    public HubspotApiException(string errorMessage) : base(errorMessage)
    {

    }

    public override string Message {
        get
        {
            if (_response == null) return base.Message;

            return base.Message + Environment.NewLine +
                    $"Url: {_response.RequestMessage?.RequestUri.AbsolutePath.ToString()}" + Environment.NewLine +
                    $"HttpCode: {(int)_response.StatusCode}" + Environment.NewLine +
                    $"Content: {_response.Content.ReadAsStringAsync().Result}" + Environment.NewLine;
        }
    }
}