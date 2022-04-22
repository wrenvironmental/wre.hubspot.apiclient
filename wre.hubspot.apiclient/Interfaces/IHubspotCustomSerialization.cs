namespace wre.hubspot.apiclient.Interfaces;

public interface IHubspotCustomSerialization
{
    public object GetCustomObject<T>(T entity);
}