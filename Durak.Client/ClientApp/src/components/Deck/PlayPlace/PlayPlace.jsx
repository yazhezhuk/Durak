import React, { useState } from "react";
import s from "./PlayPlace.module.css";
import CardOpen from "./Card/CardOpen/CardOpen";
import { useDispatch } from "react-redux";
import { tryDefendCard } from "../../../react-redux/gameSlice";

const PlayPlace = ({ pairs, setDefendCard,defendCard,gameId }) => {
  const [isClicked, setIsClicked] = useState({lear: '', rank:''});
  const dispatch = useDispatch();
  const handleClick = (card) => {
    const {enemyCard,playerCard } = defendCard
    setIsClicked(card);
   
    setDefendCard((prevState) => ({
      enemyCard:card ,
      playerCard: prevState.playerCard,
    }));
    if(enemyCard && playerCard){
      dispatch(tryDefendCard({enemyCard,playerCard,gameId}))
    }

  };
  return (
    <div className={s.playPlace}>
      {pairs.map((pair, index) => (
        <div className={s.cardPair} key={index}>
          {pair.map((card, index) => (
            <div
              className={ (card.lear !== isClicked.lear || card.rank !== isClicked.rank)  ? s.card : s.cardClicked}
              key={`${index}-${card.rank}-${card.lear}`}
              onClick={() => {
                handleClick({ lear: card.lear, rank: card.rank });
              }}
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
