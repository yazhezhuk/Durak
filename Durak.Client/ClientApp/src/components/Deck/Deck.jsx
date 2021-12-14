import React from "react";
import s from './Deck.module.css'
import BunchCards from "./BunchCards/BunchCards";
import Trump from "./Trump/Trump";


const Deck = ({trumpLear}) => {

    return (
        <div className={s.deck}>
           
            <BunchCards deckSize={24}/>
            <Trump trumpLear={trumpLear} /> 

        </div>
    )
}

export default Deck

