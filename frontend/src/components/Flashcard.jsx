import React, { useState } from 'react'
import './Flashcard.css'

function Flashcard({ flashcard, onApprove, onDelete, onUpdate, showActions }) {
  const [showAnswer, setShowAnswer] = useState(false)
  const [isEditing, setIsEditing] = useState(false)
  const [editData, setEditData] = useState({
    pregunta: flashcard.pregunta,
    respuesta: flashcard.respuesta,
    nivel: flashcard.nivel,
    tipo: flashcard.tipo
  })

  const handleSave = () => {
    onUpdate(flashcard.id, {
      ...flashcard,
      ...editData
    })
    setIsEditing(false)
  }

  const getLevelColor = (level) => {
    switch (level) {
      case 'Junior': return '#4caf50'
      case 'Mid': return '#ff9800'
      case 'Senior': return '#f44336'
      default: return '#999'
    }
  }

  const getTypeColor = (type) => {
    switch (type) {
      case 'Conceptual': return '#2196f3'
      case 'Practical': return '#9c27b0'
      case 'SystemDesign': return '#ff5722'
      case 'Tricky': return '#e91e63'
      default: return '#999'
    }
  }

  if (isEditing) {
    return (
      <div className="flashcard editing">
        <div className="flashcard-header">
          <span 
            className="badge level" 
            style={{ backgroundColor: getLevelColor(editData.nivel) }}
          >
            {editData.nivel}
          </span>
          <span 
            className="badge type" 
            style={{ backgroundColor: getTypeColor(editData.tipo) }}
          >
            {editData.tipo}
          </span>
        </div>

        <div className="flashcard-content">
          <div className="form-group">
            <label>Pregunta:</label>
            <textarea
              value={editData.pregunta}
              onChange={(e) => setEditData({ ...editData, pregunta: e.target.value })}
              rows="3"
            />
          </div>

          <div className="form-group">
            <label>Respuesta:</label>
            <textarea
              value={editData.respuesta}
              onChange={(e) => setEditData({ ...editData, respuesta: e.target.value })}
              rows="5"
            />
          </div>

          <div className="form-group">
            <label>Nivel:</label>
            <select
              value={editData.nivel}
              onChange={(e) => setEditData({ ...editData, nivel: e.target.value })}
            >
              <option value="Junior">Junior</option>
              <option value="Mid">Mid</option>
              <option value="Senior">Senior</option>
            </select>
          </div>

          <div className="form-group">
            <label>Tipo:</label>
            <select
              value={editData.tipo}
              onChange={(e) => setEditData({ ...editData, tipo: e.target.value })}
            >
              <option value="Conceptual">Conceptual</option>
              <option value="Practical">Practical</option>
              <option value="SystemDesign">SystemDesign</option>
              <option value="Tricky">Tricky</option>
            </select>
          </div>
        </div>

        <div className="flashcard-actions">
          <button className="btn-save" onClick={handleSave}>
            Guardar
          </button>
          <button className="btn-cancel" onClick={() => setIsEditing(false)}>
            Cancelar
          </button>
        </div>
      </div>
    )
  }

  return (
    <div className={`flashcard ${!flashcard.aprobada ? 'unapproved' : ''}`}>
      <div className="flashcard-header">
        <div className="badges">
          <span 
            className="badge level" 
            style={{ backgroundColor: getLevelColor(flashcard.nivel) }}
          >
            {flashcard.nivel}
          </span>
          <span 
            className="badge type" 
            style={{ backgroundColor: getTypeColor(flashcard.tipo) }}
          >
            {flashcard.tipo}
          </span>
          {flashcard.fuente === 'AI' && (
            <span className="badge ai">🤖 AI</span>
          )}
        </div>
        {!flashcard.aprobada && (
          <span className="unapproved-label">Pendiente de aprobación</span>
        )}
      </div>

      <div className="flashcard-content">
        <div className="flashcard-question">
          <h3>❓ Pregunta</h3>
          <p>{flashcard.pregunta}</p>
        </div>

        <button
          className="toggle-answer-btn"
          onClick={() => setShowAnswer(!showAnswer)}
        >
          {showAnswer ? '👁️ Ocultar Respuesta' : '👁️‍🗨️ Mostrar Respuesta'}
        </button>

        {showAnswer && (
          <div className="flashcard-answer">
            <h3>💡 Respuesta</h3>
            <p>{flashcard.respuesta}</p>
          </div>
        )}
      </div>

      {showActions && (
        <div className="flashcard-actions">
          {!flashcard.aprobada && (
            <button 
              className="btn-approve" 
              onClick={() => onApprove(flashcard.id)}
            >
              ✓ Aprobar
            </button>
          )}
          <button 
            className="btn-edit" 
            onClick={() => setIsEditing(true)}
          >
            ✏️ Editar
          </button>
          <button 
            className="btn-delete" 
            onClick={() => onDelete(flashcard.id)}
          >
            🗑️ Eliminar
          </button>
        </div>
      )}
    </div>
  )
}

export default Flashcard
