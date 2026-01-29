# 🚀 Guía de Inicio Rápido

## Prerrequisitos

1. **.NET 8 SDK** instalado
2. **Node.js 18+** instalado
3. **Ollama** instalado y corriendo

## Instalación Rápida

### 1. Instalar Ollama y el Modelo

```bash
# Instalar Ollama (Windows)
winget install Ollama.Ollama

# Descargar el modelo
ollama pull llama3

# Verificar que funciona
ollama list
```

### 2. Backend

```bash
cd InterviewFlashcards.API
dotnet restore
dotnet run
```

El API estará en: `http://localhost:5001`
Swagger: `http://localhost:5001/swagger`

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

El frontend estará en: `http://localhost:3000`

## Uso Básico

1. **Crear un Tema**: Ve a "Temas" → "+ Nuevo Tema"
2. **Generar Flashcards**: Selecciona un tema → "Generar Flashcards" → Configura y genera
3. **Revisar Flashcards**: Ve a "Estudiar" → Filtra por "Solo no aprobadas" → Revisa y aprueba
4. **Estudiar**: Usa los filtros y el "Modo Entrevista" para practicar

## Solución de Problemas

### Ollama no responde
```bash
# Verificar que Ollama esté corriendo
ollama list

# Si no está corriendo, inicia el servicio
# Windows: El servicio debería iniciarse automáticamente
```

### Error de CORS
- Verifica que el frontend esté en `http://localhost:3000`
- Si usas otro puerto, edita `InterviewFlashcards.API/Program.cs`

### Error al generar flashcards
- Verifica que el modelo esté descargado: `ollama list`
- Revisa los logs del backend
- Asegúrate de tener suficiente RAM (llama3 requiere ~4GB)

## Estructura del Proyecto

```
flashcards-interview/
├── InterviewFlashcards.API/      # Backend API
├── InterviewFlashcards.Application/
├── InterviewFlashcards.Domain/
├── InterviewFlashcards.Infrastructure/
└── frontend/                      # React Frontend
```

¡Listo para usar! 🎉
