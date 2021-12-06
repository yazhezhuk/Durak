import React from "react";
import s from './Suit.module.css'
import Spades from  './../../../../../../assets/suitsIMG/Spades.svg'

const Suit = () => {
    return (
        <div className={s.suit}>
            <img src={Spades} />
        </div>
    )
}

export default Suit