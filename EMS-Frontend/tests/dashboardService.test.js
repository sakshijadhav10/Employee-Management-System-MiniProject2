const dashboardService = require("../js/dashboardService");

beforeEach(() => {
  global.fetch = jest.fn();
  global.CONFIG = {
    API_BASE_URL: "https://localhost:7088/api"
  };

  global.authService = {
    getToken: jest.fn(() => "fake-token")
  };
});

test("getSummary success", async () => {
  fetch.mockResolvedValue({
    ok: true,
    json: () =>
      Promise.resolve({
        totalEmployees: 3,
        activeEmployees: 2,
        inactiveEmployees: 1,
        totalDepartments: 2,
        departmentBreakdown: [],
        recentEmployees: []
      })
  });

  const result = await dashboardService.getSummary();

  expect(result.totalEmployees).toBe(3);
  expect(result.activeEmployees).toBe(2);
});

test("getSummary failure returns default", async () => {
  fetch.mockResolvedValue({
    ok: false
  });

  const result = await dashboardService.getSummary();

  expect(result.total).toBe(0);
});