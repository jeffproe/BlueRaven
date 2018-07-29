// Instructs WebPack to bundle up skin assets alongside TinyMCE. See:
// https://www.tinymce.com/docs/advanced/usage-with-module-loaders/#webpackfile-loader
(require as any).context(
	'file-loader?name=[path][name].[ext]&outputPath=assets/&context=node_modules/tinymce!tinymce/skins',
	true,
	/.*/
);
