import React from "react";
import s from './WaitPlace.module.css'

const WaitPlace = () => {
    return (
        <div className={s.waitPlace}>
            <h3 className={s.h3}>Name</h3>
            <textarea className={s.players}>

            </textarea>
            <input className={s.startGame} type='button' name='startGame' value='Start Game'/>
        </div>
    )
}

export default WaitPlace