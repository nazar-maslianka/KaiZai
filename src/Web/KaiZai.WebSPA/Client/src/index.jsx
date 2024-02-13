import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './app/layout/App.jsx'
import './index.css'
import 'semantic-ui-css/semantic.min.css'
import { Provider } from 'react-redux'
import { store } from  './app/stores/configureStore'


ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
      <Provider store={store}>
        <App />
      </Provider>
  </React.StrictMode>
)
