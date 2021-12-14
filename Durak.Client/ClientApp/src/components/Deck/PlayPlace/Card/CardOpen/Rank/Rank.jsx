import React from "react";
import s from "./Rank.module.css";
import Suit from "../Suit/Suit";
import { RankFactory } from "../../../../../../scripts/enums";

const Rank = ({ suit, rank }) => {
  
    let parsedRank = '';

  (rank === "") ? parsedRank = '' : (parsedRank = RankFactory(rank));
  return (
    <div className={s.rank}>
      <div className={s.rank_high}>{parsedRank.slice(1)  }</div>

      <Suit suit={suit} />

      <div className={s.rank_low}>{parsedRank.slice(1) }</div>
    </div>
  );
};

export default Rank;
