import { useEffect, useState } from 'react'
import { api } from '../api'

export default function LarareList() {
  const [items, setItems] = useState([])
  const [form, setForm] = useState({ Email: '', Firstname: '', Middlename: '', Lastname: '', Kompentens: '' })

  async function load() {
    const data = await api.getLarare()
    setItems(data ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      await api.createLarare(form)
      setForm({ Email: '', Firstname: '', Middlename: '', Lastname: '', Kompentens: '' })
      await load()
    } catch (err) { alert(err.message) }
  }

  return (
    <section>
      <h2>Lärare</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Email" value={form.Email} onChange={e => setForm({ ...form, Email: e.target.value })} />
        <input placeholder="Förnamn" value={form.Firstname} onChange={e => setForm({ ...form, Firstname: e.target.value })} />
        <input placeholder="Efternamn" value={form.Lastname} onChange={e => setForm({ ...form, Lastname: e.target.value })} />
        <input placeholder="Kompetens" value={form.Kompentens} onChange={e => setForm({ ...form, Kompentens: e.target.value })} />
        <button type="submit">Lägg till</button>
      </form>

      <ul>
        {items.map(x => (
          <li key={x.email}>{x.firstname} {x.lastname} — {x.email}</li>
        ))}
      </ul>
    </section>
  )
}
