namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;

public class CancelPassengerPlanRequest
{
    [Required]
    public int PassengerPlanId { get; set; }

    [Required]
    public int? PassengerUserId { get; set; }


    public int? DriverUserId { get; set; }

    // For security reasons not all users can cancel passenger plans 
    public int? RequesterID { get; set; }

    public string PassengerNote { get; set; } = String.Empty;

}