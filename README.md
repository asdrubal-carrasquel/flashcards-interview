# 🎯 Interview Flashcards AI

Una aplicación web completa para crear, generar y estudiar flashcards orientadas exclusivamente a entrevistas técnicas reales. La aplicación utiliza Ollama (LLM local) para generar preguntas realistas que simulan lo que haría un Tech Lead o entrevistador senior.

## 📋 Tabla de Contenidos

- [Problema y Solución](#-problema-y-solución)
- [Características](#-características)
- [Arquitectura](#️-arquitectura)
- [Stack Tecnológico](#️-stack-tecnológico)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación y Ejecución](#-instalación-y-ejecución)
- [Uso de la Aplicación](#-uso-de-la-aplicación)
- [API Endpoints](#-api-endpoints)
- [Configuración Avanzada](#️-configuración-avanzada)
- [Solución de Problemas](#-solución-de-problemas)
- [Arquitectura Técnica Detallada](#-arquitectura-técnica-detallada)

## 🎯 Problema y Solución

### El Problema

Prepararse para entrevistas técnicas es desafiante porque:
- No sabes qué preguntas te harán
- Las preguntas en internet suelen ser genéricas o académicas
- Es difícil encontrar preguntas que reflejen lo que realmente pregunta un Tech Lead
- Las APIs de IA suelen ser costosas o tienen límites

### La Solución

**Interview Flashcards AI** resuelve esto generando preguntas realistas usando IA local (Ollama):
- ✅ **Gratis y Local**: No requiere APIs pagadas, todo corre localmente
- ✅ **Preguntas Realistas**: Genera preguntas como las haría un entrevistador senior
- ✅ **Personalizable**: Crea temas para cualquier stack tecnológico
- ✅ **Revisión Manual**: Puedes revisar, editar y aprobar cada flashcard generada
- ✅ **Modo Entrevista**: Simula una entrevista real para practicar

## ✨ Características

- ✅ **Gestión de Temas**: Crea temas técnicos personalizados (ej: ".NET Senior", "React Avanzado", "AWS")
- 🤖 **Generación Automática con IA**: Genera flashcards usando Ollama (llama3/mistral) con preguntas realistas
- 📚 **Sistema de Flashcards**: Preguntas y respuestas con toggle de visibilidad
- 🎯 **Clasificación**: Niveles (Junior/Mid/Senior) y tipos (Conceptual/Practical/SystemDesign/Tricky)
- ✅ **Revisión y Aprobación**: Revisa, edita, aprueba o descarta flashcards generadas por IA
- 🔍 **Filtros Avanzados**: Filtra por nivel, tipo y estado de aprobación
- 🎓 **Modo Entrevista**: Simula una entrevista real mostrando una pregunta a la vez
- 🎨 **UI Moderna**: Interfaz intuitiva y atractiva con diseño responsivo
- 💾 **Persistencia Local**: Base de datos SQLite para almacenar tus flashcards

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

### Software Necesario

1. **.NET 8 SDK**: [Descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0)
   ```bash
   # Verificar instalación
   dotnet --version
   # Debe mostrar: 8.0.x o superior
   ```

2. **Node.js 18+**: [Descargar aquí](https://nodejs.org/)
   ```bash
   # Verificar instalación
   node --version
   # Debe mostrar: v18.x.x o superior
   npm --version
   ```

3. **Ollama**: [Instalar Ollama](https://ollama.ai/)

### Instalación de Ollama

#### Windows
```bash
# Opción 1: Usando winget
winget install Ollama.Ollama

# Opción 2: Descargar desde https://ollama.ai/
```

#### Linux/macOS
```bash
# Instalar desde https://ollama.ai/
# O usar el script de instalación
curl -fsSL https://ollama.ai/install.sh | sh
```

#### Verificar Instalación de Ollama

```bash
# Verificar que Ollama esté instalado
ollama --version

# Descargar el modelo (llama3 requiere ~4GB de RAM)
ollama pull llama3

# O usar mistral (más ligero, ~4GB)
ollama pull mistral

# Verificar modelos disponibles
ollama list
```

**Nota**: El primer modelo puede tardar varios minutos en descargarse dependiendo de tu conexión.

## 🚀 Instalación y Ejecución

### Paso 1: Clonar el Repositorio

```bash
git clone <repository-url>
cd flashcards-interview
```

### Paso 2: Configurar el Backend

```bash
# Navegar a la carpeta del API
cd InterviewFlashcards.API

# Restaurar dependencias NuGet
dotnet restore

# Compilar el proyecto
dotnet build

# Verificar que compile sin errores
```

### Paso 3: Configurar Ollama (Opcional)

Si Ollama no está en el puerto por defecto o quieres usar otro modelo, edita `InterviewFlashcards.API/appsettings.json`:

```json
{
  "Ollama": {
    "BaseUrl": "http://localhost:11434",
    "Model": "llama3"
  }
}
```

**Modelos disponibles**:
- `llama3` (recomendado, ~4GB RAM)
- `mistral` (alternativa, ~4GB RAM)
- `llama3.2` (más pequeño, ~2GB RAM)

### Paso 4: Ejecutar el Backend

```bash
# Desde la carpeta InterviewFlashcards.API
dotnet run
```

**Verificación**:
- Deberías ver: `Now listening on: http://localhost:5001`
- Abre tu navegador en: `http://localhost:5001/swagger`
- Deberías ver la documentación de Swagger

**Si el puerto 5001 está ocupado**:
- El sistema intentará usar otro puerto
- Revisa la consola para ver qué puerto se asignó
- Actualiza `frontend/vite.config.js` y `frontend/src/services/api.js` con el nuevo puerto

### Paso 5: Configurar el Frontend

Abre una **nueva terminal** (mantén el backend corriendo):

```bash
# Navegar a la carpeta del frontend
cd frontend

# Instalar dependencias npm
npm install

# Verificar que se instalaron correctamente
# Deberías ver node_modules/ creado
```

### Paso 6: Ejecutar el Frontend

```bash
# Desde la carpeta frontend
npm run dev
```

**Verificación**:
- Deberías ver: `Local: http://localhost:3000`
- Abre tu navegador en: `http://localhost:3000`
- Deberías ver la interfaz de la aplicación

### Paso 7: Verificar que Todo Funciona

1. **Backend**: `http://localhost:5001/swagger` debe mostrar la API
2. **Frontend**: `http://localhost:3000` debe mostrar la aplicación
3. **Ollama**: Ejecuta `ollama list` para verificar modelos disponibles

## 📖 Uso de la Aplicación

### Flujo Completo de Uso

#### 1. Crear un Tema

1. Abre la aplicación en `http://localhost:3000`
2. Ve a la sección **"Temas"**
3. Haz clic en **"+ Nuevo Tema"**
4. Completa el formulario:
   - **Nombre**: Ej: ".NET Senior", "React Avanzado", "AWS Architecture"
   - **Descripción**: Descripción opcional del tema (ej: "Preguntas sobre .NET 8, C# avanzado, Entity Framework")
   - **Stack Tecnológico**: Ej: ".NET 8", "React 18", "AWS, PostgreSQL"
5. Haz clic en **"Crear Tema"**

#### 2. Generar Flashcards con IA

1. Selecciona un tema de la lista (haz clic en la tarjeta del tema)
2. Ve a la pestaña **"Generar Flashcards"**
3. Configura los parámetros:
   - **Nivel** (opcional): 
     - Junior: Preguntas básicas
     - Mid: Preguntas intermedias
     - Senior: Preguntas avanzadas
     - Vacío: Mezcla de todos los niveles
   - **Cantidad**: Número de flashcards a generar (1-20, recomendado: 5-10)
4. Haz clic en **"🚀 Generar Flashcards"**
5. Espera a que se generen (puede tardar 30-60 segundos dependiendo del modelo)
6. Las flashcards aparecerán automáticamente como **"no aprobadas"**

**Nota**: La primera generación puede tardar más si Ollama necesita cargar el modelo.

#### 3. Revisar y Aprobar Flashcards

1. Ve a la pestaña **"Estudiar"**
2. Activa el filtro **"Solo no aprobadas"** para ver solo las generadas
3. Para cada flashcard:
   - Lee la pregunta
   - Haz clic en **"👁️‍🗨️ Mostrar Respuesta"** para ver la respuesta
   - **Aprobar** (✓): Si la flashcard es correcta y útil
   - **Editar** (✏️): Para modificar pregunta/respuesta/nivel/tipo
   - **Eliminar** (🗑️): Si no es útil o está incorrecta
4. Las flashcards aprobadas aparecerán en el estudio normal

#### 4. Estudiar Flashcards

1. Selecciona un tema
2. Ve a **"Estudiar"**
3. Usa los filtros para personalizar tu estudio:
   - **Nivel**: Junior, Mid, Senior
   - **Tipo**: Conceptual, Practical, SystemDesign, Tricky
   - **Solo no aprobadas**: Para revisar flashcards pendientes
4. Para cada flashcard:
   - Lee la pregunta
   - Intenta responder mentalmente
   - Muestra la respuesta para verificar
5. Usa **"🎯 Modo Entrevista"** para simular una entrevista real (una pregunta a la vez)

#### 5. Crear Flashcards Manualmente

Actualmente, las flashcards manuales se pueden crear a través de la API. Próximamente se agregará un formulario en la UI.

## 🔌 API Endpoints

### Temas

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `POST` | `/api/themes` | Crear un tema |
| `GET` | `/api/themes` | Obtener todos los temas |
| `GET` | `/api/themes/{id}` | Obtener un tema por ID |

**Ejemplo de creación**:
```json
POST /api/themes
{
  "name": ".NET Senior",
  "description": "Preguntas avanzadas sobre .NET",
  "stackTecnologico": ".NET 8, C#, Entity Framework"
}
```

### Flashcards

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `POST` | `/api/flashcards` | Crear una flashcard manual |
| `GET` | `/api/flashcards?themeId={id}` | Obtener flashcards por tema |
| `GET` | `/api/flashcards/{id}` | Obtener una flashcard por ID |
| `POST` | `/api/flashcards/generate` | Generar flashcards con IA |
| `PUT` | `/api/flashcards/{id}` | Actualizar una flashcard |
| `POST` | `/api/flashcards/{id}/approve` | Aprobar una flashcard |
| `DELETE` | `/api/flashcards/{id}` | Eliminar una flashcard |

**Ejemplo de generación**:
```json
POST /api/flashcards/generate
{
  "temaId": "guid-del-tema",
  "nivel": "Senior",  // Opcional: "Junior", "Mid", "Senior" o null
  "cantidad": 5
}
```

### Documentación Completa

Visita `http://localhost:5001/swagger` para ver la documentación interactiva de la API.

## 🔧 Configuración Avanzada

### Cambiar el Modelo de Ollama

Edita `InterviewFlashcards.API/appsettings.json`:

```json
{
  "Ollama": {
    "BaseUrl": "http://localhost:11434",
    "Model": "mistral"  // Cambia a "llama3", "mistral", "llama3.2", etc.
  }
}
```

**Modelos recomendados**:
- `llama3`: Mejor calidad, requiere ~4GB RAM
- `mistral`: Buena calidad, requiere ~4GB RAM
- `llama3.2`: Más ligero, requiere ~2GB RAM

### Cambiar el Puerto del Backend

Edita `InterviewFlashcards.API/Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5002"  // Cambia el puerto
    }
  }
}
```

Luego actualiza:
- `frontend/vite.config.js`: Cambia `target: 'http://127.0.0.1:5002'`
- `frontend/src/services/api.js`: Cambia la URL de producción

### Usar PostgreSQL en lugar de SQLite

1. **Instala el paquete NuGet**:
```bash
cd InterviewFlashcards.Infrastructure
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

2. **Modifica `InterviewFlashcards.API/Program.cs`**:
```csharp
// Reemplaza UseSqlite con UseNpgsql
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

3. **Actualiza `appsettings.json`**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=interviewflashcards;Username=postgres;Password=tu_password"
  }
}
```

4. **Crea la base de datos**:
```bash
# Ejecuta migraciones (si las creas) o usa EnsureCreated
dotnet run --project InterviewFlashcards.API
```

### Configurar Timeout de Ollama

Si las generaciones tardan mucho, puedes aumentar el timeout en `InterviewFlashcards.API/Program.cs`:

```csharp
builder.Services.AddHttpClient<IOllamaService, OllamaService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(10); // Aumenta a 10 minutos
});
```

## 🐛 Solución de Problemas

### Error: "Failed to bind to address http://[::1]:5001: address already in use"

**Causa**: El puerto 5001 está siendo usado por otro proceso.

**Solución**:
1. Cambia el puerto en `launchSettings.json` (ver sección de configuración avanzada)
2. O identifica y cierra el proceso:
```bash
# Windows
netstat -ano | findstr :5001
taskkill /PID <numero_pid> /F
```

### Error: "Ollama no responde" o "ECONNREFUSED"

**Causa**: Ollama no está corriendo o no está en el puerto correcto.

**Solución**:
1. Verifica que Ollama esté corriendo:
```bash
ollama list
# Si no funciona, inicia Ollama manualmente
```

2. Verifica el puerto en `appsettings.json` (por defecto 11434)

3. Prueba la API de Ollama directamente:
```bash
# Windows PowerShell
curl http://localhost:11434/api/tags

# O en el navegador
http://localhost:11434/api/tags
```

4. Si Ollama no inicia automáticamente:
   - Windows: Busca "Ollama" en el menú inicio y ábrelo
   - Linux/macOS: Ejecuta `ollama serve` en una terminal

### Error: "Error al generar flashcards" - Se queda en "Generando..."

**Causa**: Ollama está tardando mucho o hay un error en la generación.

**Solución**:
1. **Verifica que el modelo esté descargado**:
```bash
ollama list
# Debe mostrar llama3 o el modelo configurado
```

2. **Revisa los logs del backend**:
   - Busca mensajes como "Calling Ollama API"
   - Si hay errores, aparecerán en la consola

3. **Verifica la memoria RAM**:
   - llama3 requiere ~4GB de RAM disponible
   - Si no tienes suficiente, usa `llama3.2` (más ligero)

4. **Prueba con menos flashcards**:
   - Intenta generar 1-2 flashcards primero
   - Si funciona, el problema puede ser la cantidad

5. **Verifica que Ollama responda**:
```bash
# Prueba generar algo directamente con Ollama
ollama run llama3 "Genera una pregunta de entrevista sobre .NET"
```

### Error: CORS en el navegador

**Causa**: El frontend está intentando acceder al backend desde un origen no permitido.

**Solución**:
1. Verifica que el frontend esté en `http://localhost:3000`
2. Si usas otro puerto, actualiza `InterviewFlashcards.API/Program.cs`:
```csharp
policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "http://localhost:TU_PUERTO")
```

### Error: "Cannot find module" en el frontend

**Causa**: Las dependencias de npm no están instaladas.

**Solución**:
```bash
cd frontend
rm -rf node_modules package-lock.json  # Linux/macOS
# O en Windows PowerShell:
Remove-Item -Recurse -Force node_modules, package-lock.json

npm install
```

### La base de datos no se crea

**Causa**: Problemas con permisos o la ruta de la base de datos.

**Solución**:
1. Verifica que la carpeta del proyecto tenga permisos de escritura
2. La base de datos se crea automáticamente en `InterviewFlashcards.API/interviewflashcards.db`
3. Si hay problemas, elimina el archivo `.db` y vuelve a ejecutar el backend

## 🏛️ Arquitectura Técnica Detallada

### Flujo de Datos

```
Frontend (React)
    ↓ HTTP Request
API Controller
    ↓ Service Layer
Application Service
    ↓ Repository Pattern
Infrastructure Repository
    ↓ EF Core
SQLite Database

Para Generación:
Application Service
    ↓ OllamaService
Ollama API (Local)
    ↓ JSON Response
Application Service
    ↓ Save to Database
SQLite Database
```

### Capas de la Aplicación

#### Domain Layer
- **Responsabilidad**: Define las entidades del negocio y sus contratos
- **Contiene**: 
  - `Theme`: Entidad de tema técnico
  - `Flashcard`: Entidad de flashcard
  - Interfaces de repositorios
- **No depende de**: Ninguna otra capa

#### Application Layer
- **Responsabilidad**: Lógica de negocio y casos de uso
- **Contiene**:
  - DTOs para transferencia de datos
  - Interfaces de servicios
  - Implementación de servicios de aplicación
- **Depende de**: Domain Layer

#### Infrastructure Layer
- **Responsabilidad**: Implementaciones técnicas
- **Contiene**:
  - Repositorios (acceso a datos)
  - DbContext (EF Core)
  - OllamaService (integración con LLM)
- **Depende de**: Domain y Application Layers

#### API Layer
- **Responsabilidad**: Punto de entrada HTTP
- **Contiene**:
  - Controllers (endpoints REST)
  - Configuración (Program.cs)
  - Middleware (CORS, Swagger)
- **Depende de**: Application Layer

### Integración con Ollama

El servicio `OllamaService` se comunica con Ollama usando su API REST:

1. **Construcción del Prompt**: Se genera un prompt específico basado en el tema, stack y nivel
2. **Llamada HTTP**: POST a `http://localhost:11434/api/generate`
3. **Procesamiento**: Se parsea la respuesta JSON y se limpia (remueve markdown si existe)
4. **Validación**: Se valida que la respuesta tenga el formato correcto
5. **Mapeo**: Se mapean las flashcards generadas a DTOs
6. **Persistencia**: Se guardan en la base de datos como "no aprobadas"

### Prompt de Generación

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

### Modelo de Datos

#### Theme (Tema)
```csharp
- Id: Guid
- Name: string (único)
- Description: string
- StackTecnologico: string
- CreatedAt: DateTime
- Flashcards: ICollection<Flashcard>
```

#### Flashcard
```csharp
- Id: Guid
- TemaId: Guid (FK a Theme)
- Pregunta: string
- Respuesta: string
- Nivel: enum (Junior, Mid, Senior)
- Tipo: enum (Conceptual, Practical, SystemDesign, Tricky)
- Fuente: enum (AI, Manual)
- Aprobada: bool
- CreatedAt: DateTime
- Theme: Navigation Property
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
- [ ] Crear flashcards manualmente desde la UI
- [ ] Compartir temas con otros usuarios
- [ ] Importar/exportar temas

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👨‍💻 Contribuciones

Las contribuciones son bienvenidas. Por favor:
1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 🙏 Agradecimientos

- [Ollama](https://ollama.ai/) por proporcionar una forma fácil de ejecutar LLMs localmente
- La comunidad de .NET y React por las excelentes herramientas

---

**¡Buena suerte en tus entrevistas técnicas! 🚀**

Si encuentras algún problema o tienes sugerencias, por favor abre un issue en el repositorio.
