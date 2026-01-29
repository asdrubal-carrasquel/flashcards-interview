using InterviewFlashcards.Domain.Entities;

namespace InterviewFlashcards.Application.DTOs;

public class FlashcardDto
{
    public Guid Id { get; set; }
    public Guid TemaId { get; set; }
    public string Pregunta { get; set; } = string.Empty;
    public string Respuesta { get; set; } = string.Empty;
    public Nivel Nivel { get; set; }
    public TipoPregunta Tipo { get; set; }
    public FuentePregunta Fuente { get; set; }
    public bool Aprobada { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateFlashcardDto
{
    public Guid TemaId { get; set; }
    public string Pregunta { get; set; } = string.Empty;
    public string Respuesta { get; set; } = string.Empty;
    public Nivel Nivel { get; set; }
    public TipoPregunta Tipo { get; set; }
}

public class GenerateFlashcardsDto
{
    public Guid TemaId { get; set; }
    public Nivel? Nivel { get; set; }
    public int Cantidad { get; set; } = 5;
}
