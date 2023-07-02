namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;

public class AddPassengerPlanRequest
{
    [Required]
    public int TravelPlanId { get; set; }

    [Required]
    public int PassengerCount { get; set; }
    public int? PassengerUserId { get; set; }

    public string PassengerNote { get; set; } = String.Empty;

}