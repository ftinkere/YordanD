using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YordanApi.Contracts.Repositories;
using YordanApi.Models;
using YordanApi.Repositories;
using YordanApi.Requests;

namespace YordanApi.Controllers;

[Route("/api/v1/")]
[ApiController]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IUserRepository userRepository,
    ImageRepository imageRepository
) : Controller {
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userRepository.GetByUserNameAsync(request.UserName);
        if (user is null) return Unauthorized(new { message = "Неправильные логин или пароль" });

        var result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded) return Ok(new { message = "Успешный вход" });

        return Unauthorized(new { message = "Неправильный логин или пароль" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new ApplicationUser { UserName = request.UserName, Email = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) return BadRequest(new { errors = result.Errors });
        await signInManager.SignInAsync(user, isPersistent: false);
        return Ok(new { message = "Успешная регистрация" });

    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok(new { message = "Успешный выход" });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe() {
        var name = User.Identity?.Name;
        if (name is null) return Unauthorized();
        var user = await userRepository.GetByUserNameAsync(name);
        if (user is null) return Unauthorized();
        var authResult = await HttpContext.AuthenticateAsync(IdentityConstants.ApplicationScheme);
        var expiresUtc = authResult.Properties?.ExpiresUtc;
        return Ok(new {
            user = user.ToDto(),
            expiresAt = expiresUtc,
            avatar = (await imageRepository.GetAvatar(user.Id))?.Base64,
        });
    }

    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatar([FromBody] AvatarRequest request) {
        var name = User.Identity?.Name;
        if (name is null) return Unauthorized();
        var user = await userRepository.GetByUserNameAsync(name);
        if (user is null) return Unauthorized();
        
        await imageRepository.AddAvatar(user.Id, request.Base64);
        return Ok(new { message = "Аватар загружен" });
    }
}