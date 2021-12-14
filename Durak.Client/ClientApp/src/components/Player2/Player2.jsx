import React, { useState } from "react";
import s from "./Player2.module.css";
import CardOpen from "../Deck/PlayPlace/Card/CardOpen/CardOpen";
import { useDispatch } from "react-redux";
import { tryDefendCard } from "../../react-redux/gameSlice";

const Player2 = ({ cards, setDefendCard,attacker, onCardClick,defendCard,gameId }) => {
    const [isClicked, setIsClicked] = useState({lear: '', rank:''});
const dispatch = useDispatch()
  const handleClick = (card) => {
    const {enemyCard,playerCard } = defendCard
    setIsClicked(card);
    setDefendCard((prevState) => ({
      enemyCard: prevState.enemyCard,
      playerCard: card,
    }));
    if(enemyCard && playerCard){
        dispatch(tryDefendCard({enemyCard,playerCard,gameId}))
    }
  };
  return (
    <div className={s.player2}>
      {cards.map((card, index) => (
        <div
          className={(card.lear !== isClicked.lear || card.rank !== isClicked.rank) ? s.card : s.cardClicked}
          key={index}
          onClick={() => {
            attacker
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
