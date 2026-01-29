using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Application.Interfaces;
using InterviewFlashcards.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InterviewFlashcards.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlashcardsController : ControllerBase
{
    private readonly IFlashcardService _flashcardService;

    public FlashcardsController(IFlashcardService flashcardService)
    {
        _flashcardService = flashcardService;
    }

    [HttpPost]
    public async Task<ActionResult<FlashcardDto>> CreateFlashcard([FromBody] CreateFlashcardDto dto)
    {
        try
        {
            var flashcard = await _flashcardService.CreateFlashcardAsync(dto);
            return CreatedAtAction(nameof(GetFlashcard), new { id = flashcard.Id }, flashcard);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlashcardDto>>> GetFlashcards([FromQuery] Guid? themeId)
    {
        IEnumerable<FlashcardDto> flashcards;
        
        if (themeId.HasValue)
        {
            flashcards = await _flashcardService.GetFlashcardsByThemeAsync(themeId.Value);
        }
        else
        {
            flashcards = await _flashcardService.GetAllFlashcardsAsync();
        }

        return Ok(flashcards);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FlashcardDto>> GetFlashcard(Guid id)
    {
        var flashcards = await _flashcardService.GetAllFlashcardsAsync();
        var flashcard = flashcards.FirstOrDefault(f => f.Id == id);
        
        if (flashcard == null)
        {
            return NotFound();
        }
        
        return Ok(flashcard);
    }

    [HttpPost("generate")]
    public async Task<ActionResult<IEnumerable<FlashcardDto>>> GenerateFlashcards([FromBody] GenerateFlashcardsDto dto)
    {
        try
        {
            var flashcard = await _flashcardService.GenerateFlashcardsAsync(dto);
            
            // Retornar todas las flashcards del tema para que el frontend pueda ver las generadas
            var allFlashcards = await _flashcardService.GetFlashcardsByThemeAsync(dto.TemaId);
            var generatedFlashcards = allFlashcards.Where(f => !f.Aprobada && f.Fuente == Domain.Entities.FuentePregunta.AI)
                                                   .OrderByDescending(f => f.CreatedAt)
                                                   .Take(dto.Cantidad);
            
            return Ok(generatedFlashcards);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FlashcardDto>> UpdateFlashcard(Guid id, [FromBody] FlashcardDto dto)
    {
        try
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var flashcard = await _flashcardService.UpdateFlashcardAsync(id, dto);
            return Ok(flashcard);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/approve")]
    public async Task<ActionResult> ApproveFlashcard(Guid id)
    {
        var result = await _flashcardService.ApproveFlashcardAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok(new { message = "Flashcard aprobada" });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFlashcard(Guid id)
    {
        var result = await _flashcardService.DeleteFlashcardAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok(new { message = "Flashcard eliminada" });
    }
}
