namespace dotnet_rideShare.DTOs;

using System.ComponentModel.DataAnnotations;

public class SendEmailRequest
{
    [Required]
    public string From { get; set; } = String.Empty;
    [Required]
    public string To { get; set; } = String.Empty;

    [Required]
    public string Subject { get; set; } = String.Empty;
    [Required]
    public string Body { get; set; } = String.Empty;

}