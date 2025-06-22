using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YordanApi.Contracts.Repositories;
using YordanApi.Models;

namespace YordanApi.Controllers;

[ApiController]
public class LanguagesController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IUserRepository userRepository
) : Controller {
    
    [HttpGet("/api/v1/languages")]
    public async Task<IActionResult> Get() {
        var name = HttpContext.User.Identity?.Name;
        if (name is null) {
            return Unauthorized();
        }
        return Ok(new { user = (await userRepository.GetByUserNameAsync(name))?.ToDto() });
    }
}