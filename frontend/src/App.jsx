import React, { useState, useEffect } from 'react'
import './App.css'
import ThemeManager from './components/ThemeManager'
import FlashcardViewer from './components/FlashcardViewer'
import FlashcardGenerator from './components/FlashcardGenerator'
import { getThemes, getFlashcards } from './services/api'

function App() {
  const [themes, setThemes] = useState([])
  const [selectedTheme, setSelectedTheme] = useState(null)
  const [flashcards, setFlashcards] = useState([])
  const [view, setView] = useState('themes') // 'themes', 'study', 'generate'
  const [error, setError] = useState(null)

  useEffect(() => {
    loadThemes()
  }, [])

  useEffect(() => {
    if (selectedTheme) {
      loadFlashcards(selectedTheme.id)
    }
  }, [selectedTheme])

  const loadThemes = async () => {
    try {
      setError(null)
      const data = await getThemes()
      setThemes(data)
    } catch (error) {
      console.error('Error loading themes:', error)
      if (error.response?.status === 404 || error.code === 'ERR_NETWORK') {
        setError('⚠️ Backend no encontrado. Asegúrate de que el backend esté corriendo en http://localhost:5001')
      } else {
        setError('Error al cargar los temas: ' + (error.message || 'Error desconocido'))
      }
    }
  }

  const loadFlashcards = async (themeId) => {
    try {
      const data = await getFlashcards(themeId)
      setFlashcards(data)
    } catch (error) {
      console.error('Error loading flashcards:', error)
    }
  }

  const handleThemeCreated = () => {
    loadThemes()
  }

  const handleThemeSelected = (theme) => {
    setSelectedTheme(theme)
    setView('study')
  }

  const handleFlashcardsUpdated = () => {
    if (selectedTheme) {
      loadFlashcards(selectedTheme.id)
    }
  }

  return (
    <div className="app">
      <header className="app-header">
        <h1>🎯 Interview Flashcards AI</h1>
        <p>Prepara tus entrevistas técnicas con preguntas realistas generadas por IA</p>
      </header>

      <nav className="app-nav">
        <button 
          className={view === 'themes' ? 'active' : ''} 
          onClick={() => setView('themes')}
        >
          Temas
        </button>
        {selectedTheme && (
          <>
            <button 
              className={view === 'study' ? 'active' : ''} 
              onClick={() => setView('study')}
            >
              Estudiar
            </button>
            <button 
              className={view === 'generate' ? 'active' : ''} 
              onClick={() => setView('generate')}
            >
              Generar Flashcards
            </button>
          </>
        )}
      </nav>

      <main className="app-main">
        {error && (
          <div className="error-banner">
            <p>{error}</p>
            <button onClick={() => setError(null)}>✕</button>
          </div>
        )}
        
        {view === 'themes' && (
          <ThemeManager 
            themes={themes}
            onThemeCreated={handleThemeCreated}
            onThemeSelected={handleThemeSelected}
          />
        )}

        {view === 'study' && selectedTheme && (
          <FlashcardViewer 
            theme={selectedTheme}
            flashcards={flashcards}
            onFlashcardsUpdated={handleFlashcardsUpdated}
          />
        )}

        {view === 'generate' && selectedTheme && (
          <FlashcardGenerator 
            theme={selectedTheme}
            onFlashcardsGenerated={handleFlashcardsUpdated}
          />
        )}
      </main>
    </div>
  )
}

export default App
