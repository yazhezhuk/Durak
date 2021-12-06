import React from "react";
import s from './Player2.module.css'
import CardOpen from "../Deck/PlayPlace/Card/CardOpen/CardOpen";

const Player2 = () => {
    return (
        <div className={s.player2}>
            <CardOpen />
            <CardOpen />
            <CardOpen />
            <CardOpen />
            <CardOpen />


        </div>
    )
}

export default Player2