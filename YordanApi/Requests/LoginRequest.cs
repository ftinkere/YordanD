using System.ComponentModel.DataAnnotations;

namespace YordanApi.Requests;

public class LoginRequest
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }

    public required bool RememberMe { get; set; }
}