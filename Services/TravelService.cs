namespace dotnet_rideShare.Services;

using dotnet_rideShare.Entities;
using dotnet_rideShare.Contexts;
using dotnet_rideShare.DTOs;
using dotnet_rideShare.Enumerators;

public class TravelService
{
    private readonly DatabaseContext _dbContext;

    public TravelService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public bool AddTravelPlan(AddTravelRequest request)
    {
        var travel = new TravelPlans
        {
            DriverUserId = request.DriverUserId,
            DepartureCityId = request.DepartureCityId,
            DestinationCityId = request.DestinationCityId,
            Price = request.Price,
            Created = request.Created,
            DepartureDate = request.DepartureDate,
            PassengerCount = request.PassengerCount,
            TravelNote = request.TravelNote,
            Status = request.Status
        };
        _dbContext.TravelPlans.Add(travel);
        _dbContext.SaveChanges();
        return true;
    }

    public bool PublishTravelPlan(PublishTravelRequest request)
    {
        var travel = _dbContext.TravelPlans.FirstOrDefault(x => x.Id == request.TravelPlanId && x.DriverUserId == request.DriverUserId);
        if (travel == null)
        {
            return false;
        }
        travel.Status = TravelStatus.Open;
        _dbContext.SaveChanges();
        return true;
    }

    public TravelPlans? GetTravelPlansById(GetTravelPlansRequest request)
    {
        var travel = _dbContext.TravelPlans.FirstOrDefault(x => x.Id == request.TravelPlanId);
        return travel;
    }

    public bool CancelTravelPlan(CancelTravelRequest request)
    {
        var travel = _dbContext.TravelPlans.FirstOrDefault(x => x.Id == request.TravelPlanId && x.DriverUserId == request.DriverUserId);
        if (travel == null)
        {
            return false;
        }
        travel.Status = TravelStatus.Cancelled;
        _dbContext.SaveChanges();
        return true;
    }

    public TravelPlans? UpdateTravelPlan(UpdateTravelRequest request)
    {
        var travel = _dbContext.TravelPlans.FirstOrDefault(x => x.Id == request.TravelPlanId && x.DriverUserId == request.DriverUserId);
        if (travel == null)
        {
            return null;
        }
        travel.DepartureCityId = request.DepartureCityId ?? travel.DepartureCityId;
        travel.DestinationCityId = request.DestinationCityId ?? travel.DestinationCityId;
        travel.Price = request.Price ?? travel.Price;
        travel.Created = request.Created ?? travel.Created;
        travel.DepartureDate = request.DepartureDate ?? travel.DepartureDate;
        travel.PassengerCount = request.PassengerCount ?? travel.PassengerCount;
        travel.TravelNote = request.TravelNote ?? travel.TravelNote;
        travel.Status = request.Status ?? travel.Status;
        _dbContext.SaveChanges();
        return travel;
    }
}