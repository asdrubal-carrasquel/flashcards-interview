using InterviewFlashcards.Application.DTOs;

namespace InterviewFlashcards.Application.Interfaces;

public interface IThemeService
{
    Task<ThemeDto> CreateThemeAsync(CreateThemeDto dto);
    Task<IEnumerable<ThemeDto>> GetAllThemesAsync();
    Task<ThemeDto?> GetThemeByIdAsync(Guid id);
}
