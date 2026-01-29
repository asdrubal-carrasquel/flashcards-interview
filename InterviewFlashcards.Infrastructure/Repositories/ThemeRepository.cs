using InterviewFlashcards.Domain.Entities;
using InterviewFlashcards.Domain.Interfaces;
using InterviewFlashcards.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InterviewFlashcards.Infrastructure.Repositories;

public class ThemeRepository : IThemeRepository
{
    private readonly ApplicationDbContext _context;

    public ThemeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Theme?> GetByIdAsync(Guid id)
    {
        return await _context.Themes
            .Include(t => t.Flashcards)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Theme>> GetAllAsync()
    {
        return await _context.Themes
            .Include(t => t.Flashcards)
            .ToListAsync();
    }

    public async Task<Theme> AddAsync(Theme entity)
    {
        await _context.Themes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Theme> UpdateAsync(Theme entity)
    {
        _context.Themes.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var theme = await _context.Themes.FindAsync(id);
        if (theme == null) return false;

        _context.Themes.Remove(theme);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Theme?> GetByNameAsync(string name)
    {
        return await _context.Themes
            .FirstOrDefaultAsync(t => t.Name == name);
    }
}
