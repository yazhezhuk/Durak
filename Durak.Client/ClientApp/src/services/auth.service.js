
const API_URL = "http://localhost:3000/api/auth/";


const  login  =  (username, password) => {
  
    const response =  fetch(API_URL,{
      method:'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept' : '*/*',
        'Accept-Encoding' : 'gzip, deflate, br',
        'Cache-Control' : 'no-cache',
        'Connection' : 'keep-alive'
       
      },
      body: JSON.stringify({Username:username,Password:password}) 
    }).catch(error => {console.log(error)}) 

    
    console.log(response)
    if (response.data.password) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }
  return response.data;
};

const logout = () => {
  localStorage.removeItem("user");
};

const authService = {
  login,
  logout,
};

export default authService;