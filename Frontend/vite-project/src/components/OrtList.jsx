import { useEffect, useState } from 'react'
import { api } from '../api'

export default function OrtList() {
  const [items, setItems] = useState([])
  const [name, setName] = useState('')

  async function load() {
    const data = await api.getOrt()
    setItems(data ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      await api.createOrt({ Ortnamn: name })
      setName('')
      await load()
    } catch (err) { alert(err.message) }
  }

  return (
    <section>
      <h2>Orter</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Ortnamn" value={name} onChange={e => setName(e.target.value)} />
        <button type="submit">Lägg till</button>
      </form>

      <ul>
        {items.map(x => (
          <li key={x.ortid ?? x.id}>{x.ortnamn ?? x.Ortnamn}</li>
        ))}
      </ul>
    </section>
  )
}
