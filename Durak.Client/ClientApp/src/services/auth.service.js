
const API_URL = "http://localhost:3000/api/auth/";


const  login  =  async (username, password) => {

    const response = await fetch(API_URL, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'text/plain',
            'Accept-Encoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'
        },
        body: JSON.stringify({Username: username, Password: password})
    }).catch(error => {
        console.log(error)
    })

    const token = await response.json()
    console.log(token)
    if (token) {
        const data = await JSON.stringify(token)
        localStorage.setItem("user",  data);
    }
    return JSON.stringify(token)
};

const logout = () => {
  localStorage.removeItem("user");
};

const authService = {
  login,
  logout,
};

export default authService;
