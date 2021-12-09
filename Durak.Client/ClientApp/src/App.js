import './App.css';
import GamePlace from "./components/GamePlace/GamePlace";
import {HttpTransportType, HubConnectionBuilder} from '@microsoft/signalr';

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
      <GamePlace />
    </div>
  );
}

export default App;
