import '../../features/style.css'
import './App.css'
import {
  Container,
} from "semantic-ui-react";
import IncomesTransactions from '../../features/incomes/IncomesTransactions'
import MainMenu from '../layout/MainMenu'

function App() {
  return (
    //TODO: add centering for big resolutions and styles for different resolutions
      <div id="main-container">
        <MainMenu  />
        <Container>
          <IncomesTransactions />
        </Container>
    </div>
  )
}

export default App
