import React from "react";
import s from './Rank.module.css'
import Suit from "../Suit/Suit";

const Rank = ({suit,rank}) => {
  
    return (
        <div className={s.rank}>
            <div className={s.rank_high}>
               {rank}
            </div>

            <Suit  suit={suit}/>

            <div className={s.rank_low}>
            {rank}
            </div>
        </div>


    )
}

export default Rank