using DormitoryManagementSystem.Application.KitchenContext;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.API.DTOs.Requests.KitchenContext;


namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/kitchens")]
public class KitchensController : Controller
{
    private KitchenService kitchenService;

    public KitchensController(KitchenService kitchenService)
    {
        this.kitchenService = kitchenService;
    }

    [HttpPost("openNew")]
    public async Task<IActionResult> OpenNewKitchen(OpenNewKitchenRequest request)
    {
        Kitchen newKitchen = await kitchenService.OpenNewKitchen(request.Name);
        return Ok(newKitchen);
    }

    [HttpPost("{kitchenId}/kitchenBalances/openNew")]
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
