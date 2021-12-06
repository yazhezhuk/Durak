import React from "react";
import s from './CardClose.module.css'
import cardClose from '../../../../assets/img/CardClose.png'


const CardClose = () => {
    return (
        <div className={s.cardClose}>
            <img src={cardClose}/>
        </div>

    )
}

export default CardClose