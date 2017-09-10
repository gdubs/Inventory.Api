//import 'babel-polyfill';
import Products from './components/products/Products.jsx';

import { Provider } from 'react-redux';
import store from './inventory.store.jsx';


ReactDOM.render(
  <Provider store={store}>
    <Products />
  </Provider>,
  document.getElementById('app.inventory')
);