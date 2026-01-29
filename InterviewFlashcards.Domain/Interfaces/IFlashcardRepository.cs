using InterviewFlashcards.Domain.Entities;

namespace InterviewFlashcards.Domain.Interfaces;

public interface IFlashcardRepository : IRepository<Flashcard>
{
    Task<IEnumerable<Flashcard>> GetByThemeIdAsync(Guid themeId);
    Task<IEnumerable<Flashcard>> GetUnapprovedAsync();
}
