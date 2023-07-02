namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;
using dotnet_rideShare.Enumerators;

public class UpdateUserRequest
{
    public string Password { get; set; } = String.Empty;

    public string UserName { get; set; } = String.Empty;

    public int UserId { get; set; }
}