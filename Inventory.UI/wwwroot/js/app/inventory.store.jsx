import { applyMiddleware, createStore } from 'redux';
import reducer from './reducers/reducers.jsx';
import thunk from 'redux-thunk';

const middleWare = applyMiddleware(thunk);
export default createStore(reducer, middleWare);
