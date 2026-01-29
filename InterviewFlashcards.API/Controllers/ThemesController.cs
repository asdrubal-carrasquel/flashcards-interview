using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterviewFlashcards.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThemesController : ControllerBase
{
    private readonly IThemeService _themeService;

    public ThemesController(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [HttpPost]
    public async Task<ActionResult<ThemeDto>> CreateTheme([FromBody] CreateThemeDto dto)
    {
        try
        {
            var theme = await _themeService.CreateThemeAsync(dto);
            return CreatedAtAction(nameof(GetTheme), new { id = theme.Id }, theme);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ThemeDto>>> GetAllThemes()
    {
        var themes = await _themeService.GetAllThemesAsync();
        return Ok(themes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ThemeDto>> GetTheme(Guid id)
    {
        var theme = await _themeService.GetThemeByIdAsync(id);
        if (theme == null)
        {
            return NotFound();
        }
        return Ok(theme);
    }
}
