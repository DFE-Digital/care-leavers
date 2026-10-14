import * as esbuild from "esbuild";
import {sassPlugin} from "esbuild-sass-plugin";
import {cpSync, rmSync, statSync} from "node:fs";

// -- Build CSS -- //
const scssFiles = ["application", "collection", "print", "rebrand"];

for (const scssFile of scssFiles) {
    await esbuild.build({
        entryPoints: [`scss/${scssFile}.scss`],
        bundle: true,
        minify: true,
        sourcemap: false,
        external: ['/assets/*'],
        plugins: [
            sassPlugin(),
        ],
        outfile: `out/css/${scssFile}.css`,
    });
}
// -- Build CSS -- //

// -- Copy JavaScript -- //
cpSync("./node_modules/govuk-frontend/dist/govuk/govuk-frontend.min.js", "./out/js/govuk-frontend.min.js", {force: true});
cpSync('./node_modules/dfe-frontend/dist/dfefrontend.min.js', './out/js/dfefrontend.min.js', {force: true});
// -- Copy JavaScript -- //

// -- Copy Assets -- //
const imagesGovUk = "./node_modules/govuk-frontend/dist/govuk/assets/images";
const fontsGovUk = "./node_modules/govuk-frontend/dist/govuk/assets/fonts";
const manifestGovUk = "./node_modules/govuk-frontend/dist/govuk/assets/manifest.json";

const imagesDfE = "./node_modules/dfe-frontend/packages/assets";

cpSync(imagesGovUk, "./out/assets/images", {force: true, recursive: true});
cpSync(fontsGovUk, "./out/assets/fonts", {force: true, recursive: true});
cpSync(manifestGovUk, "./out/assets/manifest.json", {force: true});

cpSync(imagesDfE, "./out/assets/images", {
    force: true,
    recursive: true,
    filter: (src) => {
        if (statSync(src).isDirectory()) return true;
        return src.endsWith(".png");
    }
});

cpSync("./images", "./out/assets", {force: true, recursive: true});
// -- Copy Assets -- //

// -- Copy to WWWROOT -- //
rmSync("../CareLeavers.Web/wwwroot", {force: true, recursive: true});
cpSync("./out", "../CareLeavers.Web/wwwroot", {force: true, recursive: true});
rmSync("./out", {force: true, recursive: true});
// -- Copy to WWWROOT -- //