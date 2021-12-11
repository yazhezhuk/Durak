import React, { useCallback, useEffect, useState } from "react";
import s from './GameList.module.css'
import { useDispatch, useSelector } from "react-redux";
import { Navigate, NavLink } from 'react-router-dom';
import { logout } from "../../react-redux/ProfileSlices/authSlice";
import EventBus from '../../common/EventBus'
import CreateGame from "./CreateGame/CreateGame";


const GameList = () => {


    const {user} = useSelector(state => state.auth)
    console.log(user)
    const {games,setGames} = useState([])

    const dispatch = useDispatch();
    
  
    const logOut = useCallback(() => {
        dispatch(logout());
      }, [dispatch]);
    
      
      useEffect(() => {
        EventBus.on("logout", () => {
            logOut();
          });
      
          return () => {
            EventBus.remove("logout");
          }
      }, [logOut])
    if (!user) {
        return <Navigate  to="/login" />;
      }
    return (
        <div className={s.gameList}>
            <header className={s.userData}>
                <div>
                     <p>Player </p>

                </div>
                <div>
                    <button onClick={() => {logOut()}}>logOut</button>
                </div>
                <div>
                    <CreateGame token={user.token} setGames={setGames} />
                </div>
            </header>
            <h3 className={s.h3}>
                Game list
            </h3>
            
            <form className={s.gameList_inner}>
                <input className={s.playerList} type='text' name='text' />
                <input className={s.startGame} type='button' name='startGame' value='Start game' />
            </form>
        </div>
    )
}

export default GameList