var gulp = require('gulp');
var concat = require('gulp-concat');
var sass = require('gulp-sass');

var webroot = 'wwwroot';
var scssSrc = 'scss/**/site.scss';
var scssDest = webroot;

// var vendorStylesMin = ['node_modules/bootstrap/dist/css/bootstrap.min.css'];
// var vendorScriptsMin = [
// 	'node_modules/jquery/dist/jquery.min.js',
// 	'node_modules/popper.js/dist/umd/popper.min.js',
// 	'node_modules/bootstrap/dist/js/bootstrap.min.js'
// ];
// var vendorStyles = ['node_modules/bootstrap/dist/css/bootstrap.css'];
// var vendorScripts = [
// 	'node_modules/jquery/dist/jquery.js',
// 	'node_modules/popper.js/dist/umd/popper.js',
// 	'node_modules/bootstrap/dist/js/bootstrap.js'
// ];
var vendorStylesMin = [
	'node_modules/materialize-css/dist/css/materialize.min.css'
];
var vendorScriptsMin = [
	'node_modules/materialize-css/dist/js/materialize.min.js'
];
var vendorStyles = ['node_modules/materialize-css/dist/css/materialize.css'];
var vendorScripts = ['node_modules/materialize-css/dist/js/materialize.js'];

gulp.task('default', ['build-vendor', 'compile:sass']);

gulp.task('build-vendor', [
	'build-vendor-min-css',
	'build-vendor-min-js',
	'build-vendor-css',
	'build-vendor-js'
]);

gulp.task('compile:sass', function() {
	gulp
		.src(scssSrc)
		.pipe(sass())
		.pipe(gulp.dest(scssDest));
});

gulp.task('build-vendor-css', () => {
	return gulp
		.src(vendorStyles)
		.pipe(concat('vendor.css'))
		.pipe(gulp.dest(webroot));
});

gulp.task('build-vendor-js', () => {
	return gulp
		.src(vendorScripts)
		.pipe(concat('vendor.js'))
		.pipe(gulp.dest(webroot));
});

gulp.task('build-vendor-min-css', () => {
	return gulp
		.src(vendorStylesMin)
		.pipe(concat('vendor.min.css'))
		.pipe(gulp.dest(webroot));
});

gulp.task('build-vendor-min-js', () => {
	return gulp
		.src(vendorScriptsMin)
		.pipe(concat('vendor.min.js'))
		.pipe(gulp.dest(webroot));
});
