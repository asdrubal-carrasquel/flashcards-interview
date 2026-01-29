import axios from 'axios'

// Use relative URL to leverage Vite proxy, fallback to absolute URL if needed
const API_BASE_URL = import.meta.env.DEV ? '/api' : 'http://localhost:5001/api'

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 300000, // 5 minutos para generación de flashcards
})

// Themes
export const getThemes = async () => {
  const response = await api.get('/themes')
  return response.data
}

export const createTheme = async (themeData) => {
  const response = await api.post('/themes', themeData)
  return response.data
}

// Flashcards
export const getFlashcards = async (themeId) => {
  const response = await api.get(`/flashcards?themeId=${themeId}`)
  return response.data
}

export const createFlashcard = async (flashcardData) => {
  const response = await api.post('/flashcards', flashcardData)
  return response.data
}

export const generateFlashcards = async (generateData) => {
  const response = await api.post('/flashcards/generate', generateData)
  return response.data
}

export const updateFlashcard = async (id, flashcardData) => {
  const response = await api.put(`/flashcards/${id}`, flashcardData)
  return response.data
}

export const approveFlashcard = async (id) => {
  const response = await api.post(`/flashcards/${id}/approve`)
  return response.data
}

export const deleteFlashcard = async (id) => {
  const response = await api.delete(`/flashcards/${id}`)
  return response.data
}
