import "./App.css";
import GamePlace from "./components/GamePlace/GamePlace";
import Login from "./components/Login/Login";
import GameList from "./components/GameList/GameList";
import { Navigate, Route, Routes } from "react-router-dom";
import { user } from "./react-redux/ProfileSlices/authSlice";

import { useEffect, useRef, useState } from "react";
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";

const App = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false); //хз чи воно тут треба
  const [connection, setConnection] = useState(null);

  const [gameStarted, setGameStarted] = useState(false);
  const [game, setGame] = useState(null);
  useEffect(() => {
    if (user) {
      const newConnection = new HubConnectionBuilder()
        .withUrl("http://localhost:3000/gameHub", {
          accessTokenFactory: () => user.token,
        })
        .withAutomaticReconnect()
        .build();

      setConnection(newConnection);
    }
  }, []);

  useEffect(() => {

    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("Connection started!");
          if (connection)
             connection.on("GameStarted", (game) => {
               
                   localStorage.setItem("currentGame", game); 
                
              
              console.log(game);
              setGameStarted(true);
              setGame(game);
            });
        })
        .catch((err) => console.log("Error while establishing connection :("));
    }
    // return () => {
    //   if (connection) {
    //     connection.off("GameStarted");
    //   }
    // };
  }, [connection]);

  return (
    <div className="App">
      <Routes>
        <Route exact path="/" element={<Navigate to="/login" />} />
        <Route
          path="/game"
          element={
            <GamePlace
              connection={connection}
              game={game}
              gameStarted={gameStarted}
              setGame= {setGame}
            />
          }
        />
        <Route
          path="/profile"
          element={
            <GameList isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
          }
        />
        <Route
          path="/login"
          element={
            <Login isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
          }
        />
      </Routes>
    </div>
  );
};

export default App;
