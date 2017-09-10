import ProductsProvider from '../providers/productProvider.jsx'
import * as constants from '../constants/productActionsConstants.jsx'
export function getById(){
    return {
        type: '',
        payload: {}
    }
}

/*
You need return product because index is needed alongwith what 
it is that's requested
*/
export function getAll(){
    return function(dispatch){
            ProductsProvider.getAll().then((data) => {
                    //products = data.values
                    dispatch({
                        type: 'PRODUCTS_GET_FULFILLED',
                        payload: data.values
                    })

                }).catch(error => {
                    dispatch({
                        type: 'PRODUCTS_GET_REJECTED',
                        payload: "oopsss wala pak!"
                    })
                })
            }
}
export function createNewProduct(){
    return {
        type: constants.PRODUCTS_CREATE_NEW,
        payload: { url: '', name: '', asin: '', shelfCode: '', edit: false }
    }
}
export function editProduct(product){
    return {
        type: constants.PRODUCTS_EDIT_EXISTING,
        payload: { ...product, edit: true }
    }
}
export function editProductToggleForm(product){
    return {
        type: constants.PRODUCTS_EDIT_EXISTING,
        payload: {...product}
    }
}
export function productTransactionsToggleForm(product){

    var willExpand = !product.expandTransactions;
    return function(dispatch){
        if(!willExpand){
            dispatch({
                type: constants.PRODUCTS_TRANSACTIONS_TOGGLE,
                payload: {...product, transactions: []}
            });
        }else{
            ProductsProvider.getAllTransactionsByProduct(product.id).then((data) => {
                dispatch({
                        type: constants.PRODUCTS_TRANSACTIONS_TOGGLE,
                        payload: {...product, transactions: data.values}
                    });
            }).catch(error => {
                dispatch({
                        type: constants.PRODUCTS_TRANSACTIONS_TOGGLE_REJECTED,
                        payload: 'error getting transactions'
                    });
            });
        }
    }
}
export function saveProduct (product){
    return {
        type: constants.PRODUCTS_SAVE_UPDATE_FULFILLED,
        payload: product
    }
}

export function saveNewProduct (product){
    return {
        type: constants.PRODUCTS_SAVE_NEW_FULFILLED,
        payload: product
    }
}