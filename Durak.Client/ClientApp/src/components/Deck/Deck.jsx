import React from "react";
import s from './Deck.module.css'
import BunchCards from "./BunchCards/BunchCards";
import Trump from "./Trump/Trump";
import PlayPlace from "./PlayPlace/PlayPlace";


const Deck = () => {
    return (
        <div className={s.deck}>
            <BunchCards />
            <Trump />
            <PlayPlace />
        </div>
    )
}

export default Deck

