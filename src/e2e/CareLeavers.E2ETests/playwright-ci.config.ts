import { defineConfig, devices } from '@playwright/test';
import * as dotenv from 'dotenv';

// Load environment variables from .env file
dotenv.config();

export default defineConfig({
    testDir: './src/tests',
    testMatch: '**/*.spec.ts',
    timeout: 20 * 1000, // 10 seconds
    expect: {
        timeout: 5000, // 5 seconds
    },
    fullyParallel: true,
    retries: 1,
    workers: 4,
    reporter: [['list'], ['html', { open: 'never' }]],
    use: {
        trace: 'on-first-retry',
        screenshot: 'only-on-failure',
        video: 'retain-on-failure',
        baseURL: 'https://localhost:7050/',
        ignoreHTTPSErrors: true
    },
    projects: [
        {
            name: 'Chromium',
            use: { browserName: 'chromium' },
        },
        /*Removing this to reduce the number of repetative tests
        {
            name: 'Firefox',
            use: { browserName: 'firefox' },
        },
        {
            name: 'WebKit',
            use: { browserName: 'webkit' },
        },*/
        {
            name: 'Mobile Chrome',
            use: {
                browserName: 'chromium',
                ...devices['Pixel 8'],
                isMobile: true,
                viewport: { width: 412, height: 914 }, 
                userAgent: devices['Pixel 8']?.userAgent ?? devices['Pixel 7']?.userAgent, 
            },
        },
        {
            name: 'Mobile Safari',
            use: {
                browserName: 'webkit',
                ...devices['iPhone 15'],
            },
        },
    ],
});
