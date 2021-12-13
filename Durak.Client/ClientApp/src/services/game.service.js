const API_URL = "http://localhost:3000/api/game/";


const  connectGame  =  async (name,token) => {


    const response = await fetch(API_URL + 'connect', {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'text/plain',
            'Accept-Encoding': 'gzip, deflate, br',
            'Connection': 'keep-alive',
            'Authorization': `Bearer ${token}`

        },
        body: JSON.stringify({Name:name})
    }).catch(error => {
        console.log(error)
    })
    const responseObj = await response.json()
    console.log(responseObj)
    return JSON.stringify(responseObj)
}
const createGame  = async (name,token) => {
console.log(token)
    const response = await fetch(API_URL + 'create', {
        method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'text/plain',
                'Accept-Encoding': 'gzip, deflate, br',
                'Connection': 'keep-alive',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({Name:name})
    }).catch(error => {
        console.log(error)
    })
    const responseObj = await response.json()
    console.log(responseObj)
    if (responseObj) {
        const data = await JSON.stringify(responseObj)
        localStorage.setItem("user",  data);
    }
    return JSON.stringify(responseObj)
}

const gameService = {
    connectGame,
    createGame,
}
export default gameService
