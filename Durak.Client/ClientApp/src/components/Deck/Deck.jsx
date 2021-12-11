import React from "react";
import s from './Deck.module.css'
import BunchCards from "./BunchCards/BunchCards";
import Trump from "./Trump/Trump";

import {deck} from '../../scripts'

const Deck = () => {
    return (
        <div className={s.deck}>
           
            <BunchCards deckSize={deck.length - 13}/>
            <Trump trump={deck[0]} /> 

        </div>
    )
}

export default Deck

