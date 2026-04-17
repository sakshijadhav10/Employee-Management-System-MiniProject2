

const authService = (function () {
  let token=null;
  let username=null;
  let role=null;

  return{
    async login(usernameInput,password){
      const res=await fetch(`${CONFIG.API_BASE_URL}/auth/login`,{
        method:"POST",
        headers:{
          "Content-Type":"application/json"
        },
        body:JSON.stringify(
          {username:usernameInput,
            password:password}
        )
      });
      if(!res.ok){
        return {success:false,message:"Invalid username or password "};
      }
      const data=await res.json();
      token=data.token;
      username=data.username;
      role=data.role;
      return {success:true};
    },
 async signup(username, password) {
  const res = await fetch(`${CONFIG.API_BASE_URL}/auth/register`, {  
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ username, password })
  });

  const data = await res.json();

  if (!res.ok) {
    return {
      success: false,
      message: data.message || "Signup failed"
    };
  }

  return {
    success: true,
    message: data.message || "Account created successfully"
  };
},
    getToken(){
      return token;
    },
    
    getRole(){
      return role;
    },
    getCurrentUser(){
      return {username, role};
    },
    isLoggedIn(){
  return token !== null;
},
    logout(){
      token=null;
      username=null;
      role=null;
      return {success:true,message:"Logged out successfully"};
    }
  }
})();
module.exports = authService;