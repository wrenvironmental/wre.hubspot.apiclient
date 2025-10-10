namespace wre.hubspot.apiclient.Models;

public class HubspotResponseError
{
    public DateTime CompletedAt { get; set; }
    public string? Status { get; set; }
    public DateTime StartedAt { get; set; }
    public HubspotErrorModel[]? Errors { get; set; }
    public int NumErrors { get; set; }
}

public class HubspotErrorModel
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public Guid? CorrelationId { get; set; }
    public string? Category { get; set; }
    public HubspotErrorContext? Context { get; set; }
}

public class HubspotErrorContext
{
    public string[]? Properties { get; set; }
    public string[]? Ids { get; set; }
}