import './App.css';
import GamePlace from './components/GamePlace/GamePlace';
import Login from "./components/Login/Login";
import GameList from './components/GameList/GameList';
import { Route, Routes } from 'react-router-dom';


function App() {
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
