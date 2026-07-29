// merge.config.ts
import { defineConfig } from '@playwright/test';

export default defineConfig({
  // Tell Playwright to use your custom yaml-reporter.ts during the merge
  reporter: [['./yaml-reporter.ts']],
});