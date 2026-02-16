import { useEffect, useState } from 'react'
import { api } from '../api'

export default function LarareList() {
  const [items, setItems] = useState([])
  const [form, setForm] = useState({ Email: '', Firstname: '', Middlename: '', Lastname: '', Kompentens: '' })
  const [editId, setEditId] = useState(null)

  async function load() {
    const data = await api.getLarare()
    setItems(data ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      if (editId) {
        await api.updateLarare(editId, form)
        setEditId(null)
      } else {
        await api.createLarare(form)
      }
      setForm({ Email: '', Firstname: '', Middlename: '', Lastname: '', Kompentens: '' })
      await load()
    } catch (err) { alert(err.message) }
  }

  async function deleteItem(email) {
    if (!confirm('Är du säker?')) return
    try {
      await api.deleteLarare(email)
      await load()
    } catch (err) { alert(err.message) }
  }

  function editItem(item) {
    setEditId(item.email)
    setForm({ Email: item.email, Firstname: item.firstname, Middlename: item.middlename, Lastname: item.lastname, Kompentens: item.kompetens })
  }

  function cancelEdit() {
    setEditId(null)
    setForm({ Email: '', Firstname: '', Middlename: '', Lastname: '', Kompentens: '' })
  }

  return (
    <section>
      <h2>Lärare</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Email" value={form.Email} onChange={e => setForm({ ...form, Email: e.target.value })} />
        <input placeholder="Förnamn" value={form.Firstname} onChange={e => setForm({ ...form, Firstname: e.target.value })} />
        <input placeholder="Mellannamn" value={form.Middlename} onChange={e => setForm({ ...form, Middlename: e.target.value })} />
        <input placeholder="Efternamn" value={form.Lastname} onChange={e => setForm({ ...form, Lastname: e.target.value })} />
        <input placeholder="Kompetens" value={form.Kompentens} onChange={e => setForm({ ...form, Kompentens: e.target.value })} />
        <button type="submit">{editId ? 'Uppdatera' : 'Lägg till'}</button>
        {editId && <button type="button" onClick={cancelEdit}>Avbryt</button>}
      </form>

      <table className="list">
        <thead><tr><th>Email</th><th>Namn</th><th>Kompetens</th><th>Åtgärder</th></tr></thead>
        <tbody>
          {items.map(x => (
            <tr key={x.email}>
              <td>{x.email}</td>
              <td>{[x.firstname, x.middlename, x.lastname].filter(Boolean).join(' ')}</td>
              <td>{x.kompentens}</td>
              <td>
                <button onClick={() => editItem(x)}>Redigera</button>
                <button onClick={() => deleteItem(x.email)} style={{ marginLeft: 8 }}>Ta bort</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
