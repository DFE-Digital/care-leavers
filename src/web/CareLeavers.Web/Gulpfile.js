"use strict";

const gulp = require("gulp"),
    sass = require("gulp-sass")(require('sass'))
    


let paths = {
    src: 'AssetSrc/',
    dist: 'wwwroot/'
}

gulp.task('dfe-js', function() {
    return gulp.src('node_modules/dfe-frontend/dist/*.js')
        .pipe(gulp.dest(paths.dist + 'js'));
});

gulp.task('dfe-css', function() {
    return gulp.src('node_modules/dfe-frontend/dist/*.css')
        .pipe(gulp.dest(paths.dist + 'css'));
});
gulp.task('dfe-assets', function() {
    return gulp.src('node_modules/dfe-frontend/packages/assets/**/*', {encoding:false})
        .pipe(gulp.dest(paths.dist + 'assets'));
});

gulp.task('gov-assets', function() {
    return gulp.src('node_modules/dfe-frontend/packages/assets/**/*', {encoding:false})
        .pipe(gulp.dest(paths.dist + 'assets'));
});

gulp.task("sass", function () {
    return gulp.src(paths.src + '/scss/**/*.scss')
        .pipe(sass({
            includePaths: 'node_modules'
        }))
        .pipe(gulp.dest(paths.dist + '/css'))
        // .pipe(connect.reload());
});

gulp.task("dev",
    gulp.series(
        "dfe-js",
        "dfe-css",
        "dfe-assets",
        "sass"
    )
);

gulp.task('default', gulp.series('dev'));