import React from "react";
import s from './Rank.module.css'
import Suit from "../Suit/Suit";

const Rank = () => {
    return (
        <div className={s.rank}>
            <div className={s.rank_high}>
                7
            </div>

            <Suit />

            <div className={s.rank_low}>
                8
            </div>
        </div>


    )
}

export default Rank