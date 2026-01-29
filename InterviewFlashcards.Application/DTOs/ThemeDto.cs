namespace InterviewFlashcards.Application.DTOs;

public class ThemeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StackTecnologico { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateThemeDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StackTecnologico { get; set; } = string.Empty;
}
