import { useEffect, useState } from 'react'
import { api } from '../api'

export default function DeltagareList() {
  const [items, setItems] = useState([])
  const [form, setForm] = useState({ Firstname: '', Middlename: '', Lastname: '', Email: '', Phonenumber: '' })
  const [loading, setLoading] = useState(false)

  async function load() {
    setLoading(true)
    try {
      const data = await api.getDeltagare()
      setItems(data ?? [])
    } finally { setLoading(false) }
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      await api.createDeltagare(form)
      setForm({ Firstname: '', Middlename: '', Lastname: '', Email: '', Phonenumber: '' })
      await load()
    } catch (err) { alert(err.message) }
  }

  return (
    <section>
      <h2>Deltagare</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Förnamn" value={form.Firstname} onChange={e => setForm({ ...form, Firstname: e.target.value })} />
        <input placeholder="Mellannamn" value={form.Middlename} onChange={e => setForm({ ...form, Middlename: e.target.value })} />
        <input placeholder="Efternamn" value={form.Lastname} onChange={e => setForm({ ...form, Lastname: e.target.value })} />
        <input placeholder="Email" value={form.Email} onChange={e => setForm({ ...form, Email: e.target.value })} />
        <input placeholder="Telefon" value={form.Phonenumber} onChange={e => setForm({ ...form, Phonenumber: e.target.value })} />
        <button type="submit">Lägg till</button>
      </form>

      {loading ? <p>Loading…</p> : (
        <table className="list">
          <thead><tr><th>Id</th><th>Namn</th><th>Email</th><th>Telefon</th></tr></thead>
          <tbody>
            {items.map(x => (
              <tr key={x.id ?? x.deltagareId ?? Math.random()}>
                <td>{x.id ?? x.deltagareId}</td>
                <td>{[x.firstname, x.middlename, x.lastname].filter(Boolean).join(' ')}</td>
                <td>{x.email}</td>
                <td>{x.phonenumber}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </section>
  )
}
