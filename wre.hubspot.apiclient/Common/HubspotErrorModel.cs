namespace wre.hubspot.apiclient.Common;

public class HubspotErrorModel
{
    public string status { get; set; }
    public string Message { get; set; }
    public Guid? CorrelationId { get; set; }
    public string Category { get; set; }
    public HubspotErrorContext Context { get; set; }
}

public class HubspotErrorContext
{
    public string[] Properties { get; set; }
}