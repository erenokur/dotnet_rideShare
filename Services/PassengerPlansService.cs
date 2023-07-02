namespace dotnet_rideShare.Services;

using dotnet_rideShare.Entities;
using dotnet_rideShare.Contexts;
using dotnet_rideShare.DTOs;
using dotnet_rideShare.Enumerators;

public class PassengerPlansService
{
    private readonly DatabaseContext _dbContext;
    private readonly EmailService _emailService;

    public PassengerPlansService(DatabaseContext dbContext, EmailService emailService)
    {
        _emailService = emailService;
        _dbContext = dbContext;
    }

    public bool CancelPassengerPlan(CancelPassengerPlanRequest request)
    {
        //Check if PassengerPlans with same TravelPlanId and PassengerUserId exists
        var passengerPlanExists = _dbContext.PassengerPlans.Where(x => x.TravelPlanId == request.PassengerPlanId && x.PassengerUserId == request.PassengerUserId).FirstOrDefault();
        if (passengerPlanExists == null)
        {
            return false;
        }
        var travelPlan = _dbContext.TravelPlans.Find(passengerPlanExists.TravelPlanId);
        if (travelPlan == null)
        {
            return false;
        }
        if (request.RequesterID == travelPlan.DriverUserId || request.RequesterID == passengerPlanExists.PassengerUserId)
        {
            // Only Driver or Passenger can cancel the plan
            return false;
        }
        if (request.DriverUserId != null)
        {
            passengerPlanExists.Status = PassengerPlanStatus.DriverCancelled;
        }
        else
        {
            passengerPlanExists.Status = PassengerPlanStatus.PassengerCancelled;
            passengerPlanExists.PassengerNote = request.PassengerNote;
        }
        SendEmail(passengerPlanExists.PassengerUserId);
        _dbContext.SaveChanges();
        return true;
    }

    public bool AddPassengerPlan(AddPassengerPlanRequest request)
    {
        //Check if PassengerPlans with same TravelPlanId and PassengerUserId exists
        var passengerPlanExists = _dbContext.PassengerPlans.Any(x => x.TravelPlanId == request.TravelPlanId && x.PassengerUserId == request.PassengerUserId);
        if (passengerPlanExists)
        {
            return false;
        }
        //Check if TravelPlan exists
        var travelPlan = _dbContext.TravelPlans.Find(request.TravelPlanId);
        if (travelPlan == null || request.PassengerUserId == null || request.PassengerCount == 0)
        {
            return false;
        }
        // Check if TravelPlan PassengerCount is not full
        if (travelPlan.PassengerCount < travelPlan.PassengerCount + request.PassengerCount)
        {
            return false;
        }
        var passengerPlan = new PassengerPlans
        {
            PassengerUserId = (int)request.PassengerUserId,
            TravelPlanId = request.TravelPlanId,
            Created = DateTime.Now,
            Status = PassengerPlanStatus.Active,
            PassengerNote = request.PassengerNote,
        };
        _dbContext.PassengerPlans.Add(passengerPlan);
        _dbContext.SaveChanges();
        return true;
    }

    private async void SendEmail(int UserId)
    {
        var user = _dbContext.Users.Find(UserId);
        if (user == null)
        {
            return;
        }
        SendEmailRequest request = new SendEmailRequest
        {
            From = "rideshare@rideshare.com",
            To = user.Email,
            Subject = "RideShare - Passenger Plan Cancelled",
            Body = "Your Passenger Plan has been cancelled",
        };
        await _emailService.SendEmailAsync(request);
        return;
    }
}