import React from "react";
import s from "./BunchCards.module.css";
import CardClose from "../PlayPlace/Card/CardClose";

const BunchCards = ({ deckSize }) => {
  return (
     <div className={s.bunchCards}>
          <CardClose />
    </div>
  );
};

export default BunchCards;
