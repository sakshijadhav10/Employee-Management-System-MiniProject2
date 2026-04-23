const uiService = (function () {
  return {
    // Render Employee Table
  renderEmployeeTable(result) {

  console.log(" Render Table Result:", result);

  const employees = result.data ;
  const totalCount = result.totalCount || employees.length;

  const tbody = $("#employeeTableBody");
  tbody.empty();

  if (!employees || employees.length === 0) {
    tbody.append(`<tr><td colspan="10" class="text-center">No Employees Found</td></tr>`);
    return;
  }

  // Count text
  $("#employeeCount").text(
    `Showing ${employees.length} of ${totalCount} employees`
  );

  // Rows
  employees.forEach(emp => {

    const initials = emp.firstName.charAt(0) + emp.lastName.charAt(0);
    
      // Department badge class
      const deptClass = emp.department.toLowerCase();

        // Status badge class
      const statusClass = emp.status.toLowerCase();

        // Format salary
        const salaryFormatted = emp.salary.toLocaleString("en-IN");
          const row = `
        <tr>


<td>${emp.id}</td>
<td>
  <div class="avatar-circle">
    ${initials}
  </div>
</td>

<td class="fw-semibold">
  ${emp.firstName} ${emp.lastName}
</td>

<td class="text-muted">
  ${emp.email}
</td>

<td>
  <span class="badge badge-dept ${deptClass}">
    ${emp.department}
  </span>
</td>

<td>${emp.designation}</td>

<td class="fw-semibold">₹${salaryFormatted}</td>

<td>${new Date(emp.joinDate).toLocaleDateString("en-GB")}</td>

<td>
  <span class="badge badge-status ${statusClass}">
    ${emp.status}
  </span>
</td>

<td>
  <div class="action-btns">
    <button class="btn btn-outline-primary btn-sm small-btn view-btn" 
      data-id="${emp.id || emp.Id}">
      <i class="bi bi-eye" style="font-size:10px;"></i>
    </button>

    <button class="btn btn-outline-warning btn-sm small-btn edit-btn" 
      data-id="${emp.id || emp.Id}">
      <i class="bi bi-pencil" style="font-size:10px;"></i>
    </button>

    <button class="btn btn-outline-danger btn-sm small-btn delete-btn" 
      data-id="${emp.id || emp.Id}">
      <i class="bi bi-trash" style="font-size:10px;"></i>
    </button>
  </div>
</td>

</tr>
        `;

    tbody.append(row);
      // this.renderPagination(result);
  });
console.log("FULL RESULT:", result);
console.log("FIRST EMP:", result.data[0]);
  //  IMPORTANT: pagination call
  this.renderPagination(result);
  
},


    // Dashboard KPI Cards
    renderDashboardCards(data) {
      $("#totalEmployees").text(data.totalEmployees);
      $("#activeEmployees").text(data.activeEmployees);
      $("#inactiveEmployees").text(data.inactiveEmployees);
      $("#departmentCount").text(data.totalDepartments);
    },

    // Department Breakdown
    renderDepartmentBreakdown(data) {
      const tbody = $("#departmentBreakdown");
      tbody.empty();

      
 let total = data.reduce((sum, item) => sum + item.count, 0);
      const colors = ["primary", "info", "warning", "success", "secondary"];

      let i = 0;
     

     data.forEach((item,i)=>{
      
        let percentage = ((item.count / total) * 100).toFixed(0);
        let color = colors[i % colors.length];

        const row = `
      <tr>
        <td>
          <span class="badge bg-${color}">${item.department}</span>
        </td>

        <td class="text-center fw-bold">${item.count}</td>

        <td style="width: 50%;">
          <div class="progress" style="height:8px;">
            <div class="progress-bar bg-${color} " style="width:${percentage}%"></div>
          </div>
        </td>

        <td style="font-weight:400;font-size:12px;color:grey;">${percentage}%</td>
      </tr>
    `;

        tbody.append(row);
        i++;
      });
    },

    // Recent Employees
    renderRecentEmployees(employees) {
      const list = $("#recentEmployees");
      list.empty();

      employees.forEach((emp) => {
        //initials
        const initials = emp.firstName.charAt(0) + emp.lastName.charAt(0);
        const departmentcolors = {
          Engineering: "primary",
          HR: "info",
          Marketing: "warning",
          Finance: "success",
          Operations: "secondary",
        };

        const color = departmentcolors[emp.department];

        const item = `
        <li class="list-group-item d-flex justify-content-between align-item-center">
       <div class="d-flex align-items-center gap-3">

    <div class="avatar-circle">
      ${initials}
    </div>

    <div class="mb-0">
      <div class="fw-bold mb-0">${emp.firstName} ${emp.lastName}</div>
      <small class="text-muted d-block">${emp.designation}</small>
    </div>

  </div>

          <div class="d-flex gap-3">

    <span class="badge badge-custom bg-${color}">${emp.department}</span>
    <span class="badge badge-custom ${emp.status === "Active" ? "bg-success" : "bg-danger"}">
      ${emp.status}
    </span>
  </div>
        </li>
        
        `;

        list.append(item);
      });
    },
renderPagination(result) {

  const container = $("#paginationContainer");
  container.empty();

  const totalPages = Math.ceil(result.totalCount/ _state.pageSize);
//  const totalPages = result.totalPages;
  console.log("Total Pages:", totalPages);
  console.log("Current Page:", _state.page);

  if (totalPages <= 1) return;
let html = `<ul class="pagination pagination-sm mb-0">`;

  // 🔹 Previous Button
  html += `
    <li class="page-item ${_state.page === 1 ? "disabled" : ""}">
      <button class="page-link page-btn" data-page="${_state.page - 1}">
        &laquo;
      </button>
    </li>
  `;

  // 🔹 Page Numbers
  for (let i = 1; i <= totalPages; i++) {

    html += `
      <li class="page-item ${i === _state.page ? "active" : ""}">
        <button class="page-link page-btn" data-page="${i}">
          ${i}
        </button>
      </li>
    `;
  }

  // 🔹 Next Button
  html += `
    <li class="page-item ${_state.page === totalPages ? "disabled" : ""}">
      <button class="page-link page-btn" data-page="${_state.page + 1}">
        &raquo;
      </button>
    </li>
  `;

  html += `</ul>`;

  container.append(html);
  },


    // Show Modal
    showModal(type, data) {
     if (type === "add") {
  $("#employeeModalTitle").text("Add Employee");
  this.clearForm();

  // $("#employeeId").val("");
  $("#saveEmployeeBtn").text("Save Employee");
}
if (type === "edit") {
  $("#employeeModalTitle").text("Edit Employee");
  this.populateForm(data);
  $("#saveEmployeeBtn").text("Update Employee");
}
     if (type === "view") {
        const colors = {
          Engineering: "#0d6efd",
          HR: "#0dcaf0",
          Marketing: "#ffc107",
          Finance: "#198754",
          Operations: "#6c757d",
        };
        const initials = data.firstName.charAt(0) + data.lastName.charAt(0);
        console.log("Employee data for viewing:", data);
        console.log("Initials:", initials);
        $("#viewAvatar").text(initials);
        $("#viewName").text(data.firstName + " " + data.lastName);
        $("#viewEmail").text(data.email);
        $("#viewPhone").text(data.phone);
        $("#viewDepartment").text(data.department).css({
          backgroundColor: colors[data.department],
          color: "#fff",
        });
        $("#viewDesignation").text(data.designation);
        $("#viewSalary").text(data.salary);
         $("#viewJoinDate").text(
  data.joinDate ? new Date(data.joinDate).toLocaleDateString() : "N/A"
)
   
        $("#viewStatus").text(data.status);

        //Status classes
        const statusEl = $("#viewStatus");

        statusEl.text(data.status);

        // Remove old classes first
        statusEl.removeClass("status-active status-inactive");

        // Add new class based on status
        if (data.status === "Active") {
          statusEl.addClass("status-active");
        } else {
          statusEl.addClass("status-inactive");
        }

        $("#viewEmployeeModal").modal("show");
        return;
      }

      $("#employeeModal").modal("show");
    },

    // Populate Form
    populateForm(employee) {
      $("#employeeId").val(employee.id);
      $("#firstName").val(employee.firstName);
      $("#lastName").val(employee.lastName);
      $("#email").val(employee.email);
      $("#phone").val(employee.phone);
      $("#department").val(employee.department);
      $("#designation").val(employee.designation);
      $("#salary").val(employee.salary);
      // $("#joinDate").val(employee.joinDate);
      
 $("#joinDate").val(
    employee.joinDate ? employee.joinDate.split("T")[0] : ""
  );
      console.log(employee.joinDate);
      $("#status").val(employee.status);
    },

    // Clear Form
    clearForm() {
      $("#employeeForm")[0].reset();
      $("#employeeId").val("");

      $(".error-message").text("");
    },

    // Show Inline Validation Errors
    showInlineErrors(errors) {
      $(".error-message").text("");

      for (let field in errors) {
        $(`#${field}Error`).text(errors[field]);
      }
    },

    // Toast Notification
    showToast(message, type) {
      const toastHTML = `
      <div class="toast align-items-center text-bg-${type} border-0 show" role="alert">
        <div class="d-flex">
          <div class="toast-body">
            ${message}
          </div>
          <button type="button" class="btn-close btn-close-white me-2 m-auto"
           data-bs-dismiss="toast"></button>
        </div>
      </div>
      `;

      $("#toastContainer").append(toastHTML);

      setTimeout(() => {
        $("#toastContainer .toast").remove();
      }, 3000);
    },
  };
})();
