const employeeService = require("../js/employeeService");

beforeEach(() => {
  global.storageService = {
    getAll: jest.fn(),
    getById: jest.fn(),
    add: jest.fn(),
    update: jest.fn(),
    remove: jest.fn()
  };
});

test("getAll calls storageService with query", async () => {
  storageService.getAll.mockResolvedValue({ data: [], totalRecords: 0 });

  const res = await employeeService.getAll({
    page: 1,
    pageSize: 5
  });

  expect(storageService.getAll).toHaveBeenCalled();
});

test("getById calls storageService", async () => {
  storageService.getById.mockResolvedValue({ id: 1 });

  const res = await employeeService.getById(1);

  expect(storageService.getById).toHaveBeenCalledWith("employees/1");
});

test("add employee", async () => {
  storageService.add.mockResolvedValue({});

  await employeeService.add({ firstName: "Test" });

  expect(storageService.add).toHaveBeenCalledWith("employees", { firstName: "Test" });
});

test("update employee", async () => {
  storageService.update.mockResolvedValue({});

  await employeeService.update(1, { firstName: "Updated" });

  expect(storageService.update).toHaveBeenCalledWith("employees/1", { firstName: "Updated" });
});

test("remove employee", async () => {
  storageService.remove.mockResolvedValue({});

  await employeeService.remove(1);

  expect(storageService.remove).toHaveBeenCalledWith("employees/1");
});