module.exports = {
  testEnvironment: 'jsdom',
  rootDir: '../',
  testMatch: ['**/__tests__/**/*.test.js', '**/js/tests/**/*.test.js'],
  collectCoverageFrom: [
    'CareLeavers.Web.Node/js/**/*.js',
    '!CareLeavers.Web.Node/js/**/*.min.js',
    '!CareLeavers.Web.Node/js/tests/**/*.test.js',
    '!CareLeavers.Web.Node/__tests__/**/*.test.js',
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
