using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YordanD.Models;

namespace YordanD.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager
) : Controller {
    
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index() {
        return Inertia.Render("Index");
    }

    [HttpGet("/login")]
    public IActionResult Login() {
        return Inertia.Render("Login");
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> LoginPost([FromBody] LoginRequest request, [FromQuery] string? returnUrl = null) {
        if (!ModelState.IsValid) {
            return Login();
        }
        
        returnUrl ??= "/";
        
        var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
        if (result.Succeeded) return Inertia.Location(returnUrl);
        
        ModelState.AddModelError("Username", "Неправильный логин или пароль.");
        return Login();

    }

    [HttpGet("/register")]
    public IActionResult Register() {
        return Inertia.Render("Register");
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> RegisterPost([FromBody] RegisterRequest request) {
        if (!ModelState.IsValid) {
            return Register();
        }
        
        var user = new ApplicationUser { UserName = request.Username, Email = request.Email, DisplayName = request.DisplayName };
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("Password", "Неподходящий пароль");
            return Register();
        }
        await signInManager.SignInAsync(user, isPersistent: true);
        
        return Inertia.Location("/");
    }
    
    [Authorize]
    [HttpGet("/logout")]
    public async Task<IActionResult> Logout() {
        await signInManager.SignOutAsync();
        return Inertia.Location("/"); 
    }
}

public class LoginRequest {
    [Required, MinLength(2), MaxLength(255)]
    public required string Username { get; set; }
    [Required, MinLength(8), MaxLength(255)]
    public required string Password { get; set; }
}

public class RegisterRequest {
    [Required, MinLength(2), MaxLength(255)]
    public required string Username { get; set; }
    [Required, MinLength(2), MaxLength(255)]
    public required string Email { get; set; }
    [Required, MinLength(2), MaxLength(255)]
    public required string DisplayName { get; set; }
    [Required, MinLength(8), MaxLength(255)]
    public required string Password { get; set; }
}
