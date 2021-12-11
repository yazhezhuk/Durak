import './App.css';
import GamePlace from './components/GamePlace/GamePlace';
import Login from "./components/Login/Login";
import GameList from './components/GameList/GameList';
import { Route, Routes } from 'react-router-dom';
import { HttpTransportType, HubConnectionBuilder } from '@microsoft/signalr';



function App() {
    const connection = new HubConnectionBuilder()
        .withUrl('/gameHub',{
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets})
        .withAutomaticReconnect()
        .build()
    ;
    connection.start()
        .then(result => {
            console.log('Connected!');

            connection.on('ReceiveMethod', someShite => {
                window.alert(someShite)
            });
        })
        .catch(e => console.log('Connection failed: ', e));

    return (
    <div className="App">

      <Routes>
        <Route exact path='/' element={<GamePlace />}/>
        <Route path='/profile' element={ <GameList />}/>
        <Route path='/login' element={<Login />}/>
      </Routes>



    </div>
  );
}

export default App;
