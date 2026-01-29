using InterviewFlashcards.Domain.Entities;

namespace InterviewFlashcards.Domain.Interfaces;

public interface IThemeRepository : IRepository<Theme>
{
    Task<Theme?> GetByNameAsync(string name);
}
