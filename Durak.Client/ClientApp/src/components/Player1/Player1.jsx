import React from "react";
import s from './Player1.module.css'
import CardClose from "../Deck/PlayPlace/Card/CardClose";

const Player1 = ({opponentCards}) => {
    return (
        <div className={s.player1}>

        {new Array(opponentCards).fill(1).map((card, indx) => (
            <div className={s.card} key={indx}>
            <CardClose  />
            </div>
        ))}
        </div>
    )
}

export default Player1