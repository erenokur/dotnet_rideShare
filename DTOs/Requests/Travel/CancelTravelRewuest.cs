namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;

public class CancelTravelRequest
{
    [Required]
    public int TravelPlanId { get; set; }
    public int? DriverUserId { get; set; }
}