import * as ExtractTextPlugin from 'extract-text-webpack-plugin';
import * as fs from 'fs';
import * as HtmlWebpackPlugin from 'html-webpack-plugin';
import * as path from 'path';
import * as UglifyJsPlugin from 'uglifyjs-webpack-plugin';
import * as webpack from 'webpack';

const srcPath = path.join(__dirname, '/app'),
    distPath = path.join(__dirname, '/wwwroot');

const isDevelopment = false;

// This defines all the bundles that get generated, including the .cshtml files
// that get generated so we can include assets in views.
const bundles = fs.readdirSync(path.join(__dirname, '/app/js/bundles')).map((value) => value.substring(0, value.length - 3));

const excludeTinyMCEResources = (input: string) => {
    // tinymce-resources.ts specifically requests the loading of skins and other TinyMCE assets
    // with the file loader, which causes them to be copied out to wwwroot/assets/. However, if
    // we don't exclude these files from the default CSS / resource rules, they'll then be
    // processed a second time, which will replace the content of the files emitted to wwwroot/assets/
    // with references to the extracted CSS or paths to the then again copied assets, making the
    // contents of the TinyMCE resources either invalid or empty.
    return input.replace(/\\/g, "/").indexOf("/tinymce/skins/") != -1;
}

const config: webpack.Configuration = {
    cache: true,
    devtool: 'source-map',
    context: srcPath,
    entry: {
        'polyfills': [
            'core-js',
        ],
        'jquery': [
            // this shim exports $ and jQuery to window, because when you include
            // jQuery directly in Webpack it doesn't set the variables on
            // the window object by default.
            './js/jquery-shim',
        ],
        'bootstrap': [
            'bootstrap/dist/js/bootstrap',
            'bootstrap/dist/css/bootstrap.css',
            './js/ie10-viewport-bug-workaround',
        ],
        'tinymce': [
            'tinymce/tinymce',
            'tinymce/jquery.tinymce',
            'tinymce/themes/modern',
            'tinymce/plugins/link',
            'tinymce/plugins/lists',
        ],
        'tinymce-res': [
            // We don't actually want to include the CSS and image resources in the TinyMCE
            // bundle, as these resources are only to be loaded inside the TinyMCE iframe,
            // which it creates. However, by having this bundle here we ensure the resource
            // files get copied out to the wwwroot/ directory correctly for deployment.
            './js/tinymce-resources',
        ],
        'font-awesome': [
            'font-awesome/css/font-awesome.css',
        ],
        ...bundles.reduce((map, value) => {
            // builds a JS hashmap like {'page-login': 'js/page-login.ts', 'page-default': 'js/page-default.ts', ...}
            map[value] = [
                './js/bundles/' + value + '.ts',
            ];
            return map;
        }, {})
    },
    output: {
        path: distPath,
        filename: 'js/[name].[chunkhash].' + (isDevelopment ? 'dev' : 'min') + '.js',
        publicPath: '/',
    },
    resolve: {
        extensions: ['.ts', '.tsx', '.js'],
        modules: ["node_modules"],
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: [
                    {
                        loader: 'awesome-typescript-loader',
                        options: {
                            configFile: path.join(__dirname, '/tsconfig.webpack.json'),
                            silent: true,
                        }
                    }
                ]
            },
            {
                test: /\.css?$/,
                exclude: excludeTinyMCEResources, // Prevent TinyMCE resources being processed twice (see above).
                use: ExtractTextPlugin.extract({
                    loader: 'css-loader',
                    options: { minimize: !isDevelopment },
                })
            },
            {
                test: /\.(png|jpg|eot|ttf|svg|woff|woff2|gif)$/,
                exclude: excludeTinyMCEResources, // Prevent TinyMCE resources being processed twice (see above).
                use: [
                    {
                        loader: "file-loader",
                        options: {
                            outputPath: 'assets/',
                            name: '[name].[hash].[ext]',
                        }
                    }
                ]
            }
        ]
    },
    plugins: [
        new webpack.NoEmitOnErrorsPlugin(),
        new ExtractTextPlugin('css/[name].[contenthash].' + (isDevelopment ? 'dev' : 'min') + '.css'),
        ...bundles.filter(x => x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Scripts.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_ScriptsTemplate.cshtml'),
                chunks: ['polyfills', 'jquery', 'bootstrap', value, 'font-awesome'],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...bundles.filter(x => x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Styles.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_StylesTemplate.cshtml'),
                chunks: ['polyfills', 'jquery', 'bootstrap', value, 'font-awesome'],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...bundles.filter(x => !x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Scripts.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_ScriptsTemplate.cshtml'),
                chunks: [value],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...bundles.filter(x => !x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Styles.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_StylesTemplate.cshtml'),
                chunks: [value],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...['tinymce'].map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Scripts.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_ScriptsTemplate.cshtml'),
                chunks: [value],
                inject: false,
            })
        }),
        ...['tinymce'].map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Styles.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_StylesTemplate.cshtml'),
                chunks: [value],
                inject: false,
            })
        }),
        ...(isDevelopment ? [] : [
            new UglifyJsPlugin({
                sourceMap: true,
                // Needed for TinyMCE, see https://www.tinymce.com/docs/advanced/usage-with-module-loaders/#minificationwithuglifyjs2
                uglifyOptions: {
                    output: {
                        ascii_only: true,
                    }
                }
            }),
        ])
    ]
};

export default config;