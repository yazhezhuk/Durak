import React from "react";
import s from './PlayPlace.module.css'
import CardOpen from "./Card/CardOpen/CardOpen";



const PlayPlace = ({pairs}) => {
    return (
        <div className={s.playPlace}>
            
            {pairs.map((pair,index) => (
                <div className={s.cardPair} key={index}>
                    {pair.map((card, index) => (
                        <div className={s.card} key={`${index}-${card.rank}-${card.lear}`}>
                        <CardOpen rank={card.rank} suit={card.lear}  /> 
                        </div> 
                    ) )}
                
            </div>
            ) )}
           
               

        </div>
    )
}

export default PlayPlace