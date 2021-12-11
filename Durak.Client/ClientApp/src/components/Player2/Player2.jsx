import React from "react";
import s from './Player2.module.css'
import CardOpen from "../Deck/PlayPlace/Card/CardOpen/CardOpen";

const Player2 = ({cards,onCardClick}) => {
   
    return (
        <div className={s.player2}>
            {cards.map((card) => (
                <div className={s.card} key={card.id}>
                <CardOpen rank={card.rank} suit={card.suit} onCardClick={onCardClick}/>
                </div>
            ))}


        </div>
    )
}

export default Player2