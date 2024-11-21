using DormitoryManagementSystem.Application.Clubs;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagementSystem.API.Controllers;

public class ClubsController : Controller
{
    private BookableResourceService bookableResourceService;

    public ClubsController(BookableResourceService bookableResourceService)
    {
        this.bookableResourceService = bookableResourceService;
    }

    [HttpGet]
    [Route("api/clubs/{clubId}/bookableResources/{bookableResourceId}")]
    public async Task<IActionResult> GetBookableResource(Guid clubId, Guid bookableResourceId)
    {
        BookableResource? found = await bookableResourceService
            .GetBookableResourceById(new BookableResourceId(bookableResourceId));

        return found is not null ? Ok(found) : NotFound();
    }

    [HttpPut]
    [Route("api/clubs/{clubId}/bookableResources/{bookableResourceId}/addUnit")]
    public async Task<IActionResult> AddUnitToBookableResource(Guid clubId, Guid bookableResourceId, string unitName)
    {
        BookableResource? updatedResource = await bookableResourceService
            .AddUnit(new BookableResourceId(bookableResourceId), unitName);

        if (updatedResource is null)
            return NotFound();

        return Ok(updatedResource);
    }

    [HttpPost]
    [Route("api/clubs/{clubId}/bookableResources")]
    public async Task<IActionResult> CreateNewBookableResource(CreateNewBookableResourceRequest request)
    {
        BookableResource newResource = await bookableResourceService.CreateNewBookableResource(
            request.Name,
            request.Rules,
            request.OpenDate,
            request.EndDate);

        return Ok(newResource);
    }

    [HttpPut]
    [Route("api/clubs/{clubId}/bookableResources/{bookableResourceId}/bookDays")]
    public async Task<IActionResult> BookDays(Guid clubId, Guid bookableResourceId, BookDaysRequest request)
    {
        BookableResource? updatedResource = await bookableResourceService.BookDays(
            new BookableResourceId(bookableResourceId),
            new (request.MemberId),
            new (request.UnitId),
            request.Date,
            request.Days);

        return updatedResource is not null ? Ok(updatedResource) : NotFound();
    }
    
}

public record BookDaysRequest
(
    Guid MemberId,
    int UnitId,
    DateTime Date,
    int Days
);

public record CreateNewBookableResourceRequest
(
    string Name,
    string Rules,
    DateTime OpenDate,
    DateTime EndDate
);
