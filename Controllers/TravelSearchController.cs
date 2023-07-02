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

public class TravelSearchController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private TravelSearchService _travelSearchService;
    private AppSettings _appSettings;
    public TravelSearchController(ILogger<AuthController> logger, IOptions<AppSettings> appSettings, DatabaseContext dbContext)
    {
        _appSettings = appSettings.Value;
        _travelSearchService = new TravelSearchService(dbContext, _appSettings);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpGet("SearchCityToCityTravelPlan")]
    public IActionResult SearchCityToCityTravelPlan(TravelSearchRequest request)
    {
        var result = _travelSearchService.SearchCityToCityTravelPlan(request);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpGet("SearchAdvancedTravelPlan")]
    public IActionResult SearchAdvancedTravelPlan(TravelSearchRequest request)
    {
        var result = _travelSearchService.SearchAdvancedTravelPlan(request);
        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest();
    }
}