import React, { useState } from 'react'
import { generateFlashcards } from '../services/api'
import './FlashcardGenerator.css'

function FlashcardGenerator({ theme, onFlashcardsGenerated }) {
  const [level, setLevel] = useState('')
  const [cantidad, setCantidad] = useState(5)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const [success, setSuccess] = useState(false)

  const handleGenerate = async (e) => {
    e.preventDefault()
    setLoading(true)
    setError(null)
    setSuccess(false)

    try {
      await generateFlashcards({
        temaId: theme.id,
        nivel: level || null,
        cantidad: cantidad
      })
      setSuccess(true)
      onFlashcardsGenerated()
      
      // Reset form after 2 seconds
      setTimeout(() => {
        setSuccess(false)
        setLevel('')
        setCantidad(5)
      }, 2000)
    } catch (err) {
      console.error('Error generating flashcards:', err)
      if (err.code === 'ECONNABORTED') {
        setError('⏱️ La generación está tardando demasiado. Verifica que Ollama esté corriendo y que el modelo esté disponible.')
      } else if (err.response?.status === 400) {
        setError(err.response?.data?.error || 'Error al generar flashcards. Verifica los logs del backend.')
      } else if (err.code === 'ERR_NETWORK' || err.message?.includes('Network Error')) {
        setError('🔌 Error de conexión. Verifica que el backend esté corriendo.')
      } else {
        setError(err.response?.data?.error || err.message || 'Error al generar flashcards. Asegúrate de que Ollama esté corriendo en http://localhost:11434')
      }
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="flashcard-generator">
      <div className="generator-header">
        <h2>Generar Flashcards con IA</h2>
        <p className="generator-subtitle">
          Genera preguntas realistas de entrevistas técnicas usando Ollama
        </p>
      </div>

      <div className="generator-info">
        <div className="info-card">
          <h3>📋 Tema Seleccionado</h3>
          <p><strong>Nombre:</strong> {theme.name}</p>
          <p><strong>Stack:</strong> {theme.stackTecnologico}</p>
        </div>
      </div>

      <form className="generator-form" onSubmit={handleGenerate}>
        <div className="form-group">
          <label>Nivel (Opcional)</label>
          <select
            value={level}
            onChange={(e) => setLevel(e.target.value)}
          >
            <option value="">Cualquier nivel</option>
            <option value="Junior">Junior</option>
            <option value="Mid">Mid</option>
            <option value="Senior">Senior</option>
          </select>
          <small>Si no seleccionas un nivel, se generarán preguntas de todos los niveles</small>
        </div>

        <div className="form-group">
          <label>Cantidad de Flashcards</label>
          <input
            type="number"
            min="1"
            max="20"
            value={cantidad}
            onChange={(e) => setCantidad(parseInt(e.target.value) || 1)}
          />
          <small>Mínimo: 1, Máximo: 20</small>
        </div>

        {error && (
          <div className="error-message">
            ⚠️ {error}
          </div>
        )}

        {success && (
          <div className="success-message">
            ✅ Flashcards generadas exitosamente. Revisa la sección "Estudiar" para aprobarlas.
          </div>
        )}

        <button 
          type="submit" 
          className="btn-generate" 
          disabled={loading}
        >
          {loading ? '🔄 Generando...' : '🚀 Generar Flashcards'}
        </button>
      </form>

      <div className="generator-tips">
        <h3>💡 Consejos</h3>
        <ul>
          <li>Asegúrate de que Ollama esté corriendo en <code>http://localhost:11434</code></li>
          <li>El modelo por defecto es <code>llama3</code>. Puedes cambiarlo en <code>appsettings.json</code></li>
          <li>Las flashcards generadas aparecerán como "no aprobadas" para que puedas revisarlas</li>
          <li>Puedes editar, aprobar o eliminar cualquier flashcard generada</li>
        </ul>
      </div>
    </div>
  )
}

export default FlashcardGenerator
