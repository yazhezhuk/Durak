import React from "react";
import s from './GamePlace.module.css'
import Player2 from "../Player2/Player2";
import Deck from "../Deck/Deck";
import Player1 from "../Player1/Player1";

const GamePlace = () => {
    return (
        <div className={s.gamePlace}>
            <Player1 />
            <Deck />
            <Player2 />
        </div>

    )
}

export default GamePlace