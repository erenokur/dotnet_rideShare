namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;


public class TravelSearchRequest
{
    public int PassengerUserId { get; set; }
    [Required]
    public int? DepartureCityId { get; set; }
    [Required]
    public int? DestinationCityId { get; set; }
    [Required]
    public DateTime? DepartureDate { get; set; }

    [Required]
    public int? PassengerCount { get; set; }

    [Required]
    public int CurrentPage { get; set; }
}