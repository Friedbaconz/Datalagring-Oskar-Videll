import { useEffect, useState } from 'react'
import { api } from '../api'

export default function KurstillfallelarareList() {
  const [items, setItems] = useState([])
  const [kurstillfalle, setKurstillfalle] = useState([])
  const [larare, setLarare] = useState([])
  const [selectedKurstillfalle, setSelectedKurstillfalle] = useState('')
  const [selectedLarare, setSelectedLarare] = useState('')

  async function load() {
    const k = await api.getKurstillfalle()
    setKurstillfalle(k ?? [])
    const l = await api.getLarare()
    setLarare(l ?? [])
    const lr = await api.getLarareRegi()
    setItems(lr ?? [])
  }

  useEffect(() => { load() }, [])

  async function register(e) {
    e.preventDefault()
    if (!selectedKurstillfalle || !selectedLarare) {
      alert('Välj både kurstillfälle och lärare')
      return
    }
    try {
      await api.createLarareRegi({ LarareRegiId: selectedKurstillfalle, LarareEmail: selectedLarare })
      setSelectedKurstillfalle('')
      setSelectedLarare('')
      await load()
      alert('Lärare registrerad')
    } catch (err) { alert(err.message) }
  }

  async function deleteItem(id) {
    if (!confirm('Är du säker?')) return
    try {
      await api.deleteLarareRegi(id)
      await load()
    } catch (err) { alert(err.message) }
  }

  return (
    <section>
      <h2>Kurstillfälle Lärare</h2>
      <form onSubmit={register} className="inline-form">
        <select value={selectedKurstillfalle} onChange={e => setSelectedKurstillfalle(e.target.value)}>
          <option value="">-- Välj kurstillfälle --</option>
          {kurstillfalle.map(k => (
            <option key={k.id ?? k.ID ?? k.kursTillfallenId} value={k.id ?? k.ID ?? k.kursTillfallenId}>
              {k.kursKodID ?? k.kurskod ?? k.kursKod} - {new Date(k.startdatum ?? k.Startdatum).toLocaleDateString()}
            </option>
          ))}
        </select>
        <select value={selectedLarare} onChange={e => setSelectedLarare(e.target.value)}>
          <option value="">-- Välj lärare --</option>
          {larare.map(l => (
            <option key={l.id ?? l.ID ?? l.larareId} value={l.email ?? l.Email}>
              {l.firstname ?? l.Firstname} {l.lastname ?? l.Lastname}
            </option>
          ))}
        </select>
        <button type="submit">Registrera lärare</button>
      </form>

      <table className="list">
        <thead>
          <tr>
            <th>Id</th>
            <th>Lärare</th>
            <th>Email</th>
            <th>Kurstillfälle</th>
            <th>Åtgärder</th>
          </tr>
        </thead>
        <tbody>
          {items.map(item => (
            <tr key={item.larareRegiId ?? item.LarareRegiId ?? Math.random()}>
              <td>{item.larareRegiId ?? item.LarareRegiId}</td>
              <td>
                {(item.larareRegi?.fornamn ?? item.larareRegi?.Firstname ?? item.LarareRegi?.Firstname ?? item.LarareRegi?.firstname ?? '') + ' ' + 
                 (item.larareRegi?.mellannamn ?? item.larareRegi?.Middlename ?? item.LarareRegi?.Middlename ?? item.LarareRegi?.middlename ?? '') + ' ' + 
                 (item.larareRegi?.efternamn ?? item.larareRegi?.Lastname ?? item.LarareRegi?.Lastname ?? item.LarareRegi?.lastname ?? '')}
              </td>
              <td>{item.larareEmail ?? item.LarareEmail ?? item.larareRegi?.email ?? item.larareRegi?.Email ?? item.LarareRegi?.email ?? item.LarareRegi?.Email}</td>
              <td>{item.kurstillfallen?.kursKodID ?? item.kurstillfallen?.kurskod ?? item.Kurstillfallen?.kursKodID ?? item.Kurstillfallen?.kurskod}</td>
              <td>
                <button onClick={() => deleteItem(item.larareRegiId ?? item.LarareRegiId)}>Ta bort</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
