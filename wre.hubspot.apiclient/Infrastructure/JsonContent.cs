using System.Text;

namespace wre.hubspot.apiclient.Infrastructure;

public class JsonContent : StringContent
{
    public JsonContent(string json) : this(json, Encoding.UTF8)
    {
    }

    public JsonContent(string json, Encoding encoding) : base(json, encoding, "application/json")
    {
    }
}