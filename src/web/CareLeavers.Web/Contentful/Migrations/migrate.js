const fs = require("fs");
const path = require("path");
const { execSync } = require("child_process");
require('dotenv').config();
const process = require("process");
// Path to your settings.json file
const settingsPath = path.resolve("../../appsettings.json");

// Load settings.json
let settings;
try {
    settings = JSON.parse(fs.readFileSync(settingsPath, "utf-8"));
} catch (error) {
    console.error("Error reading settings.json:", error.message);
    process.exit(1);
}

// Extract Contentful options from settings.json
const contentfulOptions = settings.ContentfulOptions || {};

// Get Contentful secrets
const SPACE_ID = process.env.SPACE_ID;
const ENV_ID = process.env.EnvironmentId || "production";
const MANAGEMENT_TOKEN = process.env.MANAGEMENT_TOKEN

// Get migration file from command-line arguments
const migrationFile = process.argv[2];

if (!migrationFile) {
    console.error("Error: Please provide a migration file.");
    console.log("Usage: node migrate.js <migration-file.cjs>");
    process.exit(1);
}

// Run Contentful migration
const command = `contentful space migration --space-id "${SPACE_ID}" --environment-id "${ENV_ID}" --management-token "${MANAGEMENT_TOKEN}" "${migrationFile}"`;

try {
    console.log(`Running migration: ${migrationFile}...`);
    execSync(command, { stdio: "inherit" });
    console.log("Migration completed successfully!");
} catch (error) {
    console.error("Migration failed:", error.message);
    process.exit(1);
}
