namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;

public class GetTravelPlansRequest
{
    [Required]
    public int TravelPlanId { get; set; }
    public int? UserId { get; set; }
}