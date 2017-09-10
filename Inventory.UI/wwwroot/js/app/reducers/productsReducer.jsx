import * as constants from '../constants/productActionsConstants.jsx';

/*
    Always expect product (in payload) on most returns on
    because you'll need the index 
*/
function reducer(state={
    products : [], 
    currentProduct: {},
    createNew: false,
    edit: false,
    saving: false,
    saved: false,
    fetching: false,
    fetched: false,
    error: null
}, action){
    switch(action.type){
        case constants.PRODUCTS_GET_ALL:{
            return {...state, fetching: true};
        }
        case constants.PRODUCTS_GET_REJECTED:{
            return {...state, fetching: false, error: action.payload };
        }
        case constants.PRODUCTS_GET_FULFILLED:{
            return {...state, products: action.payload, fetching: false, fetched: true}
        }
        case constants.PRODUCTS_CREATE_NEW : {
            return {...state, createNew: true, currentProduct: action.payload};
        }
        case constants.PRODUCTS_EDIT_TOGGLE : {
            let s = {...state, products: state.products.map(
                (product, i) => {
                    if(action.payload.index !== i){
                        return product;
                    }

                    return {
                        ...product,
                        edit: !product.edit
                    }
                }
            )};

            return s;
        }
        case constants.PRODUCTS_TRANSACTIONS_TOGGLE:{
            let s = {...state, products: state.products.map(
                (product, i) => {
                    if(action.payload.index !== i){
                        return product;
                    }

                    return {
                        ...product,
                        transactions: action.payload.transactions,
                        expandTransactions: !product.expandTransactions
                    }
                }
            )};

            return s;
        }
        case constants.PRODUCTS_SAVE_NEW_FULFILLED:{
            return {...state, products: [...state.products, Object.assign({}, action.payload)], createNew : false}
        }
        case constants.PRODUCTS_SAVE_UPDATE_FULFILLED:{

            let s = {
                ...state,
                products: state.products.map(
                    (product, i) => {
                        if(action.payload.index !== i){
                            return product;
                        }

                        return {
                            ...product, 
                            name: action.payload.name,
                            asin: action.payload.asin,
                            shelfCode: action.payload.shelfCode,
                            edit: false,
                            saved: true,
                            saving: false
                        }
                    }
                )
            };

            return s;
        }
        case constants.PRODUCTS_SAVE_REJECTED:{
            return { ...state, 
                error: action.payload, 
                saving: false, 
                saved: false, 
                edit: false, 
                createNew: false 
            }
        }
        case '':{
            //break;
        }
        default:{
            return state;
        }
    }
}

export default reducer;