# 🎯 Interview Flashcards AI

Una aplicación web completa para crear, generar y estudiar flashcards orientadas exclusivamente a entrevistas técnicas reales. La aplicación utiliza Ollama (LLM local) para generar preguntas realistas que simulan lo que haría un Tech Lead o entrevistador senior.

## 📋 Características

- ✅ **Gestión de Temas**: Crea temas técnicos personalizados (ej: ".NET Senior", "React Avanzado", "AWS")
- 🤖 **Generación Automática con IA**: Genera flashcards usando Ollama (llama3/mistral) con preguntas realistas
- 📚 **Sistema de Flashcards**: Preguntas y respuestas con toggle de visibilidad
- 🎯 **Clasificación**: Niveles (Junior/Mid/Senior) y tipos (Conceptual/Practical/SystemDesign/Tricky)
- ✅ **Revisión y Aprobación**: Revisa, edita, aprueba o descarta flashcards generadas por IA
- 🔍 **Filtros Avanzados**: Filtra por nivel, tipo y estado de aprobación
- 🎓 **Modo Entrevista**: Simula una entrevista real mostrando una pregunta a la vez
- 🎨 **UI Moderna**: Interfaz intuitiva y atractiva con diseño responsivo

## 🏗️ Arquitectura

El proyecto sigue una **Arquitectura Limpia (Clean Architecture)** con separación clara de responsabilidades:

```
InterviewFlashcards/
├── Domain/              # Entidades y interfaces del dominio
│   ├── Entities/        # Theme, Flashcard
│   └── Interfaces/      # IRepository, IThemeRepository, IFlashcardRepository
│
├── Application/         # Lógica de negocio y casos de uso
│   ├── DTOs/           # Data Transfer Objects
│   ├── Interfaces/     # Contratos de servicios
│   └── Services/       # Implementación de servicios de aplicación
│
├── Infrastructure/     # Implementaciones técnicas
│   ├── Data/          # DbContext y configuración de EF Core
│   ├── Repositories/  # Implementación de repositorios
│   └── Services/       # OllamaService (integración con LLM)
│
├── API/               # Capa de presentación (ASP.NET Web API)
│   ├── Controllers/   # Endpoints REST
│   └── Program.cs     # Configuración y startup
│
└── frontend/          # Aplicación React
    ├── src/
    │   ├── components/  # Componentes React
    │   ├── services/    # Cliente API
    │   └── App.jsx      # Componente principal
```

### Decisiones de Arquitectura

1. **Clean Architecture**: Separación en capas (Domain, Application, Infrastructure, API) para mantener el código mantenible y testeable
2. **Repository Pattern**: Abstracción del acceso a datos para facilitar testing y cambios de persistencia
3. **DTOs**: Separación entre entidades de dominio y objetos de transferencia para proteger el dominio
4. **Dependency Injection**: Uso extensivo de DI para desacoplar componentes
5. **EF Core con SQLite**: Base de datos ligera y fácil de configurar para desarrollo local

## 🛠️ Stack Tecnológico

### Backend
- **.NET 8**: Framework principal
- **ASP.NET Web API**: API REST
- **Entity Framework Core**: ORM
- **SQLite**: Base de datos (fácil de cambiar a PostgreSQL)
- **Swagger**: Documentación de API

### Frontend
- **React 18**: Biblioteca UI
- **Vite**: Build tool y dev server
- **Axios**: Cliente HTTP
- **CSS3**: Estilos modernos con gradientes y animaciones

### IA / LLM
- **Ollama**: Runtime local para LLMs
- **Modelo**: llama3 o mistral (configurable)

## 📦 Requisitos Previos

