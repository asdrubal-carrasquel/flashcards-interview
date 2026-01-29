using InterviewFlashcards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewFlashcards.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Theme> Themes { get; set; }
    public DbSet<Flashcard> Flashcards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.StackTecnologico).HasMaxLength(200);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Pregunta).IsRequired();
            entity.Property(e => e.Respuesta).IsRequired();
            entity.HasOne(e => e.Theme)
                  .WithMany(t => t.Flashcards)
                  .HasForeignKey(e => e.TemaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
