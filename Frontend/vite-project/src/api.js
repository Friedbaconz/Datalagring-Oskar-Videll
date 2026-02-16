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
  updateDeltagare: (id, body) => request(`/api/Deltagare/${id}`, { method: 'PUT', body: JSON.stringify(body) }),
  deleteDeltagare: (id) => request(`/api/Deltagare/${id}`, { method: 'DELETE' }),

  // Larare
  getLarare: () => request('/api/Larare/'),
  createLarare: (body) => request('/api/Larare', { method: 'POST', body: JSON.stringify(body) }),
  updateLarare: (email, body) => request(`/api/Larare/${email}`, { method: 'PUT', body: JSON.stringify(body) }),
  deleteLarare: (email) => request(`/api/Larare/${email}`, { method: 'DELETE' }),

  // Ort
  getOrt: () => request('/api/Ort/'),
  createOrt: (body) => request('/api/Ort', { method: 'POST', body: JSON.stringify(body) }),
  updateOrt: (id, body) => request(`/api/Ort/${id}`, { method: 'PUT', body: JSON.stringify(body) }),
  deleteOrt: (id) => request(`/api/Ort/${id}`, { method: 'DELETE' }),

  // Kurs
  getKurs: () => request('/api/Kurs/'),
  createKurs: (body) => request('/api/Kurs', { method: 'POST', body: JSON.stringify(body) }),
  updateKurs: (kurskod, body) => request(`/api/Kurs/${kurskod}`, { method: 'PUT', body: JSON.stringify(body) }),
  deleteKurs: (kurskod) => request(`/api/Kurs/${kurskod}`, { method: 'DELETE' }),

  // Kurstillfalle
  getKurstillfalle: () => request('/api/Kurstillfalle/'),
  createKurstillfalle: (body) => request('/api/Kurstillfalle', { method: 'POST', body: JSON.stringify(body) }),
  updateKurstillfalle: (id, body) => request(`/api/Kurstillfalle/${id}`, { method: 'PUT', body: JSON.stringify(body) }),
  deleteKurstillfalle: (id) => request(`/api/Kurstillfalle/${id}`, { method: 'DELETE' }),

  // KursRegi (register deltagare to kurstillfalle)
  getKursRegi: () => request('/api/KursRegi/'),
  createKursRegi: (body) => request('/api/KursRegi', { method: 'POST', body: JSON.stringify(body) }),
  updateKursRegi: (id, body) => request(`/api/KursRegi/${id}`, { method: 'PUT', body: JSON.stringify(body) }),
  deleteKursRegi: (id) => request(`/api/KursRegi/${id}`, { method: 'DELETE' }),

  // LarareRegi (register larare to kurstillfalle)
  getLarareRegi: () => request('/api/LarareRegi/'),
  createLarareRegi: (body) => request('/api/LarareRegi', { method: 'POST', body: JSON.stringify(body) }),
  updateLarareRegi: (id, body) => request(`/api/LarareRegi/${id}`, { method: 'POST', body: JSON.stringify(body) }),
  deleteLarareRegi: (id) => request(`/api/LarareRegi/${id}`, { method: 'DELETE' }),
}

export default api
