namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;
using dotnet_rideShare.Enumerators;

public class ChangeUserRoleRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public UserRoles Role { get; set; } = UserRoles.User;
}