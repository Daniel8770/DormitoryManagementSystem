using DormitoryManagementSystem.Application.KitchenContext;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.API.DTOs.Requests.KitchenContext;
using DormitoryManagementSystem.API.DTOs.Responses.KitchenContext;
using DormitoryManagementSystem.API.DTOs.Responses;



namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/kitchens")]
public class KitchensController : Controller
{
    private KitchenService kitchenService;
    private LinkGenerator linkGenerator;

    public KitchensController(KitchenService kitchenService, LinkGenerator linkGenerator)
    {
        this.kitchenService = kitchenService;
        this.linkGenerator = linkGenerator;
    }

    [HttpPost("openNew")]
    [EndpointName("open-new-kitchen")]
    public async Task<IActionResult> OpenNewKitchen(OpenNewKitchenRequest request)
    {
        Kitchen newKitchen = await kitchenService.OpenNewKitchen(request.Name);
        
        string uri = linkGenerator.GetUriByName(
            HttpContext,
            "open-new-kitchen-balance",
            new { kitchenId = newKitchen.Id.Value }) ?? throw new Exception();
        
        return Ok(new OpenNewKitchenResponse(
            newKitchen.Id.Value,
            newKitchen.Information.Name,
            newKitchen.Information?.Description,
            newKitchen.Information?.Rules,
            newKitchen.KitchenAccountId?.Value,
            newKitchen.Residents)
            .AddLink(new Link("open-new-kitchen-balance", CommonLinkStrings.POST, uri))
        );
    }

    [HttpPost("{kitchenId}/kitchenBalances/openNew")]
    [EndpointName("open-new-kitchen-balance")]
    public async Task<IActionResult> OpenNewKitchenBalanceWithAllResidents(Guid kitchenId, OpenNewKitchenBalanceRequest request)
    {
        Currency currency;
        if (!Enum.TryParse(request.Currency, true, out currency))
            return BadRequest(new { Message = "Invalid currency." });

        KitchenBalance newBalance = await kitchenService.OpenNewKitchenBalanceOnKitchenWithAllResidents(
            new KitchenId(kitchenId),
            request.Name,
            currency);

        return Ok(newBalance);
    }
}
