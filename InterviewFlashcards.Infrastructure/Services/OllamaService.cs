using System.Text;
using System.Text.Json;
using InterviewFlashcards.Application.DTOs;
using InterviewFlashcards.Application.Interfaces;
using InterviewFlashcards.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InterviewFlashcards.Infrastructure.Services;

public class OllamaService : IOllamaService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<OllamaService> _logger;
    private readonly string _ollamaBaseUrl;
    private readonly string _model;

    public OllamaService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<OllamaService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        _ollamaBaseUrl = _configuration["Ollama:BaseUrl"] ?? "http://localhost:11434";
        _model = _configuration["Ollama:Model"] ?? "llama3";
    }

    public async Task<List<FlashcardDto>> GenerateFlashcardsAsync(string tema, string stack, Nivel? nivel = null, int cantidad = 5)
    {
        try
        {
            var nivelTexto = nivel.HasValue ? nivel.Value.ToString() : "cualquier nivel";
            
            var prompt = $@"Actúa como un Tech Lead entrevistando candidatos.

Genera preguntas y respuestas REALISTAS y FRECUENTES de entrevistas técnicas
sobre el siguiente tema:

Tema: {tema}
Stack: {stack}
Nivel: {nivelTexto}

Reglas:
- Evita definiciones básicas.
- Prioriza preguntas de razonamiento y experiencia real.
- Incluye preguntas de seguimiento implícitas.
- Usa ejemplos prácticos cuando sea posible.
- Respuestas claras, concisas y correctas técnicamente.
- Genera exactamente {cantidad} preguntas.

Devuelve ÚNICAMENTE un JSON válido con esta estructura:

[
  {{
    ""question"": """",
    ""answer"": """",
    ""level"": ""Junior | Mid | Senior"",
    ""type"": ""Conceptual | Practical | SystemDesign | Tricky""
  }}
]";

            var requestBody = new
            {
                model = _model,
                prompt = prompt,
                stream = false,
                format = "json"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _logger.LogInformation("Calling Ollama API at {Url}/api/generate", _ollamaBaseUrl);
            
            // Verificar que Ollama esté disponible antes de hacer la llamada
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var healthCheck = await _httpClient.GetAsync($"{_ollamaBaseUrl}/api/tags", cts.Token);
                if (!healthCheck.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Ollama health check returned {StatusCode}", healthCheck.StatusCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw new Exception($"Timeout al conectar con Ollama. Verifica que Ollama esté corriendo en {_ollamaBaseUrl}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"No se puede conectar a Ollama en {_ollamaBaseUrl}. Verifica que Ollama esté corriendo: {ex.Message}", ex);
            }
            
            var response = await _httpClient.PostAsync($"{_ollamaBaseUrl}/api/generate", content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Ollama API error: {StatusCode} - {Error}", response.StatusCode, errorContent);
                throw new Exception($"Error al llamar a Ollama: {response.StatusCode} - {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Ollama response: {Response}", responseContent);

            // Ollama returns a JSON object with a "response" field containing the actual JSON
            var ollamaResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
            
            string jsonContent;
            if (ollamaResponse.TryGetProperty("response", out var responseProperty))
            {
                jsonContent = responseProperty.GetString() ?? string.Empty;
            }
            else
            {
                jsonContent = responseContent;
            }

            // Clean the JSON content (remove markdown code blocks if present)
            jsonContent = jsonContent.Trim();
            if (jsonContent.StartsWith("```json"))
            {
                jsonContent = jsonContent.Substring(7);
            }
            if (jsonContent.StartsWith("```"))
            {
                jsonContent = jsonContent.Substring(3);
            }
            if (jsonContent.EndsWith("```"))
            {
                jsonContent = jsonContent.Substring(0, jsonContent.Length - 3);
            }
            jsonContent = jsonContent.Trim();

            var flashcards = JsonSerializer.Deserialize<List<OllamaFlashcardResponse>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (flashcards == null || flashcards.Count == 0)
            {
                _logger.LogWarning("No flashcards generated from Ollama response");
                return new List<FlashcardDto>();
            }

            return flashcards.Select(f => new FlashcardDto
            {
                Id = Guid.NewGuid(),
                Pregunta = f.Question ?? string.Empty,
                Respuesta = f.Answer ?? string.Empty,
                Nivel = ParseNivel(f.Level),
                Tipo = ParseTipo(f.Type),
                Fuente = FuentePregunta.AI,
                Aprobada = false
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating flashcards with Ollama");
            throw new Exception($"Error al generar flashcards: {ex.Message}", ex);
        }
    }

    private Nivel ParseNivel(string? level)
    {
        if (string.IsNullOrWhiteSpace(level))
            return Nivel.Mid;

        return level.ToLower() switch
        {
            "junior" => Nivel.Junior,
            "mid" => Nivel.Mid,
            "senior" => Nivel.Senior,
            _ => Nivel.Mid
        };
    }

    private TipoPregunta ParseTipo(string? type)
    {
        if (string.IsNullOrWhiteSpace(type))
            return TipoPregunta.Conceptual;

        return type.ToLower() switch
        {
            "conceptual" => TipoPregunta.Conceptual,
            "practical" => TipoPregunta.Practical,
            "systemdesign" => TipoPregunta.SystemDesign,
            "tricky" => TipoPregunta.Tricky,
            _ => TipoPregunta.Conceptual
        };
    }

    private class OllamaFlashcardResponse
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Level { get; set; }
        public string? Type { get; set; }
    }
}
