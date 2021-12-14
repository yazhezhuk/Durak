import React, {useEffect, useMemo, useState,} from "react";
import s from "./GamePlace.module.css";
import Player2 from "../Player2/Player2";
import Deck from "../Deck/Deck";
import Player1 from "../Player1/Player1";
import Button from "./Button/Button";
import PlayPlace from "../Deck/PlayPlace/PlayPlace";
import {useDispatch, useSelector} from "react-redux";
import {Attack} from "../../react-redux/gameSlice";

const GamePlace = ({ connection, game, gameStarted }) => {
  const { cards } = useSelector((state) => state.deck);
  const player1Hand = cards.slice(7, 13);
  const player2Hand = cards.slice(1, 7);
  const [attacker, setAttacker] = useState(null);
  const [deck, setDeck] = useState([{ rank: "R6", suit: "Diamonds" }]);
  const [trumpLear, setTrumpLear] = useState(null);
  const [opponentHandCount, setOpponentHandCount] = useState(0);
  const [playerHand, setPlayerHand] = useState([]);
  const [fool, setFool] = useState(null);
  const [openedCards, setOpenedCards] = useState([]);

  const dispatch = useDispatch();
  useEffect(() => {
    //const loadedGame = JSON.parse(localStorage.getItem("currentGame"));

    if (gameStarted) {
      updateGameState(game);
    }
  }, []);
  const handleCardClick = useMemo( () =>
    (card) => {
      dispatch(Attack({ card, gameId: game.gameId }))
    }, [dispatch])

  useEffect(() => {
    //const loadedGame = JSON.parse(localStorage.getItem("currentGame"));

    if (gameStarted) {
      if (connection) {
        connection.on("CardAddedToField", (card) => {
          console.log('Карта гроші 1 ствол: ',card)
          setOpenedCards((oldArr) => [...oldArr, card])
        });
        connection.on("CardRemovedFromHand", (card) => {
          console.log('Карта гроші мінус ствол: ',card)
          setPlayerHand((old) =>
          {
            const index = old.indexOf(card);
            console.log('Нашов: ',card, index)

            return old.filter(c => (c.lear !== card.lear) || (c.rank !== card.rank))
          })
        });
        connection.on("MovePassedTo", () => {
          console.log('Chel, mozhesh hodit: ')
        });
        connection.on("MovePassedFrom", () => {
          console.log('Chel, hod zavershen: ')
        });

        connection.on("InvalidActionOccured", (error) => {
          console.log('Chel, error: ', error)
        });

      }
    }
  }, [connection]);

  const [canShowTakeButton, setCanShowTakeButton] = useState(true);
  const [canShowHangUpButton, SetCanShowHangUpButton] = useState(false);

  const updateGameState = (game) => {
    setPlayerHand([...game.playerCards]);
    setOpponentHandCount(game.enemyCardCount);
    setTrumpLear(game.trumpLear);
  };

  const handleTakeCardsClick = () => {
    setCanShowTakeButton(false);
    SetCanShowHangUpButton(true);
  };
  const handleHangUpClick = () => {
    setCanShowTakeButton(true);
    SetCanShowHangUpButton(false);
  };

  

  const pairCards = useMemo(() => {
    console.log(openedCards);
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
      {gameStarted ? (
        <>
          <Player1 opponentCards={opponentHandCount} />
          <div className={s.cards}>
            <Deck trumpLear={trumpLear} />
            <PlayPlace pairs={pairCards} />
          </div>
          <div className={s.actionPanel}>
            {canShowTakeButton && (
              <Button label="Take it!" onClick={handleTakeCardsClick} />
            )}
            {canShowHangUpButton && (
              <Button label="Hand up!" onClick={handleHangUpClick} />
            )}
          </div>
          <Player2 cards={playerHand} onCardClick={handleCardClick} />
        </>
      ) : null}
    </div>
  );
};

export default GamePlace;
