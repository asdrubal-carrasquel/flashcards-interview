using InterviewFlashcards.Application.Interfaces;
using InterviewFlashcards.Application.Services;
using InterviewFlashcards.Domain.Interfaces;
using InterviewFlashcards.Infrastructure.Data;
using InterviewFlashcards.Infrastructure.Repositories;
using InterviewFlashcards.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                     "Data Source=interviewflashcards.db"));

// Repositories
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();

// Services
builder.Services.AddScoped<IThemeService, ThemeService>();
builder.Services.AddScoped<IFlashcardService, FlashcardService>();
builder.Services.AddHttpClient<IOllamaService, OllamaService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(5); // Timeout de 5 minutos para generación de flashcards
});
builder.Services.AddScoped<IOllamaService, OllamaService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
