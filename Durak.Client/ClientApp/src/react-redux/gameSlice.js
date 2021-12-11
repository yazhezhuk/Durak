import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import GameService from '../services/game.service'
import { setMessage } from "./ProfileSlices/messageSlice";

export const createGame = createAsyncThunk(
  "game/create",
  async ({ name,token }, thunkAPI) => {
    try {
     
      const data = await GameService.createGame(name,token);
      return { user: data };
    } catch (error) {
      const message =
        (error.response &&
          error.response.data &&
          error.response.data.message) ||
        error.message ||
        error.toString();
      thunkAPI.dispatch(setMessage(message));
      return thunkAPI.rejectWithValue();
    } 
  }
);

export const gameSlice = createSlice({
    name: 'gameSession',
    initialState: {
      game: null
    },
    extraReducers: {
      [createGame.fulfilled]: (state, action) => {
        state.game = action.payload.user;
      },
      [createGame.rejected]: (state, action) => {
        state.game = null;
      },
     
    },
  })
  
  // Action creators are generated for each case reducer function
  const { reducer } = gameSlice;
  
  export default reducer