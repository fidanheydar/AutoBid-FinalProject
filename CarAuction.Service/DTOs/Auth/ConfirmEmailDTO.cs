namespace CarAuction.Service.DTOs.Auth;

public record ConfirmEmailDTO
{
    public string UserId { get; set; }
    public string Token { get; set; }
}