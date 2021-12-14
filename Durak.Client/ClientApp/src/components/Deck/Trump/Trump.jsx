import React from "react";
import CardOpen from "../PlayPlace/Card/CardOpen/CardOpen";
import s from './Trump.module.css'
import { LearFactory } from "../../../scripts/enums";

const Trump = ({trumpLear}) =>{
    return (
        <div className={s.trump}>
            <CardOpen suit={trumpLear} rank={''} />
        </div>
    )
}

export default Trump

