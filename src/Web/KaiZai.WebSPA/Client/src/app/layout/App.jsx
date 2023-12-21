// import { useState } from 'react'
import '../../features/style.css'

import './App.css'
import Main from '../../features/Main.jsx'
// import MainAppMenu from '../../features/MainAppMenu.jsx'
import IncomeCard from '../../features/incomes/IncomeCard.jsx'

function App() {
  return (
    <>
      {/* Sidebar */}
      <IncomeCard/>
      {/* Main Content */}
      <Main/>
    </>
  )
}

export default App
