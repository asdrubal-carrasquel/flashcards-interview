using InterviewFlashcards.Domain.Entities;
using InterviewFlashcards.Domain.Interfaces;
using InterviewFlashcards.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InterviewFlashcards.Infrastructure.Repositories;

public class FlashcardRepository : IFlashcardRepository
{
    private readonly ApplicationDbContext _context;

    public FlashcardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Flashcard?> GetByIdAsync(Guid id)
    {
        return await _context.Flashcards
            .Include(f => f.Theme)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Flashcard>> GetAllAsync()
    {
        return await _context.Flashcards
            .Include(f => f.Theme)
            .ToListAsync();
    }

    public async Task<Flashcard> AddAsync(Flashcard entity)
    {
        await _context.Flashcards.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Flashcard> UpdateAsync(Flashcard entity)
    {
        _context.Flashcards.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var flashcard = await _context.Flashcards.FindAsync(id);
        if (flashcard == null) return false;

        _context.Flashcards.Remove(flashcard);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Flashcard>> GetByThemeIdAsync(Guid themeId)
    {
        return await _context.Flashcards
            .Include(f => f.Theme)
            .Where(f => f.TemaId == themeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Flashcard>> GetUnapprovedAsync()
    {
        return await _context.Flashcards
            .Include(f => f.Theme)
            .Where(f => !f.Aprobada)
            .ToListAsync();
    }
}
