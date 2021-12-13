import "./App.css";
import GamePlace from "./components/GamePlace/GamePlace";
import Login from "./components/Login/Login";
import GameList from "./components/GameList/GameList";
import { Navigate, Route, Routes } from "react-router-dom";


import { useEffect, useRef, useState } from "react";
import { HubConnectionBuilder,HttpTransportType } from "@microsoft/signalr";

const App = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const [token,setToken] = useState('')

  const [connection, setConnection] = useState(null);



    useEffect(() => {

        const newConnection = new HubConnectionBuilder()
            .withUrl("http://localhost:3000/gameHub", {
            })
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);

    }, []);

    useEffect(() =>{
        if(connection){
            connection.start()
                .then(() => {console.log('Connection started!')
                    if(connection)
                        connection.on('StartGameIntegrationEvent', game => {
                            console.log(game)
                        }
                        )})
                .catch(err => console.log('Error while establishing connection :('  ));
        }

    }, [connection])

  return (
      <div className="App">
        <Routes>
          <Route exact path="/" element={<Navigate to='/game' />} />
          <Route path='/game' element={<GamePlace connection={connection}/>}/>
          <Route path="/profile" element={<GameList isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />} />
          <Route path="/login" element={<Login isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} setToken={setToken}  />} />
        </Routes>
      </div>
  );
};

export default App;
