import React from "react";
import s from './PlayPlace.module.css'
import CardOpen from "./Card/CardOpen/CardOpen";
import CardClose from "./Card/CardClose";


const PlayPlace = () => {
    return (
        <div className={s.playPlace}>
            <CardOpen />

        </div>
    )
}

export default PlayPlace