using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Application.Interfaces;
using InterviewFlashcards.Domain.Entities;
using InterviewFlashcards.Domain.Interfaces;

namespace InterviewFlashcards.Application.Services;

public class ThemeService : IThemeService
{
    private readonly IThemeRepository _repository;

    public ThemeService(IThemeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ThemeDto> CreateThemeAsync(CreateThemeDto dto)
    {
        var existingTheme = await _repository.GetByNameAsync(dto.Name);
        if (existingTheme != null)
        {
            throw new InvalidOperationException($"Ya existe un tema con el nombre '{dto.Name}'");
        }

        var theme = new Theme
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            StackTecnologico = dto.StackTecnologico,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(theme);
        return MapToDto(created);
    }

    public async Task<IEnumerable<ThemeDto>> GetAllThemesAsync()
    {
        var themes = await _repository.GetAllAsync();
        return themes.Select(MapToDto);
    }

    public async Task<ThemeDto?> GetThemeByIdAsync(Guid id)
    {
        var theme = await _repository.GetByIdAsync(id);
        return theme == null ? null : MapToDto(theme);
    }

    private static ThemeDto MapToDto(Theme theme)
    {
        return new ThemeDto
        {
            Id = theme.Id,
            Name = theme.Name,
            Description = theme.Description,
            StackTecnologico = theme.StackTecnologico,
            CreatedAt = theme.CreatedAt
        };
    }
}
