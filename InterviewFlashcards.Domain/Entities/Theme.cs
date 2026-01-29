namespace InterviewFlashcards.Domain.Entities;

public class Theme
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StackTecnologico { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
}
