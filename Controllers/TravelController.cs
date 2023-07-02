namespace dotnet_rideShare.Controllers;

using dotnet_rideShare.Contexts;
using dotnet_rideShare.Models;
using dotnet_rideShare.DTOs;
using dotnet_rideShare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


[ApiController]
[Route("[controller]")]

public class TravelController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private TravelService _travelService;
    private AppSettings _appSettings;

    public TravelController(ILogger<AuthController> logger, IOptions<AppSettings> appSettings, DatabaseContext dbContext)
    {
        _travelService = new TravelService(dbContext);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpGet("GetTravelPlansById")]
    public IActionResult GetTravelPlansById(GetTravelPlansRequest request)
    {
        var result = _travelService.GetTravelPlansById(request);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPost("AddTravelPlan")]
    public IActionResult AddTravelPlan(AddTravelRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.DriverUserId = userId;
        var result = _travelService.AddTravelPlan(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPost("PublishTravelPlan")]
    public IActionResult PublishTravelPlan(PublishTravelRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.DriverUserId = userId;
        var result = _travelService.PublishTravelPlan(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }


    [Authorize(Policy = "UserPolicy")]
    [HttpPost("CancelTravelPlan")]
    public IActionResult CancelTravelPlan(CancelTravelRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.DriverUserId = userId;
        var result = _travelService.CancelTravelPlan(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPost("UpdateTravelPlan")]
    public IActionResult UpdateTravelPlan(UpdateTravelRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.DriverUserId = userId;
        var travelPlan = _travelService.UpdateTravelPlan(request);
        if (travelPlan != null)
        {
            return Ok(travelPlan);
        }
        return BadRequest();
    }
}