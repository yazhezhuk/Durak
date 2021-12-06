import React from "react";
import s from './CardOpen.module.css'
import Rank from "./Rank/Rank";


const CardOpen = () => {
    return (
        <div className={s.cardOpen}>
            <Rank />
        </div>
    )
}

export default CardOpen