import React from "react";
import s from './Player1.module.css'
import CardClose from "../Deck/PlayPlace/Card/CardClose";

const Player1 = () => {
    return (
        <div className={s.player1}>
            <CardClose />
        </div>
    )
}

export default Player1