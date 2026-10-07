module.exports = {
  testEnvironment: 'jsdom',
  rootDir: '../',
  testMatch: ['**/__tests__/**/*.test.js', '**/js/tests/**/*.test.js'],
  collectCoverageFrom: [
    'CareLeavers.Web/wwwroot/js/**/*.js',
    '!CareLeavers.Web/wwwroot/js/**/*.min.js',
    '!CareLeavers.Web/wwwroot/js/tests/**/*.test.js',
  ],
  coveragePathIgnorePatterns: [
    '/node_modules/',
    '.*\\.test\\.js$',
  ],
  coverageReporters: ['text', 'text-summary', 'html', 'json', 'lcov'],
  moduleNameMapper: {
    '\\.(css|scss)$': '<rootDir>/jest.mock.js',
  },
  transform: {
    '^.+\\.js$': 'babel-jest',
  },
};
