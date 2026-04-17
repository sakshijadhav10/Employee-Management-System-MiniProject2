const _state = {
  page: 1,
  pageSize: 10,
  sortBy:"id",
  sortDir:"desc"
};

$(document).ready(function () {

  init();

  function init() {
    bindAuthEvents();
    if(authService.isLoggedIn()){
      $("#loginView").addClass("d-none");
      $("#signupView").addClass("d-none");
      $("#dashboardView").removeClass("d-none");
        $("body").removeClass("login-bg");
          $("body").addClass("app-bg");
          setLoggedUser();
          applyRoleUI();
          //  $("#dashboardView").removeClass("d-none");
      loadDashboard();
     loadEmployees();
       
    }
  }

  function bindAuthEvents() {

    // SIGNUP
    $("#signupForm").submit(async function (e) {

      e.preventDefault();

      const formData = {
        username: $("#signupUsername").val(),
  password: $("#signupPassword").val(),
  confirmPassword: $("#confirmPassword").val()
      };
  
      const errors = validationService.validateAuthForm(formData);

      if (Object.keys(errors).length > 0) {
        uiService.showInlineErrors(errors);
        return;
      }

      const result = await authService.signup(formData.username, formData.password);

      if (!result.success) {
        uiService.showToast(result.message, "danger");
        return;
      }

      uiService.showToast("Signup successful", "success");

      $("#signupView").addClass("d-none");
      $("#loginView").removeClass("d-none");

    });


    // LOGIN
    $("#loginForm").submit(async function (e) {

  e.preventDefault();

  const formData = {
    username: $("#loginUsername").val(),
    password: $("#loginPassword").val()
  };

  const errors = validationService.validateLoginForm(formData);

  if (Object.keys(errors).length > 0) {
    uiService.showInlineErrors(errors);
    return;
  }

  const result =await authService.login(formData.username, formData.password);

  if (!result.success) {
    uiService.showToast(result.message, "danger");
    return;
  }

  $("#loginView").addClass("d-none");

  $("#dashboardView").removeClass("d-none");
  $("#mainNavbar").removeClass("d-none");
  $("body").removeClass("login-bg");
    $("body").addClass("app-bg");
  uiService.showToast("Signin successful", "success");
setLoggedUser();
applyRoleUI();
  loadDashboard();
  loadEmployees();

});
//Logout
$("#logoutBtn").click(function () {

    authService.logout();
  $("#loggedInUser").text("");
    $("#dashboardView").addClass("d-none");
    $("#loginView").removeClass("d-none");
   $("body").removeClass("app-bg");
  $("body").addClass("login-bg");
    uiService.showToast("Logged out successfully", "success");

});

    // PAGE TOGGLE
    $("#showSignup").click(function (e) {
      e.preventDefault();
      $("#loginView").addClass("d-none");
      $("#signupView").removeClass("d-none");
    });

    $("#showLogin").click(function (e) {
      e.preventDefault();
      $("#signupView").addClass("d-none");
      $("#loginView").removeClass("d-none");
    });

  }
function setLoggedUser(){
  const user=authService.getCurrentUser();

  if(user && user.username){
    $("#loggedInUser").text(user.username);
  }
}
function applyRoleUI() {
  const role = authService.getRole();
console.log("User Role:", role);
   if (role && role.toLowerCase() === "viewer") {
     $(
      "#addEmployeeBtnDashboard, #addEmployeeBtnTable, #addEmployeeBtnNavbar"
    ).addClass("d-none");

    $(".edit-btn, .delete-btn").addClass("d-none");
  }
  else {
    
    $(
      "#addEmployeeBtnDashboard, #addEmployeeBtnTable, #addEmployeeBtnNavbar"
    ).removeClass("d-none");

    $(".edit-btn, .delete-btn").removeClass("d-none");
  }
  
};



async function loadDashboard() {

  const data = await dashboardService.getSummary();
console.log("Dashboard Summary:", data);
  // KPI Cards
  uiService.renderDashboardCards(data);

  // Department Breakdown
  uiService.renderDepartmentBreakdown(data.departmentBreakdown);

  // Recent Employees
  uiService.renderRecentEmployees(data.recentEmployees);

  applyRoleUI();
};

  /* -----------------------------
     LOAD EMPLOYEES
  ------------------------------*/
  
async function loadEmployees() {
  
const result = await employeeService.getAll({
  page: _state.page,
  pageSize: _state.pageSize,
  search: $("#searchInput").val() || "",
  department:  $("#departmentFilter").val()||"",
  status:  $("#statusFilter .active").data("status")||"",
   sortBy: _state.sortBy,
    sortDir: _state.sortDir
});
  

  uiService.renderEmployeeTable(result);
  applyRoleUI();
};

/*Add Pagination*/
 $(document).on("click", ".page-btn", function () {

  _state.page = Number($(this).data("page"));

  loadEmployees();
});

  /* -----------------------------
     ADD EMPLOYEE BUTTON
  ------------------------------*/

$(document).on("click", "#addEmployeeBtnDashboard, #addEmployeeBtnTable,#addEmployeeBtnNavbar", function () {
  uiService.showModal("add");
});

  /* -----------------------------
     SAVE EMPLOYEE
  ------------------------------*/

  $("#employeeForm").submit(async function (e) {

    e.preventDefault();

    const formData = {
      firstName: $("#firstName").val().trim(),
      lastName: $("#lastName").val().trim(),
      email: $("#email").val().trim(),
      phone: $("#phone").val().trim(),
      department: $("#department").val(),
      designation: $("#designation").val().trim(),
      salary: $("#salary").val(),
      joinDate: $("#joinDate").val(),
      status: $("#status").val()
    };

    const errors = validationService.validateEmployeeForm(formData);

    if (Object.keys(errors).length > 0) {
      uiService.showInlineErrors(errors);
      return;
    }

    const id = $("#employeeId").val();
    console.log("Form Data to Save:", formData, "Employee ID:", id);
    if (id) {
      const result=await employeeService.update(Number(id), formData);
      if(!result.success){
        uiService.showToast(result.message, "danger");
        return;
      }
      uiService.showToast("Employee updated", "success");
    } else {
      const result = await employeeService.add(formData);

if (!result.success) {
  uiService.showToast(result.message, "danger");
  return;
}

uiService.showToast("Employee added", "success");
    }

    $("#employeeModal").modal("hide");

    await loadEmployees();
    
    await loadDashboard();

  });


  /* -----------------------------
     VIEW EMPLOYEE
  ------------------------------*/
$(document).on("click", ".view-btn", async function () {

  const id = Number($(this).data("id"));

  const employee = await employeeService.getById(id); // ✅ await

  uiService.showModal("view", employee);

});


  /* -----------------------------
     EDIT EMPLOYEE
  ------------------------------*/

  $(document).on("click", ".edit-btn", async function () {

  const id = Number($(this).data("id"));

  const employee = await employeeService.getById(id); // ✅ await

  uiService.showModal("edit", employee);

});


  /* -----------------------------
     DELETE EMPLOYEE
  ------------------------------*/

  let deleteId = null;

$(document).on("click", ".delete-btn", async function () {

  deleteId = Number($(this).data("id"));

  const emp = await employeeService.getById(deleteId); // ✅ await

  if (!emp) return;

  $("#deleteEmployeeName").text(emp.firstName + " " + emp.lastName);

  $("#deleteModal").modal("show");
});

// Confirm Delete
$("#confirmDeleteBtn").click(async function () {

  if (!deleteId) return;

  await employeeService.remove(deleteId); // ✅ await

  deleteId = null;

  $("#deleteModal").modal("hide");

  uiService.showToast("Employee deleted", "danger");

  loadEmployees();
  loadDashboard();

});


  /* -----------------------------
     SEARCH EMPLOYEE
  ------------------------------*/

  $("#searchInput").on("keyup", function () {
    _state.page=1;
    loadEmployees();

  });


  /* -----------------------------
     FILTER BY DEPARTMENT
  ------------------------------*/

  $("#departmentFilter").change(function () {

    _state.page=1;
    loadEmployees();

  });


  /* -----------------------------
     FILTER BY STATUS
  ------------------------------*/
$("#statusFilter button").click(function () {

    // remove active from all
    $("#statusFilter button").removeClass("active");

    // add active to clicked button
    $(this).addClass("active");

    // get selected status
    // const status = $(this).data("status");
_state.page=1;
loadEmployees();
  

});

//Sort Employee
$(document).on("click", ".sort-icon", function () {
// let sortDirection = {}; 
  const field = $(this).data("field");
 // toggle direction
  if (_state.sortBy === field) {
    _state.sortDir = _state.sortDir === "asc" ? "desc" : "asc";
  } else {
    _state.sortBy = field;
    _state.sortDir = "asc";
  }

  // reset icons
  $(".sort-icon")
    .removeClass("bi-arrow-up bi-arrow-down")
    .addClass("bi-arrows-vertical");

  // set current icon
  if (_state.sortDir === "asc") {
    $(this)
      .removeClass("bi-arrows-vertical")
      .addClass("bi-arrow-up");
  } else {
    $(this)
      .removeClass("bi-arrows-vertical")
      .addClass("bi-arrow-down");
  }

  loadEmployees(); // ✅ API call

});

});