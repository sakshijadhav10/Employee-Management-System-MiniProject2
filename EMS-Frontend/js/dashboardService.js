



const dashboardService = (function () {

  async function getSummary() {
    try {
      const response = await fetch(`${CONFIG.API_BASE_URL}/Employees/dashboard`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer " + authService.getToken()
        }
      });

      if (!response.ok) {
        throw new Error("Failed to fetch dashboard data");
      }

      const data = await response.json();
      console.log("Dashboard Data:", data);
      return data;


      /*
        Expected response from API:

        {
          total: 15,
          active: 10,
          inactive: 5,
          departments: 5,
          departmentBreakdown: { Engineering: 4, HR: 3, ... },
          recentEmployees: [ ... ]
        }
      */

    } catch (error) {
      console.error("Dashboard Error:", error);
      return {
        total: 0,
        active: 0,
        inactive: 0,
        departments: 0,
        departmentBreakdown: {},
        recentEmployees: []
      };
    }
  }

  return {
    getSummary
  };

})();

module.exports = dashboardService;