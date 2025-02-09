const { execSync } = require("child_process");

// Extract Contentful options from settings.json
// const contentfulOptions = settings.ContentfulOptions || {};

// Get Contentful secrets
const SPACE_ID = process.env.SPACE_ID;
const ENV_ID = process.env.ENVIRONMENT_ID || "production";
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
