var gulp = require("gulp");
var del = require("del");
var browserSync = require("browser-sync");
var sass = require("gulp-ruby-sass");
var prefixer = require("gulp-autoprefixer");
var sourcemaps = require("gulp-sourcemaps");
var webpack = require("webpack");
var karma = require("karma").server;
var bower = require("gulp-bower-deps")({
  deps: {
    "bootstrap-sass-official": {
      version: "3.3.x",
      files: ""
    },
    "angular": {
      version: "1.2.x",
      files: ""
    },
    "angular-mocks": {
      version: "1.2.x",
      files: ""
    },
  }
});

bower.installtask(gulp);

gulp.task("default", ["dev"]);

gulp.task("clean", function(done) {
  del(["public/js", "public/css"], done);
});

gulp.task("dev", ["css-debug", "browser-sync", "tdd"], function(done) {
  gulp.watch("frontend/scss/*.scss", ["css-debug"]);
  webpack({
    entry: {
      app: ["./bower_components/angular/angular.min.js", "./frontend/js/app.js"]
    },
    output: {
      path: "public/js",
      filename: "app.js"
    },
    cache: true,
    watch: true,
    devtool: "source-map",
    module: {
      loaders: [
        {test: /\.html$/, loader: "html?attr=''"}
      ]
    }
  }, function(err, stats) {
    console.log(stats.toString({colors: true}));
  });
});

gulp.task("prod", ["clean", "css", "js"]);

gulp.task("browser-sync", function() {
  browserSync({
//    server: {baseDir: "public"},
    proxy: "localhost:8000",
    files: ["public/**/*.html", "public/**/*.css", "public/**/*.js"]
  });
});

gulp.task("css-debug", function() {
  return sass("frontend/scss", {sourcemap: true, style: "expanded"})
    .pipe(prefixer())
    .pipe(sourcemaps.write())
    .pipe(gulp.dest("public/css"));
});

gulp.task("css", function() {
  return sass("frontend/scss", {style: "compressed"})
    .pipe(prefixer())
    .pipe(gulp.dest("public/css"));
});

gulp.task("js", function() {
  webpack({
    entry: "./frontend/js/app.js",
    output: {
      path: "public/js",
      filename: "app.js"
    },
    cache: true,
    plugins: [
      new webpack.optimize.UglifyJsPlugin(),
      new webpack.optimize.OccurenceOrderPlugin(),
    ]
  }, function(err, stats) {
    console.log(stats.toString({colors: true}));
  });
});

gulp.task("test", function(done) {
  karma.start({
    configFile: __dirname + "/karma.conf.js",
    singleRun: true
  }, done);
});

gulp.task("tdd", function() {
  karma.start({
    configFile: __dirname + "/karma.conf.js"
  });
});
