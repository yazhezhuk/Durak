import React from "react";
import s from './GameList.module.css'

const GameList = () => {
    return (
        <div className={s.gameList}>
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