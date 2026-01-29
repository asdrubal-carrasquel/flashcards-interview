using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Domain.Entities;

namespace InterviewFlashcards.Application.Interfaces;

public interface IFlashcardService
{
    Task<FlashcardDto> CreateFlashcardAsync(CreateFlashcardDto dto);
    Task<IEnumerable<FlashcardDto>> GetFlashcardsByThemeAsync(Guid themeId);
    Task<IEnumerable<FlashcardDto>> GetAllFlashcardsAsync();
    Task<FlashcardDto> GenerateFlashcardsAsync(GenerateFlashcardsDto dto);
    Task<FlashcardDto> UpdateFlashcardAsync(Guid id, FlashcardDto dto);
    Task<bool> ApproveFlashcardAsync(Guid id);
    Task<bool> DeleteFlashcardAsync(Guid id);
}
