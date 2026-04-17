let store = {};

global.localStorage = {
  getItem: jest.fn((key) => store[key] || null),
  setItem: jest.fn((key, value) => {
    store[key] = value;
  }),
  clear: jest.fn(() => {
    store = {};
  })
};

global.sessionStorage = {
  getItem: jest.fn(),
  setItem: jest.fn(),
  removeItem: jest.fn()
};
global.CONFIG = {
  API_BASE_URL: "https://localhost:7088/api"
};
const authService = require("../js/authService");

beforeEach(() => {
  jest.clearAllMocks();
});

test("signup success", async () => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: true,
      json: () => Promise.resolve({})
    })
  );

  const res = await authService.signup("admin", "admin123");

  expect(res.success).toBe(true);
});

test("signup duplicate username", async () => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: false
    })
  );

  const res = await authService.signup("admin", "admin123");

  expect(res.success).toBe(false);
});

test("login success", async () => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: true,
      json: () =>
        Promise.resolve({
          token: "abc123",
          username: "admin",
          role: "Admin"
        })
    })
  );

  const res = await authService.login("admin", "admin123");

  expect(res.success).toBe(true);
  expect(authService.isLoggedIn()).toBe(true);
});

test("login failure", async () => {
  global.fetch = jest.fn(() =>
    Promise.resolve({
      ok: false
    })
  );

  const res = await authService.login("wrong", "wrong");

  expect(res.success).toBe(false);
});

test("logout clears session", () => {
  authService.logout();

  expect(authService.isLoggedIn()).toBe(false);
});