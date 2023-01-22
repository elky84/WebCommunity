module.exports = {
    configureWebpack: {
        devtool: 'source-map',
      },
      devServer: {
            proxy : 'https://localhost:30000'
        }
}