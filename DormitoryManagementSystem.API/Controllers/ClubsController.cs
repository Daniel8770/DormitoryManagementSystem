using DormitoryManagementSystem.API.DTOs.Requests.ClubsContext;
using DormitoryManagementSystem.API.DTOs.Responses.ClubsContext;
using DormitoryManagementSystem.Application.Clubs;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagementSystem.API.Controllers;

public class ClubsController : Controller
{
    private IBookableResourceService bookableResourceService;

    public ClubsController(IBookableResourceService bookableResourceService)
    {
        this.bookableResourceService = bookableResourceService;
    }

    [HttpGet]
    [Route("api/clubs/{clubId}/bookableResources/{bookableResourceId}")]
    public async Task<IActionResult> GetBookableResource(Guid clubId, Guid bookableResourceId)
    {
        BookableResource? found = await bookableResourceService
            .GetBookableResourceById(new BookableResourceId(bookableResourceId));

        return found is not null 
            ? Ok(found.ConvertToResponse()) 
            : NotFound();
    }

    [HttpPut]
    [Route("api/clubs/{clubId}/bookableResources/{bookableResourceId}/addUnit")]
    public async Task<IActionResult> AddUnitToBookableResource(Guid clubId, Guid bookableResourceId, [FromBody] AddUnitToBookableResourceRequest request)
    {
        BookableResource? updatedResource = await bookableResourceService
            .AddUnit(new BookableResourceId(bookableResourceId), request.UnitName);

        if (updatedResource is null)
            return NotFound();

        return Ok(updatedResource.ConvertToResponse());
    }

    [HttpPost]
    [Route("api/clubs/{clubId}/bookableResources")]
    public async Task<IActionResult> CreateNewBookableResource([FromBody] CreateNewBookableResourceRequest request)
    {
        BookableResource newResource = await bookableResourceService.CreateNewBookableResource(
            request.Name,
            request.Rules,
            request.OpenDate,
            request.EndDate);

        return Ok(newResource.ConvertToResponse());
    }

    [HttpPut]
    [Route("api/clubs/{clubId}/bookableResources/{bookableResourceId}/bookDays")]
    public async Task<IActionResult> BookDays(Guid clubId, Guid bookableResourceId, [FromBody] BookDaysRequest request)
    {
        BookableResource? updatedResource = await bookableResourceService.BookDays(
            new BookableResourceId(bookableResourceId),
            new (request.MemberId),
            new (request.UnitId),
            request.Date,
            request.Days);

        return updatedResource is not null 
            ? Ok(updatedResource.ConvertToResponse()) 
            : NotFound();
    }
}



