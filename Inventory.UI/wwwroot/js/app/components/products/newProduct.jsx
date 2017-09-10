
class NewProduct extends React.Component{
    constructor() {
        super();
        this.state = {};
        this.handleChange = this.handleChange.bind(this);
    }
    componentWillMount(){
        this.setState(this.props.currentProduct);
    }
    handleChange(e) {
        /*var prop   = e.target.name;
        var val = e.target.value;*/
        e.persist();    // is this advisable??
        this.setState((prevState, props) => {
            return { [e.target.name]: e.target.value };
        })
    }
    render() {
        const label = this.props.currentProduct.edit ? "Edit Product" : "New product";
        const currentProduct = this.props.currentProduct;

        return (
                <div className="panel panel-default">
                    <div className="panel-heading">
                        {label}
                        <span className="pull-right" onClick={() => this.props.toggleProductHandler()}>
                            <i className="glyphicon glyphicon-remove"></i>
                        </span>
                    </div>
                    <div className="panel-body form-horizontal payment-form">
                        <div className="form-group">
                            <label htmlFor="name" className="col-sm-3 control-label">Name</label>
                            <div className="col-sm-9">
                                <input type="text" value={this.state.name} name="name" className="form-control" onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="asin" className="col-sm-3 control-label">ASIN</label>
                            <div className="col-sm-9">
                                <input type="text" value={this.state.asin} name="asin" className="form-control" onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="shelfCode" className="col-sm-3 control-label">Shelf Code</label>
                            <div className="col-sm-9">
                                <input type="text" value={this.state.shelfCode} name="shelfCode" className="form-control" onChange={this.handleChange} />
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-sm-12 text-right">
                                <button type="button" className="btn btn-default preview-add-button" onClick={() => this.props.saveProductHandler(this.state)}>
                                {
                                    this.state.edit
                                    ? <span className="glyphicon glyphicon-plus">Save</span>
                                    : <span className="glyphicon glyphicon-plus">Add</span>
                                }
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            );
    }
};
export default NewProduct;