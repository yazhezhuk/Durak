const API_URL = "http://localhost:3000/api/game/";

const  create  =  async (name) => {

    const response = await fetch(API_URL + 'connect', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'Accept-Encoding': 'gzip, deflate, br',
            'Cache-Control': 'no-cache',
            'Connection': 'keep-alive'

        },
        body: JSON.stringify({Name:name})
    }).catch(error => {
        console.log(error)
    })
    console.log(await response.json())
    if (response.data.password) {
        localStorage.setItem("userToken", JSON.stringify(response.data));
    }
    return response.data;
}
