namespace dotnet_rideShare.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet_rideShare.Enumerators;

public class PassengerPlans
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int PassengerUserId { get; set; }

    [Required]
    public int TravelPlanId { get; set; }

    [Required]
    public int PassengerAndFriendsCount { get; set; }


    [Required]
    public DateTime Created { get; set; }

    [Required]
    public PassengerPlanStatus Status { get; set; } = PassengerPlanStatus.Active;

    public string PassengerNote { get; set; } = String.Empty;

}