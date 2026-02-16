import { useEffect, useState } from 'react'
import { api } from '../api'

export default function OrtList() {
  const [items, setItems] = useState([])
  const [name, setName] = useState('')
  const [editId, setEditId] = useState(null)

  async function load() {
    const data = await api.getOrt()
    setItems(data ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      if (editId) {
        await api.updateOrt(editId, { Ortnamn: name })
        setEditId(null)
      } else {
        await api.createOrt({ Ortnamn: name })
      }
      setName('')
      await load()
    } catch (err) { alert(err.message) }
  }

  async function deleteItem(id) {
    if (!confirm('Är du säker?')) return
    try {
      await api.deleteOrt(id)
      await load()
    } catch (err) { alert(err.message) }
  }

  function editItem(item) {
    setEditId(item.ortid ?? item.id)
    setName(item.ortnamn ?? item.Ortnamn)
  }

  function cancelEdit() {
    setEditId(null)
    setName('')
  }

  return (
    <section>
      <h2>Orter</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Ortnamn" value={name} onChange={e => setName(e.target.value)} />
        <button type="submit">{editId ? 'Uppdatera' : 'Lägg till'}</button>
        {editId && <button type="button" onClick={cancelEdit}>Avbryt</button>}
      </form>

      <table className="list">
        <thead><tr><th>Id</th><th>Ortnamn</th><th>Åtgärder</th></tr></thead>
        <tbody>
          {items.map(x => (
            <tr key={x.ortid ?? x.id}>
              <td>{x.ortid ?? x.id}</td>
              <td>{x.ortnamn ?? x.Ortnamn}</td>
              <td>
                <button onClick={() => editItem(x)}>Redigera</button>
                <button onClick={() => deleteItem(x.ortid ?? x.id)} style={{ marginLeft: 8 }}>Ta bort</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
