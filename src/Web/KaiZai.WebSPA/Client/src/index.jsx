import ReactDOM from 'react-dom/client'
import App from './app/layout/App.jsx'
import './index.css'
import 'semantic-ui-less/semantic.less'
import { Provider } from 'react-redux'
import { store } from  './app/stores/configureStore'


ReactDOM.createRoot(document.getElementById('root')).render(
      <Provider store={store}>
        <App />
      </Provider>
)
