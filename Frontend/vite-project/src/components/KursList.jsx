import { useEffect, useState } from 'react'
import { api } from '../api'

export default function KursList() {
  const [items, setItems] = useState([])
  const [form, setForm] = useState({ Kurskod: '', KursNamn: '', Description: '' })

  async function load() {
    const data = await api.getKurs()
    setItems(data ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      await api.createKurs({ Kurskod: form.Kurskod, KursNamn: form.KursNamn, Description: form.Description })
      setForm({ Kurskod: '', KursNamn: '', Description: '' })
      await load()
    } catch (err) { alert(err.message) }
  }

  return (
    <section>
      <h2>Kurs</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Kurskod" value={form.Kurskod} onChange={e => setForm({ ...form, Kurskod: e.target.value })} />
        <input placeholder="Kursnamn" value={form.KursNamn} onChange={e => setForm({ ...form, KursNamn: e.target.value })} />
        <input placeholder="Description" value={form.Description} onChange={e => setForm({ ...form, Description: e.target.value })} />
        <button type="submit">Skapa kurs</button>
      </form>

      <ul>
        {items.map(k => (
          <li key={k.kurskod ?? k.Kurskod}>{k.kursNamn ?? k.KursNamn} — {k.kurskod ?? k.Kurskod}</li>
        ))}
      </ul>
    </section>
  )
}
