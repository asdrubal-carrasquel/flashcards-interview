namespace InterviewFlashcards.Domain.Entities;

public class Flashcard
{
    public Guid Id { get; set; }
    public Guid TemaId { get; set; }
    public string Pregunta { get; set; } = string.Empty;
    public string Respuesta { get; set; } = string.Empty;
    public Nivel Nivel { get; set; }
    public TipoPregunta Tipo { get; set; }
    public FuentePregunta Fuente { get; set; }
    public bool Aprobada { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public Theme Theme { get; set; } = null!;
}

public enum Nivel
{
    Junior,
    Mid,
    Senior
}

public enum TipoPregunta
{
    Conceptual,
    Practical,
    SystemDesign,
    Tricky
}

public enum FuentePregunta
{
    AI,
    Manual
}
