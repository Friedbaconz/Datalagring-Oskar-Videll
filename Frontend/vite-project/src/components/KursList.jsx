import { useEffect, useState } from 'react'
import { api } from '../api'

export default function KursList() {
  const [items, setItems] = useState([])
  const [form, setForm] = useState({ Kurskod: '', KursNamn: '', Description: '' })
  const [editId, setEditId] = useState(null)

  async function load() {
    const data = await api.getKurs()
    setItems(data ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      if (editId) {
        await api.updateKurs(editId, { Kurskod: form.Kurskod, KursNamn: form.KursNamn, Description: form.Description })
        setEditId(null)
      } else {
        await api.createKurs({ Kurskod: form.Kurskod, KursNamn: form.KursNamn, Description: form.Description })
      }
      setForm({ Kurskod: '', KursNamn: '', Description: '' })
      await load()
    } catch (err) { alert(err.message) }
  }

  async function deleteItem(kurskod) {
    if (!confirm('Är du säker?')) return
    try {
      await api.deleteKurs(kurskod)
      await load()
    } catch (err) { alert(err.message) }
  }

  function editItem(item) {
    setEditId(item.kurskod ?? item.Kurskod)
    setForm({ Kurskod: item.kurskod ?? item.Kurskod, KursNamn: item.kursnamn ?? item.KursNamn, Description: item.description ?? item.Description })
  }

  function cancelEdit() {
    setEditId(null)
    setForm({ Kurskod: '', KursNamn: '', Description: '' })
  }

  return (
    <section>
      <h2>Kurs</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Kurskod" value={form.Kurskod} onChange={e => setForm({ ...form, Kurskod: e.target.value })} disabled={!!editId} />
        <input placeholder="Kursnamn" value={form.KursNamn} onChange={e => setForm({ ...form, KursNamn: e.target.value })} />
        <input placeholder="Description" value={form.Description} onChange={e => setForm({ ...form, Description: e.target.value })} />
        <button type="submit">{editId ? 'Uppdatera' : 'Skapa kurs'}</button>
        {editId && <button type="button" onClick={cancelEdit}>Avbryt</button>}
      </form>

      <table className="list">
        <thead><tr><th>Kurskod</th><th>Kursnamn</th><th>Beskrivning</th><th>Åtgärder</th></tr></thead>
        <tbody>
          {items.map(k => (
            <tr key={k.kurskod ?? k.Kurskod}>
              <td>{k.kurskod ?? k.Kurskod}</td>
              <td>{k.kursnamn ?? k.kursNamn}</td>
              <td>{k.description ?? k.Description}</td>
              <td>
                <button onClick={() => editItem(k)}>Redigera</button>
                <button onClick={() => deleteItem(k.kurskod ?? k.Kurskod)} style={{ marginLeft: 8 }}>Ta bort</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
