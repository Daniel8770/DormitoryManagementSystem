using System.Runtime.InteropServices;

namespace DormitoryManagementSystem.API.DTOs.Responses;

public static class CommonLinkStrings 
{
    public const string GET = "GET";
    public const string POST = "POST";
    public const string PUT = "PUT";
    public const string DELETE = "DELETE";
    public const string SELF = "self";
}

public class Link(string rel, string method, string href)
{
    public string Rel { get; set; } = rel;
    public string Method { get; set; } = method;
    public string Href { get; set; } = href;
}