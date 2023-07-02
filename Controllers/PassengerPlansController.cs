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

public class PassengerPlansController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private PassengerPlansService _passengerService;
    private readonly EmailService _emailService;

    public PassengerPlansController(ILogger<AuthController> logger, DatabaseContext dbContext, EmailService emailService)
    {
        _emailService = emailService;
        _passengerService = new PassengerPlansService(dbContext, _emailService);
        _logger = logger;
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPost("AddPassengerPlan")]
    public IActionResult AddPassengerPlan(AddPassengerPlanRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.PassengerUserId = userId;
        var result = _passengerService.AddPassengerPlan(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [Authorize(Policy = "UserPolicy")]
    [HttpPost("CancelPassengerPlan")]
    public IActionResult CancelPassengerPlan(CancelPassengerPlanRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.RequesterID = userId;
        var result = _passengerService.CancelPassengerPlan(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }
}