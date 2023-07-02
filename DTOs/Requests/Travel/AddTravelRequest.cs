namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;
using dotnet_rideShare.Enumerators;

public class AddTravelRequest
{
    public int DriverUserId { get; set; }

    [Required]
    public int DepartureCityId { get; set; }

    [Required]
    public int DestinationCityId { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime DepartureDate { get; set; }

    [Required]
    public int PassengerCount { get; set; }

    public string TravelNote { get; set; } = String.Empty;
    [Required]
    public TravelStatus Status { get; set; } = TravelStatus.Open;

}