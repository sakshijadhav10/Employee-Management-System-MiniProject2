const employeeService = (function () {

  return {

    async getAll(params) {

      const query = new URLSearchParams({
        page: params.page || 1,
        pageSize: params.pageSize || 5,
        search: params.search || "",
        department: params.department || "",
        status: params.status || "",
            sortBy: params.sortBy || "id",
    sortDir: params.sortDir || "desc"
      });

      const url = `employees?${query.toString()}`;

      console.log("🔥 FINAL API URL:", url);

      return await storageService.getAll(url);
    },

    async getById(id) {
      return await storageService.getById(`employees/${id}`);
    },

    async add(data) {
      return await storageService.add("employees", data);
    },

    async update(id, data) {
      return await storageService.update(`employees/${id}`, data);
    },

    async remove(id) {
      return await storageService.remove(`employees/${id}`);
    }

  };

})();
module.exports = employeeService;