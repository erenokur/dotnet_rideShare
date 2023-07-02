namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;
}