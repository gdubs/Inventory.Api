import Product from './Product.jsx';
import NewProduct from './NewProduct.jsx';
import { connect } from 'react-redux';
import * as productActions from '../../actions/productActions.jsx';

class Products extends React.Component{
    constructor() {
        super();
        
        // this.state = { products: [], productNewFormVisible: false } ;
        this.saveProduct = this.saveProduct.bind(this);
        this.openNewObject = this.openNewObject.bind(this);
        this.toggleProductEdit = this.toggleProductEdit.bind(this);
        this.toggleTransactionsView = this.toggleTransactionsView.bind(this);
    }
    componentWillMount(){
        this.props.dispatch(productActions.getAll());
    }
    openNewObject(e) {
        this.props.dispatch(productActions.createNewProduct());
    }
    saveProduct(e) {
        if(e.edit){
            this.props.dispatch(productActions.saveProduct(e));
        }else{
            this.props.dispatch(productActions.saveNewProduct(e));
        }
    }
    toggleProductEdit(e){
        this.props.dispatch(productActions.editProductToggleForm(e));
    }
    toggleTransactionsView(e){
        this.props.dispatch(productActions.productTransactionsToggleForm(e));
    }
    render() {

        //console.log('pak render')
        //console.log(this.props)
        var data = this.props.products.products;
        var products = [];
        
        for (var p=0; p < data.length; p++) {
            let currProduct = Object.assign({}, data[p], { index:p });
            products.push(<li key={p} className='list-group-item'>
                <Product key={p} id={p} 
                    product={currProduct} 
                    saveProductHandler={this.saveProduct}
                    toggleProductHandler={this.toggleProductEdit}
                    toggleTransactionsHandler = {this.toggleTransactionsView}
                    />
            </li>);
        }
        
        return (
            <div>
                <h2>Products</h2>
                <div className="row">
                    <ul className='list-group'>
                        {products}
                    </ul>
                </div>
                <div className="row">
                    <div className="col-md-1">
                        <button className="btn btn-default" onClick={this.openNewObject}><i className="glyphicon glyphicon-plus"></i></button>
                    </div>
                    <div className="col-md-11">
                    {
                        this.props.products.createNew
                            ? <NewProduct saveProductHandler={this.saveProduct} currentProduct={this.props.products.currentProduct}/>
                            : null
                    }
                    </div>
                </div>
            </div>
        );
    }
};

function mapStateToProps (state, props) {
    return state;
}

export default connect(mapStateToProps)(Products);

// ReactDOM.render(
//     <Products/>,
//     document.getElementById('products')
// );