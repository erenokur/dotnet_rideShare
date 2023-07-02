namespace dotnet_rideShare.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet_rideShare.Enumerators;

public class Users
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string UserName { get; set; } = String.Empty;

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = String.Empty;

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = String.Empty;

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public UserRoles Role { get; set; } = UserRoles.User;

}