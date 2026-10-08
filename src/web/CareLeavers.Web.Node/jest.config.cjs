module.exports = {
  testEnvironment: 'jsdom',
  rootDir: '../',
  testMatch: ['**/__tests__/**/*.test.js', '**/js/tests/**/*.test.js'],
  coverageDirectory: 'CareLeavers.Web.Node/coverage',
  collectCoverageFrom: [
    '**/*.js',
    '!**/node_modules/**',
    '!**/dist/**',
    '!**/*.min.js',
    '!**/js/tests/**/*.test.js',
    '!**/__tests__/**',
  ],
  coveragePathIgnorePatterns: [
    '/node_modules/',
    String.raw`.*\.test\.js$`,
  ],
  coverageReporters: ['text', 'text-summary', 'html', 'json', 'lcov'],
  moduleNameMapper: {
    [String.raw`\.(css|scss)$`]: '<rootDir>/jest.mock.js',
  },
  transform: {
    [String.raw`^.+\.js$`]: 'babel-jest',
  },
};
