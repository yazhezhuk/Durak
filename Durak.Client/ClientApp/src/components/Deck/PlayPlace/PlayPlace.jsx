import React, { useState } from "react";
import s from "./PlayPlace.module.css";
import CardOpen from "./Card/CardOpen/CardOpen";

const PlayPlace = ({ pairs, handleAttackCard, attacker,setSelectedAttackCard }) => {
    const FROM_ATTACKER_HAND = "from-attacker-plays";


  const [selectedCard, setSelectedCard] = useState({ lear: "", rank: "" });

  const handleClick = (card) => {
    setSelectedAttackCard(card)
    handleAttackCard(FROM_ATTACKER_HAND, card);
    setSelectedCard(card);
  };
  return (
    <div className={s.playPlace}>
      {pairs.map((pair, index) => (
        <div className={s.cardPair} key={index}>
          {pair.map((card, index) => (
            <div
              className={
                (attacker && JSON.stringify(card)) !==
                JSON.stringify(selectedCard)
                  ? s.card
                  : s.selectedCard
              }
              key={`${index}-${card.rank}-${card.lear}`}
              onClick={
               attacker
                  ? handleClick({ lear: card.lear, rank: card.rank })
                  : ()=>{}
              }
            >
              <CardOpen rank={card.rank} suit={card.lear} />
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};

export default PlayPlace;
