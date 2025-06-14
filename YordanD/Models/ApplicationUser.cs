using Microsoft.AspNetCore.Identity;

namespace YordanD.Models;

public class ApplicationUser : IdentityUser {
    public string DisplayName { get; set; }
}