class Menu extends React.Component{
    constructor(){
        super();
    }
    render(){
        return(<div className="sidebar-wrapper">
                    <ul className="sidebar-nav">
                    <li><a href="/">Products</a></li>
                    <li><a href="/">Location</a></li>
                    <li><a href="/">Reports</a></li>
                </ul>
        </div>);
    }
}

export default Menu;