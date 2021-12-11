import React, { useEffect, useMemo, useState } from "react";
import s from "./GamePlace.module.css";
import Player2 from "../Player2/Player2";
import Deck from "../Deck/Deck";
import Player1 from "../Player1/Player1";
import Button from "./Button/Button";
import PlayPlace from "../Deck/PlayPlace/PlayPlace";
import { useDispatch, useSelector } from "react-redux";
import { initCards,shuffle, setTrump } from "../../react-redux/deckSlice";
import { setDeck } from "../../react-redux/bankerSlice";

const GamePlace = () => {
  const { cards } = useSelector((state) => state.deck);
  const player1Hand = cards.slice(7, 13);
  const player2Hand = cards.slice(1, 7);

  const [attacker, setAttacker] = useState(null);
  const [deckLength, setDeckLength] = useState(0);
  const [trumpCard, setTrumpCard] = useState(null);
  const [opponentHand, setOpponentHand] = useState([]);
  const [playerHand, setPlayerHand] = useState([]);
  const [fool, setFool] = useState(null);
  const [openedCards, setOpenedCards] = useState([]);
   
   
   const dispatch = useDispatch()

  useEffect(() => {
    dispatch(initCards())
    dispatch(shuffle())
    dispatch(setTrump())
    
    
    
  }, [dispatch])
 

  const [canShowTakeButton, setCanShowTakeButton] = useState(true)
  const [canShowHangUpButton, SetCanShowHangUpButton] = useState(false)
  const pairs = []

  const cardPair1= cards.slice(14, 16);
  const cardPair2= cards.slice(17, 19);
  const cardPair3= cards.slice(19, 20);
 

  pairs.push(cardPair1)
  pairs.push(cardPair2)
  pairs.push(cardPair3)
  


  const handleTakeCardsClick = () => {
    setCanShowTakeButton(false)
    SetCanShowHangUpButton(true)
  };
  const handleHangUpClick = () => {
    setCanShowTakeButton(true)
    SetCanShowHangUpButton(false)
  };

  const handleCardClick = (card) => {
  
      setOpenedCards(oldArr =>[...oldArr, card])
   
    
    
  };

  const pairCards = useMemo(() => {
    console.log(openedCards)
    debugger
    return openedCards.reduce((acc, card) => {
      const len = acc.length - 1;

      if (!acc[len] || acc[len].length === 2) {
        acc.push([card]);
      } else {
        acc[len].push(card);
      }

      return acc;
    }, []);
  }, [openedCards]);

  return (
    <div className={s.gamePlace}>
      <Player1 opponentCards={player1Hand} />

      <div className={s.cards}>
        <Deck />
        <PlayPlace pairs={pairCards}  />

      </div>

    <div className={s.actionPanel}>
      {canShowTakeButton && (
        <Button label="Take it!" onClick={handleTakeCardsClick} />
      )}
      {canShowHangUpButton && (
        <Button label="Hand up!" onClick={handleHangUpClick} />
      )}
      </div>
      <Player2 cards={player2Hand} onCardClick={handleCardClick} />
    </div>
  );
};

export default GamePlace;
