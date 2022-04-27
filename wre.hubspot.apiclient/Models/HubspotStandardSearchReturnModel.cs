namespace wre.hubspot.apiclient.Models;

public class HubspotStandardSearchReturnModel<T>
{
    public int Total { get; set; }
    public List<HubspotStandardResponseModel<T>>? Results { get; set; }
    public HubspotStandardSearchReturnPaging? Paging { get; set; }
}


public class HubspotStandardSearchReturnPaging
{
    public HubspotStandardSearchReturnPagingNext? Next { get; set; }
}

public class HubspotStandardSearchReturnPagingNext
{
    public int After { get; set; }
}