import React from "react";
import s from './Deck.module.css'
import BunchCards from "./BunchCards/BunchCards";
import Trump from "./Trump/Trump";


const Deck = ({deck}) => {

    return (
        <div className={s.deck}>
           
            <BunchCards deckSize={deck.length - 13}/>
            <Trump trump={deck[0]} /> 

        </div>
    )
}

export default Deck

