namespace dotnet_rideShare.Services;

using dotnet_rideShare.Entities;
using dotnet_rideShare.Contexts;
using dotnet_rideShare.DTOs;
using dotnet_rideShare.Enumerators;
using Microsoft.EntityFrameworkCore;
using dotnet_rideShare.Models;
using dotnet_rideShare.Helpers;
using dotnet_rideShare.Interfaces;

public class TravelSearchService
{
    private readonly DatabaseContext _dbContext;
    private AppSettings _appSettings;
    public TravelSearchService(DatabaseContext dbContext, AppSettings appSettings)
    {
        _dbContext = dbContext;
        _appSettings = appSettings;
    }

    public async Task<IEnumerable<TravelPlans>> SearchCityToCityTravelPlan(TravelSearchRequest request)
    {
        var travelPlans = await _dbContext.TravelPlans
            .Where(x => x.DepartureCityId == request.DepartureCityId && x.DestinationCityId == request.DestinationCityId)
            .ToListAsync();
        return await CheckSearchResults(travelPlans, request);
    }


    public async Task<IEnumerable<TravelPlans>> SearchAdvancedTravelPlan(TravelSearchRequest request)
    {
        // First, get all X and Y information for the departure city and destination city
        var departureCity = _dbContext.Cities.FirstOrDefault(x => x.Id == request.DepartureCityId);
        var destinationCity = _dbContext.Cities.FirstOrDefault(x => x.Id == request.DestinationCityId);
        if (departureCity == null || destinationCity == null)
        {
            return Enumerable.Empty<TravelPlans>();
        }

        var cityNodes = _dbContext.Cities.Select(city => new CityNode
        {
            Id = city.Id,
            XCoordinate = city.XCoordinate,
            YCoordinate = city.YCoordinate
        }).ToList();

        var routeFinder = new RouteHelper(cityNodes);
        var citiesInBetween = routeFinder.FindBestRoute(departureCity.Id, destinationCity.Id);
        // Get all city IDs that are in between the departure city and destination city
        // var citiesInBetween = await _dbContext.Cities
        //     .Where(x => x.XCoordinate > departureCity.XCoordinate && x.XCoordinate < destinationCity.XCoordinate &&
        //                 x.YCoordinate > departureCity.YCoordinate && x.YCoordinate < destinationCity.YCoordinate)
        //     .Select(x => x.Id)
        //     .ToListAsync();

        // Get all travel plans that are in between the departure city and destination city
        var travelPlans = await _dbContext.TravelPlans
            .Where(x => citiesInBetween.Contains(x.DepartureCityId) && citiesInBetween.Contains(x.DestinationCityId))
            .ToListAsync();

        return await CheckSearchResults(travelPlans, request);
    }

    private async Task<IEnumerable<TravelPlans>> CheckSearchResults(IEnumerable<TravelPlans> travelPlans, TravelSearchRequest request)
    {
        // Check if there are any travel plans that have the same departure date and passenger count as the request
        var filteredResults = await FilterSearchResultByAvailability(travelPlans, request);

        // Check if when request passenger count added to the passenger count of the travel plan is greater than the maximum passenger count
        if (filteredResults.Count() > 0)
        {
            int calculateIndex = request.CurrentPage * _appSettings.PageSize;
            var filteredResultsPassengerCountCheck = filteredResults
                .Where(x => x.PassengerCount + request.PassengerCount <= x.PassengerCount)
                .Skip(calculateIndex)
                .Take(_appSettings.PageSize)
                .ToList();
            if (filteredResultsPassengerCountCheck.Count > 0)
            {

                return filteredResultsPassengerCountCheck;
            }
        }
        return Enumerable.Empty<TravelPlans>();
    }

    private async Task<IEnumerable<TravelPlans>> FilterSearchResultByAvailability(IEnumerable<TravelPlans> travelPlans, TravelSearchRequest request)
    {
        // Get all travel plans that have the same departure date
        var travelPlansWithSameDepartureDateAndOpen = travelPlans
        .Where(x => x.DepartureDate == request.DepartureDate && x.Status == TravelStatus.Open)
        .ToList();

        // Get all travel plans that have the same departure date and passenger count
        var travelPlansWithSameDepartureDateAndPassengerCount = travelPlansWithSameDepartureDateAndOpen
            .Where(x => x.PassengerCount >= request.PassengerCount)
            .ToList();

        // Get all PassengerPlans that have the same travel plan ID as the travel plans that have the same departure date and passenger count
        var passengerPlans = await _dbContext.PassengerPlans
            .Where(x => x.Status == PassengerPlanStatus.Active && travelPlansWithSameDepartureDateAndPassengerCount.Select(y => y.Id).Contains(x.TravelPlanId))
            .ToListAsync();

        // Get all travel plans that have the same departure date and passenger count and have not reached the maximum passenger count
        travelPlansWithSameDepartureDateAndPassengerCount = travelPlansWithSameDepartureDateAndPassengerCount
            .Where(x => passengerPlans.Count(y => y.TravelPlanId == x.Id) < x.PassengerCount)
            .ToList();

        return travelPlansWithSameDepartureDateAndPassengerCount;
    }
}