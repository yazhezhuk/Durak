import React from "react";
import s from './PlayPlace.module.css'
import CardOpen from "./Card/CardOpen/CardOpen";



const PlayPlace = ({pairs}) => {
    return (
        <div className={s.playPlace}>
            
            {pairs.map(pair => (
                <div className={s.cardPair}>
                    {pair.map((card, index) => (
                        <div className={s.card} key={index}>
                        <CardOpen rank={card.rank} suit={card.lear}  /> 
                        </div> 
                    ) )}
                
            </div>
            ) )}
           
               

        </div>
    )
}

export default PlayPlace