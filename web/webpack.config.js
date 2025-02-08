const path = require('path');

module.exports = () => {
	return {
		mode: 'production',
		entry: './src/MyBackendClient.ts',
		module: {
			rules: [
				{ test: /\.tsx?$/, use: 'ts-loader' }
			]
		},
		resolve: {
			extensions: ['.ts', '.js']
		},
		devtool: 'source-map',
		output: {
			path: path.resolve(__dirname, 'dist'),
			filename: 'MyBackendClient.js',
			library: 'MyBackendClient',
			libraryTarget: 'umd',
			libraryExport: 'default',
			globalObject: 'this'
		},
		optimization: {
			splitChunks: {
				chunks: 'all',
				cacheGroups: {
					commons: {
						test: /[\\/]node_modules[\\/]/,
						name: 'vendor',
						chunks: 'all'
					}
				}
			}
		}
	}
};
