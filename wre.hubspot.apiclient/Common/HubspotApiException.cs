using wre.hubspot.apiclient.Models;

namespace wre.hubspot.apiclient.Common;

public class HubspotApiException : Exception
{
    public HubspotApiException(string errorMessage, HubspotErrorModel? hubspotError) : base(errorMessage)
    {
        HubspotError = hubspotError;
    }

    public HubspotErrorModel? HubspotError { get; }
}