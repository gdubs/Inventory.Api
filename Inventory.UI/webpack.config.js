var path = require('path');

// module.exports = {
//     context: path.join(__dirname + '/wwwroot/js', 'app'),
//     entry: './gtestyo.js',
//     output: {
//         path: path.join(__dirname + '/wwwroot/js', 'built'),
//         filename: '[name].bundle.js'
//     }
// };

module.exports = {
    context: path.join(__dirname + '/wwwroot/js', 'app'),
    entry: { 
        inventory: './app.product.jsx',
        shelves: './app.shelf.jsx',
        layout: './app.layout.public.jsx'
     },
    output: {
        path: path.join(__dirname + '/wwwroot/js', 'built'),
        filename: '[name].bundle.js'
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: 'babel-loader',
                exclude: /(node_modules|bower_components)/,
                query: {
                    presets: ['es2015', 'react', 'stage-0']
                }
            }
        ]
    }
}