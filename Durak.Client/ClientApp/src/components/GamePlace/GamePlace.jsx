import React, { useEffect, useMemo, useState } from "react";
import s from "./GamePlace.module.css";
import Player2 from "../Player2/Player2";
import Deck from "../Deck/Deck";
import Player1 from "../Player1/Player1";
import Button from "./Button/Button";
import PlayPlace from "../Deck/PlayPlace/PlayPlace";
import { useDispatch, useSelector } from "react-redux";
import { Attack, PassTurn, tryDefendCard } from "../../react-redux/gameSlice";

const GamePlace = ({ connection, game, gameStarted, setGame }) => {
  const FROM_ATTACKER_HAND = "from-attacker-plays";
  const FROM_PLAYER_HAND = "from-player-hand";
  const { cards } = useSelector((state) => state.deck);
  const [attacker, setAttacker] = useState(true);
  const [trumpLear, setTrumpLear] = useState(null);
  const [opponentHandCount, setOpponentHandCount] = useState(0);
  const [playerHand, setPlayerHand] = useState([]);
  const [fool, setFool] = useState(null);
  const [openedCards, setOpenedCards] = useState([]);

  const [selectedAttackCard, setSelectedAttackCard] = useState({
    rank: "",
    lear: "",
  });
  const [selectedDefendCard, setSelectedDefendCard] = useState({
    rank: "",
    lear: "",
  });

  const dispatch = useDispatch();
  useEffect(() => {
    console.clear();
    const loadedGame = localStorage.getItem("currentGame");
    console.log(loadedGame);
    if (gameStarted) {
      updateGameState(game);
    }
  }, []);
  const handleCardClick = useMemo(
    () => (card) => {
      dispatch(Attack({ card, gameId: game.gameId }));
    },
    [dispatch]
  );

  const [canShowTakeButton, setCanShowTakeButton] = useState(false);
  const [canShowPassMoveButton, SetCanShowPassMoveButton] = useState(true);

  const handleTakeCardsClick = () => {
    setCanShowTakeButton(false);
    SetCanShowPassMoveButton(true);
  };
  const handlePassClick = () => {
    dispatch(PassTurn({ gameId: game.gameId }));
    SetCanShowPassMoveButton(true);
  };

  useEffect(() => {
    //const loadedGame = JSON.parse(localStorage.getItem("currentGame"));

    if (gameStarted) {
      if (connection) {
        connection.on("CardAddedToField", (card) => {
          console.log("Карта гроші 1 ствол: ", card);
          setOpenedCards((oldArr) => [...oldArr, card]);
        });
        connection.on("CardRemovedFromHand", (card) => {
          console.log("Карта гроші мінус ствол: ", card);
          setPlayerHand((old) => {
            return old.filter(
              (c) => c.lear !== card.lear || c.rank !== card.rank
            );
          });
        });
        connection.on("MovePassedTo", () => {
          console.log("Chel, mozhesh hodit: ");
        });
        connection.on("MovePassedFrom", () => {
          console.log("Chel, hod zavershen: ");
        });

        connection.on("InvalidActionOccured", (error) => {
          console.log("Chel, error: ", error);
        });
        // return () => {
        //   connection.off("CardAddedToField");
        //   connection.off("CardRemovedFromHand");
        //   connection.off("MovePassedTo");
        //   connection.off("MovePassedFrom");
        //   connection.off("InvalidActionOccured");
        // };
      }
    }
  }, [connection, gameStarted]);

  const updateGameState = (game) => {
    setPlayerHand([...game.playerCards]);
    setOpponentHandCount(game.enemyCardCount);
    setTrumpLear(game.trumpLear);
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
  }, [attacker, openedCards]);

  const handleAttackCard = (fromComponent) => {
    if (fromComponent === FROM_ATTACKER_HAND &&
      selectedDefendCard.lear !== "" &&
      selectedDefendCard.rank !== "") {
      dispatch(
        tryDefendCard({
          enemyCard: selectedAttackCard,
          playerCard: selectedDefendCard,
          gameId: game.gameId,
        })
      );
    } else if (
      fromComponent === FROM_PLAYER_HAND &&
      selectedAttackCard.lear !== "" &&
      selectedAttackCard.rank !== ""
    ) {
      dispatch(
        tryDefendCard({
          enemyCard: selectedAttackCard,
          playerCard: selectedDefendCard,
          gameId: game.gameId,
        })
      );
    }
  };

  return (
    <div className={s.gamePlace}>
      {gameStarted ? (
        <>
          <Player1 opponentCards={opponentHandCount} />
          <div className={s.cards}>
            <Deck trumpLear={trumpLear} />
            <PlayPlace
              pairs={pairCards}
              handleAttackCard={handleAttackCard}
              attacker={attacker}
              setSelectedAttackCard={setSelectedAttackCard}
            />
          </div>
          <div className={s.actionPanel}>
            {canShowTakeButton && (
              <Button label="Take it!" onClick={handleTakeCardsClick} />
            )}
            {canShowPassMoveButton && (
              <Button label="Pass move!" onClick={handlePassClick} />
            )}
          </div>
          <Player2
            cards={playerHand}
            onCardClick={handleCardClick}
            handleAttackCard={handleAttackCard}
            attacker={attacker}
            setSelectedDefendCard={setSelectedDefendCard}
          />
        </>
      ) : null}
    </div>
  );
};

export default GamePlace;
