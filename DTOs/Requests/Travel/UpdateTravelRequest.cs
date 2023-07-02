namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;
using dotnet_rideShare.Enumerators;

public class UpdateTravelRequest
{
    [Required]
    public int TravelPlanId { get; set; }
    public int? DriverUserId { get; set; }

    public int? DepartureCityId { get; set; }

    public int? DestinationCityId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? DepartureDate { get; set; }

    public int? PassengerCount { get; set; }

    public string? TravelNote { get; set; }

    public TravelStatus? Status { get; set; }

}