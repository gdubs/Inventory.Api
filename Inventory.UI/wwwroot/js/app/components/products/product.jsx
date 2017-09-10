import NewProduct from './NewProduct.jsx';
import ProductTransactions from './ProductTransactions.jsx';

class Product extends React.Component{
    constructor() {
        super();
        this.state = {};
        this.toggleEdit = this.toggleEdit.bind(this);
        this.saveEdit = this.saveEdit.bind(this);
    }
    toggleEdit(e){
        let clone = this.state.edit;
        this.setState({ edit: !clone}, ()=> {
            this.props.toggleProductHandler(this.state);
        });
    }
    saveEdit(e){
        this.props.saveProductHandler(e);
        //this.toggleEdit(e);
    }
    componentDidMount(){
    }
    componentWillMount(){
        this.setState(this.props.product);
    }
    componentWillReceiveProps(nextProps,d) {
        if(this.state !== nextProps.product){
            this.setState(nextProps.product);
        }
      }
    render() {
        const product = this.state;
        var productStyle = {
            padding:"5px 0"
        }

        var transactions = (product.transactions == null ? [] : product.transactions);
        return (
            <div style={productStyle}>
                {
                    product.edit
                    ?   <div className="row">
                            <NewProduct 
                                currentProduct={product} 
                                saveProductHandler={this.saveEdit}
                                toggleProductHandler={this.toggleEdit}/>
                        </div>
                    : <div className="panel-body form-horizontal payment-form">
                            <div className="row">
                                <div className="form-group">
                                    <label htmlFor="name" className="col-sm-3 control-label">Name</label>
                                    <div className="col-sm-9" name="name">
                                        {product.name}
                                    </div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group">
                                    <label htmlFor="asin" className="col-sm-3 control-label">ASIN</label>
                                    <div className="col-sm-9" name="asin">
                                        {product.asin}
                                    </div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group">
                                    <label htmlFor="shelfCode" className="col-sm-3 control-label">Shelf Code</label>
                                    <div className="col-sm-9" name="shelfCode">
                                        {product.shelfCode}
                                    </div>
                                </div>
                            </div>
                            <div className="row">
                                <label htmlFor="totalCount" className="col-sm-3 control-label">Total Available</label>
                                <div className="col-sm-2" name="totalCount">
                                    {product.totalCount}
                                </div>
                            </div>
                            <div className="row">
                                <button type="button" className="btn btn-default" onClick={this.toggleEdit}>
                                    <i className="glyphicon glyphicon-edit"></i> Edit
                                </button>
                                <button type="button" className="btn btn-default" onClick={() => this.props.toggleTransactionsHandler(this.state)}>
                                    Transactions / History
                                </button>
                            </div>
                            {
                                product.expandTransactions 
                                ? <ProductTransactions 
                                    transactions={transactions}
                                    toggleTransactionsHandler = {() => this.props.toggleTransactionsHandler(this.state)}/>
                                : null
                            }
                        </div>

                        
                }
            </div>
        );
    }
}

export default Product;