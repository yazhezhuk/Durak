import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";


export const user = JSON.parse(localStorage.getItem("user")) ;

const API_URL = "http://localhost:3000/api/auth/";

export const login = createAsyncThunk(
  "auth/login",
  async ({ username, password }, thunkAPI) => {
    try {
      const response = await fetch(API_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "*/*",
          "Accept-Encoding": "gzip, deflate, br",
          "Cache-Control": "no-cache",
          Connection: "keep-alive",
        },
        body: JSON.stringify({
          Username: username,
          Password: password,
        }),
      });
      let data = await response.json();
      console.log("response", typeof data);
      if (response.status === 200) {
        localStorage.setItem("user", JSON.stringify(data));
        return { user: data };
      } else {
        return thunkAPI.rejectWithValue(data);
      }
    } catch (e) {
      console.log("Error", e.response.data);
      thunkAPI.rejectWithValue(e.response.data);
    }
  }
);

export const logout = createAsyncThunk("auth/logout", async () => {
  return await sessionStorage.removeItem('user')
});

const initialState = user
  ? { isLoggedIn: true,  user }
  : { isLoggedIn: false, user: null };

const authSlice = createSlice({
  name: "auth",
  initialState,
  extraReducers: {
    [login.fulfilled]: (state, { payload }) => {
      state.isLoggedIn = true;
      state.user = payload.user;
    },
    [login.rejected]: (state, action) => {
      state.isLoggedIn = false;
      state.user = null;
    },
    [logout.fulfilled]: (state, action) => {
      state.isLoggedIn = false;
      state.user = null;
    },
  },
});

const { reducer } = authSlice;
export default reducer;