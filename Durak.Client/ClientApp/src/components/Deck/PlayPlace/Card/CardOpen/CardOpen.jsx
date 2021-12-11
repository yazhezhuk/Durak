import React from "react";
import s from "./CardOpen.module.css";
import Rank from "./Rank/Rank";

const CardOpen = ({ rank, suit, onCardClick }) => {
  

  return (
    <div
      className={s.cardOpen}
      onClick={() => {
        onCardClick({id:`card-${rank}-${suit}`, rank,suit});
      }}
    >
      <Rank rank={rank} suit={suit} />
    </div>
  );
};

export default CardOpen;
