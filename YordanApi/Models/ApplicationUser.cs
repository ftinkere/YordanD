using Microsoft.AspNetCore.Identity;
using YordanApi.DTOs;

namespace YordanApi.Models;

public class ApplicationUser : IdentityUser<Guid> {
    public string? DisplayName { get; set; }
    
    public UserDto ToDto() {
        return new UserDto {
            Id = Id,
            UserName = UserName ?? Id.ToString(),
            DisplayName = DisplayName
        };
    }
}
