using DormitoryManagementSystem.Application.KitchenContext;
using Microsoft.AspNetCore.Mvc;


namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KitchenController : Controller
{
    private KitchenService kitchenService;

    public KitchenController(KitchenService kitchenService)
    {
        this.kitchenService = kitchenService;
    }

    [HttpPut("raiseKitchenCreatedEvent")]
    public async Task<IActionResult> RaiseKitchenCreatedEvent()
    {
        await kitchenService.AddKitchenAccount();
        return Ok();
    }
}
