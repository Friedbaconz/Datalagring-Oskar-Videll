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
  const [maxSeats, setMaxSeats] = useState('')
  const [ortId, setOrtId] = useState('')
  const [editId, setEditId] = useState(null)

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
      if (editId) {
        await api.updateKurstillfalle(editId, { Kurskod: kurskod, Startdatum: start, Slutdatum: slut, Maxseats: Number(maxSeats), OrtId: Number(ortId) })
        setEditId(null)
      } else {
        await api.createKurstillfalle({ Kurskod: kurskod, Startdatum: start, Slutdatum: slut, Maxseats: Number(maxSeats), OrtId: Number(ortId) })
      }
      setKurskod(''); setStart(''); setSlut(''); setMaxSeats('')
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

  async function deleteItem(id) {
    if (!confirm('Är du säker?')) return
    try {
      await api.deleteKurstillfalle(id)
      await load()
    } catch (err) { alert(err.message) }
  }

  function editItem(item) {
    setEditId(item.id ?? item.ID ?? item.kursTillfallenId)
    setKurskod(item.kursKodID ?? item.kurskod ?? item.kursKod)
    setStart(item.startdatum ?? item.Startdatum)
    setSlut(item.slutdatum ?? item.Slutdatum)
    setMaxSeats((item.maxseats ?? item.Maxseats)?.toString())
    setOrtId((item.ortid ?? item.Ortid)?.toString())
  }

  function cancelEdit() {
    setEditId(null)
    setKurskod(''); setStart(''); setSlut(''); setMaxSeats(''); setOrtId('')
  }

  return (
    <section>
      <h2>Kurstillfällen</h2>
      <form onSubmit={submit} className="inline-form">
        <input placeholder="Kurskod" value={kurskod} onChange={e => setKurskod(e.target.value)} disabled={!!editId} />
        <input type="date" value={start} onChange={e => setStart(e.target.value)} />
        <input type="date" value={slut} onChange={e => setSlut(e.target.value)} />
        <input placeholder="Max seats" value={maxSeats} onChange={e => setMaxSeats(e.target.value)} />
        <input placeholder="OrtId" value={ortId} onChange={e => setOrtId(e.target.value)} />
        <button type="submit">{editId ? 'Uppdatera' : 'Skapa tillfälle'}</button>
        {editId && <button type="button" onClick={cancelEdit}>Avbryt</button>}
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
        <thead>
          <tr>
            <th>Id</th>
            <th>Kurskod</th>
            <th>KursNamn</th>
            <th>Period (Starts/Ends)</th>
            <th>Ort</th>
            <th>Max Seats</th>
            <th>Lärare</th>
            <th>Deltagare</th>
            <th>Åtgärder</th>
          </tr>
        </thead>
        <tbody>
          {items.map(k => {
            const participants = k.deltagare ?? k.Deltagare ?? k.KursRegiDeltagare ?? []
            const teachers = k.larareEmai ?? k.LarareEmai ?? k.kursTillfallenLarare ?? k.KursTillfallenLarare ?? []
            return (
              <tr key={k.id ?? k.ID ?? k.kursTillfallenId}>
                <td>{k.id ?? k.ID ?? k.kursTillfallenId}</td>
                <td>{k.kursKodID ?? k.kurskod ?? k.kursKod}</td>
                <td>{(k.kurs && (k.kurs.kursnamn ?? k.kurs.Kursnamn)) ?? ''}</td>
                <td>{prettyDate(k.startdatum ?? k.Startdatum)} - {prettyDate(k.slutdatum ?? k.Slutdatum)}</td>
                <td>{(k.ort && (k.ort.ortNamn ?? k.ort.OrtNamn)) ?? ''} - {k.ort?.ortId ?? k.ort?.Ortid ?? ''}</td>

                <td>{participants.length ?? 0} / {k.maxseats ?? k.Maxseats}</td>

                <td>
                  {teachers && teachers.length > 0 ? (
                    <ul style={{ margin: 0, paddingLeft: 16 }}>
                      {teachers.map(t => (
                        <li key={t.id ?? t.ID ?? t.larareId ?? Math.random()}>
                          {(t.fornamn ?? t.firstname ?? t.Fornamn ?? t.Firstname) + ' ' + (t.efternamn ?? t.lastname ?? t.Efternamn ?? t.Lastname)}
                        </li>
                      ))}
                    </ul>
                  ) : <small>Inga lärare</small>}
                </td>

                <td>
                  {participants && participants.length > 0 ? (
                    <ul style={{ margin: 0, paddingLeft: 16 }}>
                      {participants.map(p => (
                        <li key={p.id ?? p.ID ?? p.deltagareId ?? p.DeltagareId}>
                          {(p.fornamn ?? p.firstname ?? p.Fornamn ?? p.Firstname) + ' ' + (p.efternamn ?? p.lastname ?? p.Efternamn ?? p.Lastname)}
                        </li>
                      ))}
                    </ul>
                  ) : <small>Inga deltagare</small>}
                </td>

                <td>
                  <button onClick={() => register(k.id ?? k.ID ?? k.kursTillfallenId)}>Registrera</button>
                  <button onClick={() => editItem(k)} style={{ marginLeft: 4 }}>Redigera</button>
                  <button onClick={() => deleteItem(k.id ?? k.ID ?? k.kursTillfallenId)} style={{ marginLeft: 4 }}>Ta bort</button>
                </td>
              </tr>
            )
          })}
        </tbody>
      </table>
    </section>
  )
}
