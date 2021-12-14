import React from "react";
import s from './Player2.module.css'
import CardOpen from "../Deck/PlayPlace/Card/CardOpen/CardOpen";

const Player2 = ({cards,onCardClick}) => {
    return (
        <div className={s.player2}>
            
            {cards.map((card,index) => (
                <div className={s.card} key={index} onClick={() => {onCardClick({lear: card.lear, rank: card.rank })}}>
                <CardOpen rank={card.rank} suit={card.lear} />
                    

                </div>
            ))}


        </div>
    )
}

export default Player2
