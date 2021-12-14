import React, { useState } from "react";
import s from "./Player2.module.css";
import CardOpen from "../Deck/PlayPlace/Card/CardOpen/CardOpen";

const Player2 = ({ cards, onCardClick, handleAttackCard, attacker,setSelectedDefendCard }) => {
    const FROM_PLAYER_HAND = "from-player-hand";

  const [selectedCard, setSelectedCard] = useState({ lear: "", rank: "" });

  const handleClick = (card) => {
    setSelectedDefendCard(card)
    setSelectedCard(card);
    handleAttackCard(FROM_PLAYER_HAND);
  };
  return (
    <div className={s.player2}>
      {cards.map((card, index) => (
        <div
          className={
            JSON.stringify(card)  !== JSON.stringify(selectedCard)  ? s.card : s.selectedCard
          }
          key={index}
          onClick={() => {
            !attacker
              ? onCardClick({ lear: card.lear, rank: card.rank })
              : handleClick({ lear: card.lear, rank: card.rank });
          }}
        >
          <CardOpen rank={card.rank} suit={card.lear} />
        </div>
      ))}
    </div>
  );
};

export default Player2;