1. **.NET 8 SDK**: [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **Node.js 18+**: [Descargar aquí](https://nodejs.org/)
3. **Ollama**: [Instalar Ollama](https://ollama.ai/)

### Instalación de Ollama

```bash
# Windows (PowerShell)
winget install Ollama.Ollama

# O descargar desde https://ollama.ai/
```

Después de instalar, descarga el modelo:

```bash
ollama pull llama3
# o
ollama pull mistral
```

Verifica que Ollama esté corriendo:

```bash
ollama list
```

## 🚀 Instalación y Ejecución

### 1. Clonar el Repositorio

```bash
git clone <repository-url>
cd flashcards-interview
```

### 2. Configurar el Backend

```bash
cd InterviewFlashcards.API
dotnet restore
dotnet build
```

### 3. Configurar Ollama (si no está en el puerto por defecto)

Edita `InterviewFlashcards.API/appsettings.json`:

```json
{
  "Ollama": {
    "BaseUrl": "http://localhost:11434",
    "Model": "llama3"
  }
}
```

### 4. Ejecutar el Backend

```bash
cd InterviewFlashcards.API
dotnet run
```

El API estará disponible en `http://localhost:5001`
Swagger UI: `http://localhost:5001/swagger`

### 5. Configurar el Frontend

```bash
cd frontend
npm install
```

### 6. Ejecutar el Frontend

```bash
npm run dev
```

El frontend estará disponible en `http://localhost:3000`

## 📖 Uso de la Aplicación

### 1. Crear un Tema

1. Ve a la sección "Temas"
2. Haz clic en "+ Nuevo Tema"
3. Completa:
   - **Nombre**: Ej: ".NET Senior", "React Avanzado"
   - **Descripción**: Descripción opcional del tema
   - **Stack Tecnológico**: Ej: ".NET 8", "React 18", "AWS"

### 2. Generar Flashcards con IA

1. Selecciona un tema
2. Ve a "Generar Flashcards"
3. Configura:
   - **Nivel** (opcional): Junior, Mid, o Senior
   - **Cantidad**: Número de flashcards a generar (1-20)
4. Haz clic en "Generar Flashcards"
5. Las flashcards aparecerán como "no aprobadas"

### 3. Revisar y Aprobar Flashcards

1. Ve a "Estudiar"
2. Activa el filtro "Solo no aprobadas"
3. Revisa cada flashcard:
   - Lee la pregunta
   - Muestra la respuesta
   - **Aprobar**: Si la flashcard es correcta
   - **Editar**: Para modificar pregunta/respuesta
   - **Eliminar**: Si no es útil

### 4. Estudiar Flashcards

1. Selecciona un tema
2. Ve a "Estudiar"
3. Usa los filtros para personalizar tu estudio:
   - **Nivel**: Junior, Mid, Senior
   - **Tipo**: Conceptual, Practical, SystemDesign, Tricky
4. Haz clic en "Modo Entrevista" para simular una entrevista real

### 5. Crear Flashcards Manualmente

1. Ve a "Estudiar"
2. (Próximamente: botón para crear flashcard manual)

## 🔌 API Endpoints

### Temas

- `POST /api/themes` - Crear un tema
- `GET /api/themes` - Obtener todos los temas
- `GET /api/themes/{id}` - Obtener un tema por ID

### Flashcards

- `POST /api/flashcards` - Crear una flashcard manual
- `GET /api/flashcards?themeId={id}` - Obtener flashcards por tema
- `GET /api/flashcards/{id}` - Obtener una flashcard por ID
- `POST /api/flashcards/generate` - Generar flashcards con IA
- `PUT /api/flashcards/{id}` - Actualizar una flashcard
- `POST /api/flashcards/{id}/approve` - Aprobar una flashcard
- `DELETE /api/flashcards/{id}` - Eliminar una flashcard

## 🎨 Características de UI

- **Diseño Responsivo**: Funciona en desktop, tablet y móvil
- **Gradientes Modernos**: Paleta de colores atractiva
- **Animaciones Suaves**: Transiciones y efectos visuales
- **Badges de Clasificación**: Visualización clara de nivel y tipo
- **Modo Oscuro**: (Próximamente)

## 🔧 Configuración Avanzada

### Cambiar el Modelo de Ollama

Edita `appsettings.json`:

```json
{
  "Ollama": {
    "Model": "mistral"  // o cualquier otro modelo disponible
  }
}
```

### Usar PostgreSQL en lugar de SQLite

1. Instala el paquete NuGet:
```bash
dotnet add InterviewFlashcards.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
```

2. Modifica `Program.cs`:
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

3. Actualiza `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=interviewflashcards;Username=postgres;Password=password"
  }
}
```

## 🐛 Solución de Problemas

### Ollama no responde

1. Verifica que Ollama esté corriendo:
```bash
ollama list
```

2. Verifica el puerto en `appsettings.json` (por defecto 11434)

3. Prueba la API de Ollama directamente:
```bash
curl http://localhost:11434/api/tags
```

### Error al generar flashcards

- Verifica que el modelo esté descargado: `ollama list`
- Revisa los logs del backend para ver el error específico
- Asegúrate de que Ollama tenga suficiente memoria RAM

### CORS Errors

- Verifica que el frontend esté en `http://localhost:3000` o `http://localhost:5173`
- Si usas otro puerto, actualiza `Program.cs` en la configuración de CORS

## 📝 Prompt de Generación

El sistema usa este prompt exacto para generar flashcards:

```
Actúa como un Tech Lead entrevistando candidatos.

Genera preguntas y respuestas REALISTAS y FRECUENTES de entrevistas técnicas
sobre el siguiente tema:

Tema: {{TEMA}}
Stack: {{STACK}}
Nivel: {{NIVEL}}

Reglas:
- Evita definiciones básicas.
- Prioriza preguntas de razonamiento y experiencia real.
- Incluye preguntas de seguimiento implícitas.
- Usa ejemplos prácticos cuando sea posible.
- Respuestas claras, concisas y correctas técnicamente.
- Genera exactamente {{CANTIDAD}} preguntas.

Devuelve ÚNICAMENTE un JSON válido con esta estructura:
[
  {
    "question": "",
    "answer": "",
    "level": "Junior | Mid | Senior",
    "type": "Conceptual | Practical | SystemDesign | Tricky"
  }
]
```

## 🚧 Próximas Mejoras

- [ ] Marcar flashcards como difíciles
- [ ] Tracking de progreso de estudio
- [ ] Repetición espaciada (Spaced Repetition)
- [ ] Follow-up automático a partir de una respuesta
- [ ] Exportar flashcards a PDF/JSON
- [ ] Modo oscuro
- [ ] Búsqueda de flashcards
- [ ] Estadísticas de estudio

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👨‍💻 Autor

Desarrollado como proyecto de demostración de arquitectura limpia y integración con LLMs locales.

---

**¡Buena suerte en tus entrevistas técnicas! 🚀**
