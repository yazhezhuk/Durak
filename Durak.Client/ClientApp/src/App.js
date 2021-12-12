import "./App.css";
import GamePlace from "./components/GamePlace/GamePlace";
import Login from "./components/Login/Login";
import GameList from "./components/GameList/GameList";
import { Navigate, Route, Routes } from "react-router-dom";


import { useState } from "react";

const App = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false)

  const [token, setToken] = useState('')
  // const connection = new HubConnectionBuilder()
  //     .withUrl('/gameHub',{
  //         skipNegotiation: true,
  //         transport: HttpTransportType.WebSockets})
  //     .withAutomaticReconnect()
  //     .build()
  // ;
  // connection.start()
  //     .then(result => {
  //         console.log('Connected!');

  //         connection.on('ReceiveMethod', someShite => {
  //             window.alert(someShite)
  //         });
  //     })
  //     .catch(e => console.log('Connection failed: ', e));

  return (
    <div className="App">
      <Routes>
        <Route exact path="/" element={<Navigate to='/game' />} />
        <Route path='/game' element={<GamePlace/>}/>
        <Route path="/profile" element={<GameList isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />} />
        <Route path="/login" element={<Login isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} setToken={setToken}  />} />
      </Routes>
    </div>
  );
};

export default App;
