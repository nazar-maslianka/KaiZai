import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './app/layout/App.jsx'
import './index.css'
import 'semantic-ui-css/semantic.min.css'
import { Provider } from 'react-redux'
import configureStore from  './app/stores/configureStore.js'

const store = configureStore();

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
      <Provider store={store}>
        <App />
      </Provider>
  </React.StrictMode>
)
