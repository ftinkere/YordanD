using System.ComponentModel.DataAnnotations;

namespace YordanApi.Requests;

public class RegisterRequest
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
}