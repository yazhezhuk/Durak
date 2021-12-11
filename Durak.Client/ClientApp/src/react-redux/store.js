import { configureStore } from "@reduxjs/toolkit";
import deckSlice from "./deckSlice";
import GameSessionReducer from "./gameSlice";
import authReducer from "./ProfileSlices/authSlice";
import messageReducer from "./ProfileSlices/authSlice";
import bankerReducer from "./bankerSlice";

export default configureStore({
  reducer: {
    auth: authReducer,
    message: messageReducer,
    deck: deckSlice,
    gameSession: GameSessionReducer,
    banker: bankerReducer
  },
  devTools: true,
  
});
