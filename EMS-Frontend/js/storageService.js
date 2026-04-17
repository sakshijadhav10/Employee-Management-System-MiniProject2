const storageService = (function () {

  function getHeaders() {
    return {
      "Content-Type": "application/json",
      "Authorization": "Bearer " + authService.getToken()
    };
  }

  return {

    // ✅ IMPORTANT: accept URL
    async getAll(url) {
      const res = await fetch(`${CONFIG.API_BASE_URL}/${url}`, {
        method: "GET",
        headers: getHeaders()
      });

      if (!res.ok) {
        console.error("API ERROR:", res.status);
        return { data: [], totalCount: 0 };
      }

      const data = await res.json();
      return data;
    },
 async getById(url) {
      const response = await fetch(`${CONFIG.API_BASE_URL}/${url}`, {
        method: "GET",
        headers: getHeaders()
      });
      const data=await response.json();

      return data;
    
    },


    async add(url, data) {
      const res = await fetch(`${CONFIG.API_BASE_URL}/${url}`, {
        method: "POST",
        headers: getHeaders(),
        body: JSON.stringify(data)
      });

       if (res.status === 409) {
    const err = await res.json();
    return { success: false, message: err.message };
  }
      return await res.json();
    },

    async update(url, data) {
      const res = await fetch(`${CONFIG.API_BASE_URL}/${url}`, {
        method: "PUT",
        headers: getHeaders(),
        body: JSON.stringify(data)
      });
       if (res.status === 409) {
    const err = await res.json();
    return { success: false, message: err.message };
  }

  if (!res.ok) {
    return { success: false, message: "Update failed Email Already exists" };
  }

  return { success: true };
      // return await res.json();
    },

    async remove(url) {
      const res = await fetch(`${CONFIG.API_BASE_URL}/${url}`, {
        method: "DELETE",
        headers: getHeaders()
      });
      return await res.json();
    }

  };

})();