namespace DormitoryManagementSystem.API.DTOs.Responses;

public abstract class Response
{
    public IEnumerable<Link> Links { get; set; } = new List<Link>();

    public Response AddLinks(IEnumerable<Link> links)
    {
        Links = links;
        return this;
    }

    public Response AddLink(Link link)
    {
        Links = Links.Append(link);
        return this;
    }

}
