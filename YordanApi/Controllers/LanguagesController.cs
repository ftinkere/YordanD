using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YordanApi.Contracts.Repositories;
using YordanApi.DTOs;
using YordanApi.Models;
using YordanApi.Requests;
using YordanApi.Services;

namespace YordanApi.Controllers;

[ApiController]
public class LanguagesController(
    IUserRepository userRepository,
    LanguageService languageService
) : Controller {
    
    [HttpGet("/api/v1/languages")]
    public async Task<IActionResult> GetAll() {
        return Ok(new {
            items = (await languageService.GetAllLanguagesAsync())
                .OrderByDescending(l => l.Id)
                .Select(l => l
                .ToLight()
                .ToDto())
        });
    }

    [HttpGet("/api/v1/languages/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) {
        return Ok((await languageService.GetLanguageByIdAsync(id))?.ToDto());
    }

    [Authorize]
    [HttpPost("/api/v1/languages")]
    public async Task<IActionResult> Create([FromBody] CreateLanguageRequest request) {
        var user = await GetCurrentUser();
        if (user is null) {
            return Unauthorized();
        }

        var language = await languageService.CreateLanguageAsync(request, user.Id);
        if (language is not null) {
            return Ok(language.ToDto());
        }
        return BadRequest();
    }

    [Authorize]
    [HttpPut("/api/v1/languages/{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLanguageRequest request) {
        var user = await GetCurrentUser();
        if (user is null) {
            return Unauthorized();
        }

        var language = await languageService.GetLanguageByIdAsync(id);
        if (language is null) {
            return NotFound();
        }
        
        if (language.AuthorId != user.Id) {
            return Unauthorized();
        }

        language.Update(request.Name, request.AutoName, request.AutoNameTranscription, request.IsPublished, request.Description);
        if (await languageService.UpdateLanguageAsync(language) > 0) {
            return Ok(language.ToDto());
        }
        
        return BadRequest();
    }
    
    [Authorize]
    [HttpDelete("/api/v1/languages/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) {
        var user = await GetCurrentUser();
        if (user is null) {
            return Unauthorized();
        }
        
        var language = await languageService.GetLanguageByIdAsync(id);
        if (language is null) {
            return NotFound();
        }
        
        if (language.AuthorId != user.Id) {
            return Unauthorized();
        }
        
        var result = await languageService.DeleteLanguageAsync(id);
        if (result > 0) {
            return Ok();
        }
        return NotFound();
    }

    private async Task<ApplicationUser?> GetCurrentUser() {
        var name = HttpContext.User.Identity?.Name;
        if (name is null) {
            return null;
        }
        return await userRepository.GetByUserNameAsync(name);
    }
}