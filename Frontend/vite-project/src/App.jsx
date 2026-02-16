import { useState } from 'react'
import './App.css'
import DeltagareList from './components/DeltagareList'
import LarareList from './components/LarareList'
import OrtList from './components/OrtList'
import KursList from './components/KursList'
import KurstillfalleList from './components/KurstillfalleList'
import KurstillfallelarareList from './components/KurstillfallelarareList'

function App() {
  const [view, setView] = useState('deltagare')

  return (
    <div className="app">
      <header>
        <h1>Utbildningsportal</h1>
        <nav>
          <button onClick={() => setView('deltagare')}>Deltagare</button>
          <button onClick={() => setView('larare')}>Lärare</button>
          <button onClick={() => setView('ort')}>Ort</button>
          <button onClick={() => setView('kurs')}>Kurs</button>
          <button onClick={() => setView('kurstillfalle')}>Kurstillfälle</button>
          <button onClick={() => setView('kurstillfallelarare')}>Kurstillfälle Lärare</button>
        </nav>
      </header>

      <main>
        {view === 'deltagare' && <DeltagareList />}
        {view === 'larare' && <LarareList />}
        {view === 'ort' && <OrtList />}
        {view === 'kurs' && <KursList />}
        {view === 'kurstillfalle' && <KurstillfalleList />}
        {view === 'kurstillfallelarare' && <KurstillfallelarareList />}
      </main>
    </div>
  )
}

export default App
