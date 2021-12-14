import React from "react";
import s from "./CardOpen.module.css";
import Rank from "./Rank/Rank";

const CardOpen = ({ rank, suit }) => {
  

  return (
    <div
      className={s.cardOpen}
    >
      <Rank rank={rank} suit={suit} />
    </div>
  );
};

export default CardOpen;
