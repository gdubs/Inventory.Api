import ProductTransaction from './ProductTransaction.jsx';

class ProductTransactions extends React.Component{
    constructor(){
        super();
        this.state = null;
    }
    componentWillMount(nextProps,d){
        if(nextProps && this.state !== nextProps.transactions){
            this.setState(nextProps.transactions);
        }
    }
    render(){
        var whatevs = (this.state == null || this.state.length < 1);
        var element = (this.state == null || this.state.length < 1)
                ? <div>No Transactions Found</div>
                : <ProductTransaction/>;
        return (
            <div>
            {element}
                    <button type="button" className="btn btn-default" onClick={() => this.props.toggleTransactionsHandler()}>
                        Exit
                    </button>
            </div>
        );
    }
}

export default ProductTransactions;