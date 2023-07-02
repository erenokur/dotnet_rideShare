namespace dotnet_rideShare.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet_rideShare.Enumerators;

public class TravelPlans
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
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