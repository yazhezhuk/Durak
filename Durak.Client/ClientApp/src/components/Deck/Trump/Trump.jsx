import React from "react";
import CardOpen from "../PlayPlace/Card/CardOpen/CardOpen";
import s from './Trump.module.css'

const Trump = ({trump}) =>{
    return (
        <div className={s.trump}>
            <CardOpen suit={"trump.suit"} rank={''} />
        </div>
    )
}

export default Trump

