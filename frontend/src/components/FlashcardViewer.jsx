import React, { useState } from 'react'
import Flashcard from './Flashcard'
import { approveFlashcard, deleteFlashcard, updateFlashcard } from '../services/api'
import './FlashcardViewer.css'

function FlashcardViewer({ theme, flashcards, onFlashcardsUpdated }) {
  const [showUnapproved, setShowUnapproved] = useState(false)
  const [filterLevel, setFilterLevel] = useState('all')
  const [filterType, setFilterType] = useState('all')
  const [studyMode, setStudyMode] = useState(false)
  const [currentIndex, setCurrentIndex] = useState(0)

  const filteredFlashcards = flashcards.filter(f => {
    if (showUnapproved && f.aprobada) return false
    if (!showUnapproved && !f.aprobada) return false
    if (filterLevel !== 'all' && f.nivel !== filterLevel) return false
    if (filterType !== 'all' && f.tipo !== filterType) return false
    return true
  })

  const unapprovedCount = flashcards.filter(f => !f.aprobada).length

  const handleApprove = async (id) => {
    try {
      await approveFlashcard(id)
      onFlashcardsUpdated()
    } catch (error) {
      console.error('Error approving flashcard:', error)
      alert('Error al aprobar la flashcard')
    }
  }

  const handleDelete = async (id) => {
    if (!confirm('¿Estás seguro de eliminar esta flashcard?')) return
    try {
      await deleteFlashcard(id)
      onFlashcardsUpdated()
    } catch (error) {
      console.error('Error deleting flashcard:', error)
      alert('Error al eliminar la flashcard')
    }
  }

  const handleUpdate = async (id, updatedData) => {
    try {
      await updateFlashcard(id, updatedData)
      onFlashcardsUpdated()
    } catch (error) {
      console.error('Error updating flashcard:', error)
      alert('Error al actualizar la flashcard')
    }
  }

  const nextCard = () => {
    if (currentIndex < filteredFlashcards.length - 1) {
      setCurrentIndex(currentIndex + 1)
    }
  }

  const prevCard = () => {
    if (currentIndex > 0) {
      setCurrentIndex(currentIndex - 1)
    }
  }

  if (studyMode && filteredFlashcards.length > 0) {
    const currentCard = filteredFlashcards[currentIndex]
    return (
      <div className="flashcard-viewer">
        <div className="viewer-header">
          <h2>{theme.name} - Modo Entrevista</h2>
          <button className="btn-secondary" onClick={() => setStudyMode(false)}>
            Salir del Modo Entrevista
          </button>
        </div>

        <div className="study-mode">
          <div className="study-counter">
            {currentIndex + 1} / {filteredFlashcards.length}
          </div>
          <Flashcard 
            flashcard={currentCard}
            onApprove={handleApprove}
            onDelete={handleDelete}
            onUpdate={handleUpdate}
            showActions={false}
          />
          <div className="study-navigation">
            <button 
              className="btn-primary" 
              onClick={prevCard}
              disabled={currentIndex === 0}
            >
              ← Anterior
            </button>
            <button 
              className="btn-primary" 
              onClick={nextCard}
              disabled={currentIndex === filteredFlashcards.length - 1}
            >
              Siguiente →
            </button>
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="flashcard-viewer">
      <div className="viewer-header">
        <h2>{theme.name}</h2>
        <div className="viewer-actions">
          {filteredFlashcards.length > 0 && (
            <button className="btn-primary" onClick={() => setStudyMode(true)}>
              🎯 Modo Entrevista
            </button>
          )}
        </div>
      </div>

      <div className="filters">
        <div className="filter-group">
          <label>
            <input
              type="checkbox"
              checked={showUnapproved}
              onChange={(e) => setShowUnapproved(e.target.checked)}
            />
            Solo no aprobadas {unapprovedCount > 0 && `(${unapprovedCount})`}
          </label>
        </div>

        <div className="filter-group">
          <label>Nivel:</label>
          <select value={filterLevel} onChange={(e) => setFilterLevel(e.target.value)}>
            <option value="all">Todos</option>
            <option value="Junior">Junior</option>
            <option value="Mid">Mid</option>
            <option value="Senior">Senior</option>
          </select>
        </div>

        <div className="filter-group">
          <label>Tipo:</label>
          <select value={filterType} onChange={(e) => setFilterType(e.target.value)}>
            <option value="all">Todos</option>
            <option value="Conceptual">Conceptual</option>
            <option value="Practical">Practical</option>
            <option value="SystemDesign">SystemDesign</option>
            <option value="Tricky">Tricky</option>
          </select>
        </div>
      </div>

      <div className="flashcards-grid">
        {filteredFlashcards.length === 0 ? (
          <div className="empty-state">
            <p>No hay flashcards que coincidan con los filtros seleccionados.</p>
          </div>
        ) : (
          filteredFlashcards.map(flashcard => (
            <Flashcard
              key={flashcard.id}
              flashcard={flashcard}
              onApprove={handleApprove}
              onDelete={handleDelete}
              onUpdate={handleUpdate}
              showActions={true}
            />
          ))
        )}
      </div>
    </div>
  )
}

export default FlashcardViewer
