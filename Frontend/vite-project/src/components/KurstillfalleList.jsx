import { useEffect, useState } from 'react'
import { api } from '../api'

function prettyDate(d) {
  try { return new Date(d).toLocaleDateString() } catch { return d }
}

export default function KurstillfalleList() {
  const [items, setItems] = useState([])
  const [kurskod, setKurskod] = useState('')
  const [start, setStart] = useState('')
  const [slut, setSlut] = useState('')
  const [maxSeats, setMaxSeats] = useState(10)
  const [ortId, setOrtId] = useState(1)

  const [deltagare, setDeltagare] = useState([])
  const [selectedDeltagare, setSelectedDeltagare] = useState('')

  async function load() {
    const k = await api.getKurstillfalle()
    setItems(k ?? [])
    const d = await api.getDeltagare()
    setDeltagare(d ?? [])
  }

  useEffect(() => { load() }, [])

  async function submit(e) {
    e.preventDefault()
    try {
      await api.createKurstillfalle({ Kurskod: kurskod, Startdatum: start, Slutdatum: slut, Maxseats: Number(maxSeats), OrtId: Number(ortId) })
      setKurskod(''); setStart(''); setSlut(''); setMaxSeats(10)
      await load()
    } catch (err) { alert(err.message) }
  }

  async function register(kurstillfalleId) {
    if (!selectedDeltagare) { alert('Välj deltagare'); return }
    try {
      await api.createKursRegi({ RegiID: kurstillfalleId, Antagen: selectedDeltagare, RegistrationDate: new Date().toISOString(), Status: 'Pending' })
      await load()
      alert('Registrerad')
    } catch (err) { alert(err.message) }
  }

  return (
    <section>
      <h2>Kurstillfällen</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Kurskod" value={kurskod} onChange={e => setKurskod(e.target.value)} />
        <input type="date" value={start} onChange={e => setStart(e.target.value)} />
        <input type="date" value={slut} onChange={e => setSlut(e.target.value)} />
        <input placeholder="Max seats" value={maxSeats} onChange={e => setMaxSeats(e.target.value)} />
        <input placeholder="OrtId" value={ortId} onChange={e => setOrtId(e.target.value)} />
        <button type="submit">Skapa tillfälle</button>
      </form>

      <div style={{ marginTop: 12 }}>
        <label>Välj deltagare för registrering: </label>
        <select value={selectedDeltagare} onChange={e => setSelectedDeltagare(e.target.value)}>
          <option value="">-- välj --</option>
          {deltagare.map(d => (
            <option key={d.id ?? d.ID ?? d.deltagareId} value={d.id ?? d.ID ?? d.deltagareId}>{(d.fornamn ?? d.firstname ?? d.Fornamn ?? d.Firstname) + ' ' + (d.efternamn ?? d.lastname ?? d.Efternamn ?? d.Lastname)}</option>
          ))}
        </select>
      </div>

      <table className="list">
        <thead><tr><th>Id</th><th>Kurskod</th><th>Period</th><th>Ort</th><th>Reg</th></tr></thead>
        <tbody>
          {items.map(k => (
            <tr key={k.id ?? k.ID ?? k.kursTillfallenId}>
              <td>{k.id ?? k.ID ?? k.kursTillfallenId}</td>
              <td>{k.kursKodID ?? k.kurskod ?? k.kursKod}</td>
              <td>{prettyDate(k.startdatum ?? k.Startdatum)} - {prettyDate(k.slutdatum ?? k.Slutdatum)}</td>
              <td>{k.ort?.ortnamn ?? k.ort?.Ortnamn ?? k.ort?.ortnamn}</td>
              <td><button onClick={() => register(k.id ?? k.ID ?? k.kursTillfallenId)}>Registrera vald deltagare</button></td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
