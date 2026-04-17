const validationService = (function () {
  return {
    // Authentication Form Vaidation
    // Sign Up Form Validation
    validateAuthForm(formData) {
      const errors = {};

      // Required fields
      if (!formData.username || formData.username.trim() === "") {
        errors.username = "Username is required";
      }
      if (!formData.password) {
        errors.password = "Password is required";
      }
      if (!formData.confirmPassword) {
        errors.confirmPassword = "Confirm password is required";
      }

      // Password length
      if (formData.password && formData.password.length < 6) {
        errors.password = "Password must be at least 6 characters";
      }

      // Password match
      if (
        formData.password &&
        formData.confirmPassword &&
        formData.password !== formData.confirmPassword
      ) {
        errors.confirmPassword = "Passwords do not match";
      }

      return errors;
    },
    // Sign In Form Validation
    validateLoginForm(formData) {
      const errors = {};

      if (!formData.username || formData.username.trim() === "") {
        errors.username = "Username is required";
      }

      if (!formData.password) {
        errors.password = "Password is required";
      }

      return errors;
    },

    // Employee table Validation
    validateEmployeeForm(data) {
      const errors = {};

      if (!data.firstName) errors.firstName = "First name required";
      if (!data.lastName) errors.lastName = "Last name required";

      if (!data.email) errors.email = "Email required";
      else if (!/\S+@\S+\.\S+/.test(data.email))
        errors.email = "Invalid email format";

      if (!data.phone) {
        errors.phone = "Phone is required";
      } else if (!/^\d{10}$/.test(data.phone)) {
        errors.phone = "Phone must be exactly 10 digits and contain only numbers";
      }

      if (!data.department) errors.department = "Select department";
      if(!data.designation) errors.designation = "Designation required";
      if (!data.salary || data.salary <= 0)
        errors.salary = "Salary must be positive";

      if (!data.joinDate) {
        errors.joinDate = "Join date required";
      } else {
          const today = new Date();
          const joinDate = new Date(data.joinDate);

   
          today.setHours(0, 0, 0, 0);
          joinDate.setHours(0, 0, 0, 0);

        if (joinDate > today) {
            errors.joinDate = "Join date cannot be greater than today's date";
        }
      }

      if (!data.status) errors.status = "Select status";

      return errors;
    },
    mapServerErrors(response) {
  if (response.status === 409) {
    return { email: "Email already exists" };
  }
  return {};
}
  };
})();
