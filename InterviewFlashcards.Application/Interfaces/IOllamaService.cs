using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Domain.Entities;

namespace InterviewFlashcards.Application.Interfaces;

public interface IOllamaService
{
    Task<List<FlashcardDto>> GenerateFlashcardsAsync(string tema, string stack, Nivel? nivel = null, int cantidad = 5);
}
