import React, { useCallback, useEffect } from "react";
import s from './GameList.module.css'
import { useDispatch, useSelector } from "react-redux";
import { Navigate } from 'react-router-dom';
import { logout } from "../../react-redux/ProfileSlices/authSlice";
import EventBus from '../../common/EventBus'

const GameList = () => {
    const {user: currentUser} = useSelector(state => state.auth)
    const dispatch = useDispatch();
    console.log(localStorage)
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
    // if (!currentUser) {
    //     return <Navigate  to="/login" />;
    //   }
    return (
        <div className={s.gameList}>
            <header className={s.userData}>
                <div>
                     <p>Player <span><strong></strong></span></p>
                <p>Password <span><strong></strong></span></p>
                </div>
                <div>
                    <button onClick={logOut}>logOut</button>
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