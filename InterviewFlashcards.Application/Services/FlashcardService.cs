using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Application.Interfaces;
using InterviewFlashcards.Domain.Entities;
using InterviewFlashcards.Domain.Interfaces;

namespace InterviewFlashcards.Application.Services;

public class FlashcardService : IFlashcardService
{
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly IThemeRepository _themeRepository;
    private readonly IOllamaService _ollamaService;

    public FlashcardService(
        IFlashcardRepository flashcardRepository,
        IThemeRepository themeRepository,
        IOllamaService ollamaService)
    {
        _flashcardRepository = flashcardRepository;
        _themeRepository = themeRepository;
        _ollamaService = ollamaService;
    }

    public async Task<FlashcardDto> CreateFlashcardAsync(CreateFlashcardDto dto)
    {
        var theme = await _themeRepository.GetByIdAsync(dto.TemaId);
        if (theme == null)
        {
            throw new InvalidOperationException($"No se encontró el tema con ID {dto.TemaId}");
        }

        var flashcard = new Flashcard
        {
            Id = Guid.NewGuid(),
            TemaId = dto.TemaId,
            Pregunta = dto.Pregunta,
            Respuesta = dto.Respuesta,
            Nivel = dto.Nivel,
            Tipo = dto.Tipo,
            Fuente = FuentePregunta.Manual,
            Aprobada = true, // Las manuales se aprueban automáticamente
            CreatedAt = DateTime.UtcNow
        };

        var created = await _flashcardRepository.AddAsync(flashcard);
        return MapToDto(created);
    }

    public async Task<IEnumerable<FlashcardDto>> GetFlashcardsByThemeAsync(Guid themeId)
    {
        var flashcards = await _flashcardRepository.GetByThemeIdAsync(themeId);
        return flashcards.Select(MapToDto);
    }

    public async Task<IEnumerable<FlashcardDto>> GetAllFlashcardsAsync()
    {
        var flashcards = await _flashcardRepository.GetAllAsync();
        return flashcards.Select(MapToDto);
    }

    public async Task<FlashcardDto> GenerateFlashcardsAsync(GenerateFlashcardsDto dto)
    {
        var theme = await _themeRepository.GetByIdAsync(dto.TemaId);
        if (theme == null)
        {
            throw new InvalidOperationException($"No se encontró el tema con ID {dto.TemaId}");
        }

        var generatedFlashcards = await _ollamaService.GenerateFlashcardsAsync(
            theme.Name,
            theme.StackTecnologico,
            dto.Nivel,
            dto.Cantidad
        );

        // Guardar todas las flashcards generadas como no aprobadas
        var savedFlashcards = new List<Flashcard>();
        foreach (var flashcardDto in generatedFlashcards)
        {
            var flashcard = new Flashcard
            {
                Id = flashcardDto.Id,
                TemaId = dto.TemaId,
                Pregunta = flashcardDto.Pregunta,
                Respuesta = flashcardDto.Respuesta,
                Nivel = flashcardDto.Nivel,
                Tipo = flashcardDto.Tipo,
                Fuente = FuentePregunta.AI,
                Aprobada = false,
                CreatedAt = DateTime.UtcNow
            };

            var saved = await _flashcardRepository.AddAsync(flashcard);
            savedFlashcards.Add(saved);
        }

        // Retornar la primera flashcard generada (o podríamos retornar todas)
        return savedFlashcards.Count > 0 ? MapToDto(savedFlashcards.First()) : throw new Exception("No se pudieron generar flashcards");
    }

    public async Task<FlashcardDto> UpdateFlashcardAsync(Guid id, FlashcardDto dto)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(id);
        if (flashcard == null)
        {
            throw new InvalidOperationException($"No se encontró la flashcard con ID {id}");
        }

        flashcard.Pregunta = dto.Pregunta;
        flashcard.Respuesta = dto.Respuesta;
        flashcard.Nivel = dto.Nivel;
        flashcard.Tipo = dto.Tipo;
        flashcard.Aprobada = dto.Aprobada;

        var updated = await _flashcardRepository.UpdateAsync(flashcard);
        return MapToDto(updated);
    }

    public async Task<bool> ApproveFlashcardAsync(Guid id)
    {
        var flashcard = await _flashcardRepository.GetByIdAsync(id);
        if (flashcard == null)
        {
            return false;
        }

        flashcard.Aprobada = true;
        await _flashcardRepository.UpdateAsync(flashcard);
        return true;
    }

    public async Task<bool> DeleteFlashcardAsync(Guid id)
    {
        return await _flashcardRepository.DeleteAsync(id);
    }

    private static FlashcardDto MapToDto(Flashcard flashcard)
    {
        return new FlashcardDto
        {
            Id = flashcard.Id,
            TemaId = flashcard.TemaId,
            Pregunta = flashcard.Pregunta,
            Respuesta = flashcard.Respuesta,
            Nivel = flashcard.Nivel,
            Tipo = flashcard.Tipo,
            Fuente = flashcard.Fuente,
            Aprobada = flashcard.Aprobada,
            CreatedAt = flashcard.CreatedAt
        };
    }
}
