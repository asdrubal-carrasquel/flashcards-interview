import React, { useState } from 'react'
import { createTheme } from '../services/api'
import './ThemeManager.css'

function ThemeManager({ themes, onThemeCreated, onThemeSelected }) {
  const [showForm, setShowForm] = useState(false)
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    stackTecnologico: ''
  })
  const [loading, setLoading] = useState(false)

  const handleSubmit = async (e) => {
    e.preventDefault()
    setLoading(true)
    try {
      await createTheme(formData)
      setFormData({ name: '', description: '', stackTecnologico: '' })
      setShowForm(false)
      onThemeCreated()
    } catch (error) {
      console.error('Error creating theme:', error)
      alert('Error al crear el tema: ' + (error.response?.data?.error || error.message))
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="theme-manager">
      <div className="theme-manager-header">
        <h2>Temas Técnicos</h2>
        <button 
          className="btn-primary" 
          onClick={() => setShowForm(!showForm)}
        >
          {showForm ? 'Cancelar' : '+ Nuevo Tema'}
        </button>
      </div>

      {showForm && (
        <form className="theme-form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Nombre del Tema *</label>
            <input
              type="text"
              value={formData.name}
              onChange={(e) => setFormData({ ...formData, name: e.target.value })}
              placeholder="Ej: .NET Senior, React Avanzado, AWS"
              required
            />
          </div>

          <div className="form-group">
            <label>Descripción</label>
            <textarea
              value={formData.description}
              onChange={(e) => setFormData({ ...formData, description: e.target.value })}
              placeholder="Descripción del tema..."
              rows="3"
            />
          </div>

          <div className="form-group">
            <label>Stack Tecnológico *</label>
            <input
              type="text"
              value={formData.stackTecnologico}
              onChange={(e) => setFormData({ ...formData, stackTecnologico: e.target.value })}
              placeholder="Ej: .NET 8, React 18, AWS, PostgreSQL"
              required
            />
          </div>

          <button type="submit" className="btn-primary" disabled={loading}>
            {loading ? 'Creando...' : 'Crear Tema'}
          </button>
        </form>
      )}

      <div className="themes-grid">
        {themes.length === 0 ? (
          <div className="empty-state">
            <p>No hay temas creados aún. Crea tu primer tema para comenzar.</p>
          </div>
        ) : (
          themes.map(theme => (
            <div 
              key={theme.id} 
              className="theme-card"
              onClick={() => onThemeSelected(theme)}
            >
              <h3>{theme.name}</h3>
              <p className="theme-stack">{theme.stackTecnologico}</p>
              {theme.description && (
                <p className="theme-description">{theme.description}</p>
              )}
              <div className="theme-footer">
                <span className="theme-date">
                  Creado: {new Date(theme.createdAt).toLocaleDateString()}
                </span>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  )
}

export default ThemeManager
