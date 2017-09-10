import { combineReducers } from 'redux';

import products from './productsReducer.jsx';
import shelves from './shelvesReducer.jsx';

const reducers = combineReducers({
    products,
    shelves
});

export default reducers;