"use strict";

const gulp = require("gulp"),
    sass = require("gulp-sass")(require('sass'))
    


let paths = {
    src: 'AssetSrc/',
    dist: 'wwwroot/'
}

gulp.task('govuk-assets', function() {
    return gulp.src('node_modules/govuk-frontend/dist/govuk/assets/**/*')
        .pipe(gulp.dest(paths.dist + 'assets'));
});

gulp.task('govuk-js', function() {
    return gulp.src('node_modules/govuk-frontend/dist/govuk/*.js', { sourcemaps: true })
        .pipe(gulp.dest(paths.dist + 'js'));
});

gulp.task('govuk-css', function() {
    return gulp.src('node_modules/govuk-frontend/dist/govuk/*.css', { sourcemaps: true })
        .pipe(gulp.dest(paths.dist + 'css'));
});

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

gulp.task("sass", function () {
    return gulp.src(paths.src + '/scss/**/*.scss')
        .pipe(sass({
            style: 'compressed',
            includePaths: 'node_modules'
        }))
        .pipe(gulp.dest(paths.dist + '/css'))
        // .pipe(connect.reload());
});

gulp.task("images", function() {
    return gulp.src(paths.src + '/assets/**/*', {encoding:false})
        .pipe(gulp.dest(paths.dist + 'assets'));
})

gulp.task("dev",
    gulp.series(
        "govuk-assets",
        "govuk-js",
        "govuk-css",
        "dfe-js",
        "dfe-css",
        "dfe-assets",
        "images",
        "sass"
    )
);

gulp.task('default', gulp.series('dev'));