import delay from './delay.jsx';

const products = [
    { url: 'yoyoyo', id: 1, name: 'Coke', asin: 'EDIKDO', shelfCode: 'AKDIDO' },
    { url: 'yoyoyo', id: 2, name: 'Sprite', asin: 'BLAKDO', shelfCode: 'AKDIDO' },
    { url: 'yoyoyo', id: 3, name: 'Pickles', asin: 'TAEKDO', shelfCode: 'DREO90' },
    { url: 'yoyoyo', id: 4, name: 'Steak', asin: 'EPALKAO', shelfCode: 'IDIEO8' }
];

const transactionHistory = [
    { id: 1, type: 'add', productId: 1, count: 50, date: '01/01/2017' },
    { id: 2, type: 'add', productId: 1, count: 50, date: '02/01/2017' },
    { id: 3, type: 'add', productId: 1, count: 50, date: '03/01/2017' },
    { id: 4, type: 'add', productId: 1, count: 50, date: '04/01/2017' },
    { id: 5, type: 'sold', productId: 1, count: 35, date: '01/15/2017' },
    { id: 6, type: 'sold', productId: 1, count: 60, date: '03/21/2017' },
    { id: 7, type: 'sold', productId: 1, count: 5, date: '04/5/2017' },
    { id: 8, type: 'add', productId: 2, count: 30, date: '01/01/2017' },
    { id: 9, type: 'sold', productId: 2, count: 20, date: '02/01/2017' },
    { id: 10, type: 'add', productId: 2, count: 10, date: '03/01/2017' },
    { id: 11, type: 'sold', productId: 2, count: 12, date: '04/01/2017' },
    { id: 12, type: 'discard', productId: 2, count: 8, date: '04/15/2017' },
    { id: 13, type: 'add', productId: 2, count: 60, date: '04/01/2017' },
    { id: 14, type: 'sold', productId: 2, count: 5, date: '04/25/2017' },
    { id: 15, type: 'add', productId: 3, count: 20, date: '01/01/2017' },
    { id: 16, type: 'add', productId: 3, count: 60, date: '02/01/2017' },
    { id: 17, type: 'sold', productId: 3, count: 35, date: '02/21/2017' },
    { id: 18, type: 'sold', productId: 3, count: 45, date: '03/21/2017' },
    { id: 19, type: 'add', productId: 3, count: 50, date: '03/01/2017' },
    { id: 20, type: 'discard', productId: 3, count: 10, date: '04/26/2017' },
    { id: 21, type: 'sold', productId: 3, count: 15, date: '05/04/2017' },
    { id: 15, type: 'add', productId: 4, count: 10, date: '01/01/2017' },
    { id: 16, type: 'add', productId: 4, count: 10, date: '02/01/2017' },
    { id: 17, type: 'sold', productId: 4, count: 5, date: '01/21/2017' },
    { id: 18, type: 'sold', productId: 4, count: 8, date: '02/21/2017' },
    { id: 19, type: 'add', productId: 4, count: 10, date: '03/01/2017' },
    { id: 20, type: 'discard', productId: 4, count: 7, date: '02/25/2017' },
    { id: 21, type: 'sold', productId: 4, count: 6, date: '03/27/2017' }
]
class productsProvider {
    static getAll(){
        return new Promise((resolve, reject) =>{

            var data = {
                values : products,
                links : []
            }

            data.values.map(product => {
                var count = 0;
                var transactions = transactionHistory.map(transaction => {
                    if(transaction.productId === product.id){
                        switch(transaction.type){
                            case 'add':
                                count += transaction.count;
                                break;
                            default:
                                count -= transaction.count;
                                break;
                        }
                    }
                })
                product.totalCount = count;
            })

            setTimeout(() => {
                resolve(Object.assign({}, data));
            }, delay);
        });
    }

    static getAllTransactionsByProduct(id){
        return new Promise((resolve, reject) =>{
            var data = {
                values: [],
                links: []
            }

            var transactions = []
            
            transactionHistory.forEach(transaction => {
                if(transaction.productId === id){
                    transactions.push(transaction);
                }
            });
            
            setTimeout(() => {
                resolve(Object.assign({}, data, { values : transactions }));
            }, delay);
        });
    }
}

export default productsProvider;