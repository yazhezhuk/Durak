import React, {memo, useCallback, useEffect, useState} from "react";
import s from "./GameList.module.css";
import {useDispatch, useSelector} from "react-redux";
import {Navigate, NavLink} from "react-router-dom";
import {logout} from "../../react-redux/ProfileSlices/authSlice";
import EventBus from "../../common/EventBus";
import CreateGame from "./CreateGame/CreateGame";
import {connectGame, getAllGames} from "../../react-redux/gameSlice";

const GameList = (props) => {
    const {user} = useSelector((state) => state.auth);
    const [loading, setLoading] = useState(false);
    const [games, setGames] = useState([]);
    const [selectedGame, setSelectedGame] = useState(null);
    const dispatch = useDispatch();
    const [isConnect, setIsConnect] = useState(false);

    useEffect(() => {
        setLoading(true)
        dispatch(getAllGames())
            .unwrap()
            .then((response) => {
                setGames(response.games);
                setLoading(false)
            });
    }, []);

    const logOut = useCallback(() => {
        dispatch(logout());
    }, [dispatch]);

    const getAll = () => {
        setLoading(true);
        dispatch(getAllGames())
            .unwrap()
            .then((allGames) => {
                const items = allGames.games;
                setGames(items);
                setLoading(false);
            });
    };

    useEffect(() => {
        EventBus.on("logout", () => {
            logOut();
        });

        return () => {
            EventBus.remove("logout");
        };
    }, [logOut]);

    if (!user) {
      return <Navigate to="/login" />;
    }
    if (isConnect) {
        return <Navigate to="/game"/>;
    }

    const handleSelectedGame = (game) => {
        setSelectedGame(game);
    };
    const executeConnect = async (name) => {
        const resultAction = await dispatch(connectGame({name}));
        if (connectGame.fulfilled.match(resultAction)) {
            console.log("user successfully connected");
        }
        setIsConnect(true);
    };
    return (
        <div className={s.gameList}>
            <style>
                @import url('https://fonts.googleapis.com/css2?family=Open+Sans&display=swap');
            </style>
            <div className={s.userData}>
                <header className={s.userData_header}>
                    <h1 className={s.userData_h1}>Player {'user.token'} </h1>
                </header>
                <main className={s.main}>
                    <div>
                        <CreateGame
                            setLoading={setLoading}
                            loading={loading}
                            games={games}
                            setGames={setGames}
                        />
                    </div >
                    <div className={s.gamesContainer}>
                        
                        {games ?
                            games.map((game, indx) => {
                                return (
                                    <div
                                        onClick={() => {
                                            handleSelectedGame(game);
                                        }}
                                        className={
                                            selectedGame === game ? s.selectedGame : s.notSelected
                                        }
                                        key={indx}
                                    >
                                        {game.name}
                                    </div>
                                );
                            }) : 'More games'}
                    </div>
                    <div className={s.buttonGame}>
                        <button className={s.RequestAllGames}
                                disabled={loading}
                                onClick={() => {
                                    getAll();
                                }}
                        >
                            Update
                        </button>

                        <button className={s.startGame}
                            disabled={!selectedGame}
                            onClick={() => {
                                executeConnect(selectedGame.name);
                            }}
                        >
                            start
                        </button>

                    </div>

                    <div className={s.div_logout}>
                        <button className={s.logout}
                            onClick={() => {
                                logOut();
                            }}
                        >
                            log Out
                        </button>
                    </div>
                </main>
            </div>
        </div>
    );
};

export default GameList;
