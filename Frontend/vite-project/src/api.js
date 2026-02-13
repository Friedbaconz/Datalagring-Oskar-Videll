const BASE = import.meta.env.VITE_API_BASE ?? ''

async function request(path, options = {}) {
  const res = await fetch(`${BASE}${path}`, {
    headers: { 'Content-Type': 'application/json' },
    ...options,
  })
  if (!res.ok) {
    const txt = await res.text()
    throw new Error(txt || res.statusText)
  }
  return res.status === 204 ? null : res.json()
}

export const api = {
  // Deltagare
  getDeltagare: () => request('/api/Deltagare'),
  createDeltagare: (body) => request('/api/Deltagare', { method: 'POST', body: JSON.stringify(body) }),
  deleteDeltagare: (id) => request(`/api/Deltagare/${id}`, { method: 'DELETE' }),

  // Larare
  getLarare: () => request('/api/Larare/'),
  createLarare: (body) => request('/api/Larare', { method: 'POST', body: JSON.stringify(body) }),

  // Ort
  getOrt: () => request('/api/Ort/'),
  createOrt: (body) => request('/api/Ort', { method: 'POST', body: JSON.stringify(body) }),

  // Kurs
  getKurs: () => request('/api/Kurs/'),
  createKurs: (body) => request('/api/Kurs', { method: 'POST', body: JSON.stringify(body) }),

  // Kurstillfalle
  getKurstillfalle: () => request('/api/Kurstillfalle/'),
  createKurstillfalle: (body) => request('/api/Kurstillfalle', { method: 'POST', body: JSON.stringify(body) }),

  // KursRegi (register deltagare to kurstillfalle)
  getKursRegi: () => request('/api/KursRegi/'),
  createKursRegi: (body) => request('/api/KursRegi', { method: 'POST', body: JSON.stringify(body) }),
}

export default api
