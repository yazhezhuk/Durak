import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

import { user } from "./ProfileSlices/authSlice";


const API_URL = "http://localhost:3000/api/game/";

export const createGame = createAsyncThunk(
  "game/create",
  async ({ name }, thunkAPI) => {
    try {
      const response = await fetch(API_URL + "create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          'Accept': "*/*",
          "Accept-Encoding": "gzip, deflate, br",
          "Cache-Control": "no-cache",
          'Connection': "keep-alive",
          'Authorization': `Bearer ${user.token}`,
        },
        body: JSON.stringify({ Name: name }),
      });
      let data = await response.json();
      console.log("response", data);
      if (response.status === 200) {
        return { game: data };
      } else {
        return thunkAPI.rejectWithValue(data);
      }
    } catch (e) {
      console.log("Error", e.response.data);
      thunkAPI.rejectWithValue(e.response.data);
    }
  }
);
export const connectGame = createAsyncThunk(
  "game/create",
  async ({ name }, thunkAPI) => {
    try {
      const response = await fetch(API_URL + "connect", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          'Accept': "*/*",
          "Accept-Encoding": "gzip, deflate, br",
          "Cache-Control": "no-cache",
          'Connection': "keep-alive",
          'Authorization': `Bearer ${user.token}`,
        },
        body: JSON.stringify({ Name: name }),
      });
      let data = await response.json(); 
      if (response.status === 200) {
        console.log("connect response", data);
  
        thunkAPI.dispatch(setCurrentGame(name))
        return { isConnected: data };
      } else {
        return thunkAPI.rejectWithValue(data);
      }
    } catch (e) {
      console.log("Error", e.response.data);
      thunkAPI.rejectWithValue(e.response.data);
    }
  }
);
export const getAllGames = createAsyncThunk("game/getAll", async (thunkAPI) => {
  try {
    const response = await fetch(API_URL + "all", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        'Accept': "*/*",
        "Accept-Encoding": "gzip, deflate, br",
        "Cache-Control": "no-cache",
        'Connection': "keep-alive",
        'Authorization': `Bearer ${user.token}`,
      },
    });
    let data = await response.json();
    console.log("response", data);
    if (response.status === 200) {
      return { games: data };
    } else {
      return thunkAPI.rejectWithValue(data);
    }
  } catch (e) {
    console.log("Error", e.response.data);
    thunkAPI.rejectWithValue(e.response.data);
  }
});

export const gameSlice = createSlice({
  name: "gameSession",
  initialState: {
    games: [],
    isConnected: false,
    currentGameName: null,
  },
  reducers: {
  setCurrentGame: (state,action) =>{
    state.currentGameName = action.payload
  }
  },
  extraReducers: {
    [createGame.fulfilled]: (state, action) => {
      state.games = [...state.games, action.payload.game];
    },
    [getAllGames.fulfilled]: (state, action) => {
      state.games = [action.payload.games];
    },
    [connectGame.fulfilled]: (state, action) => {
      state.isConnected = action.payload.isConnected.result;
    
    },
    [connectGame.rejected]: (state, action) => {
      state.isConnected = false
    },
  },
});

// Action creators are generated for each case reducer function
export const  {setCurrentGame}  = gameSlice.actions
const { reducer } = gameSlice;

export default reducer;
