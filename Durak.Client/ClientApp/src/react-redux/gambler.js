import { createSlice } from "@reduxjs/toolkit";

export const deckSlice = createSlice({
  name: "gambler",
  initialState: {
    id: null,
    hand: {},
    attacker: false,
  },
  reducers: {
    addToHand: (state, action) => {
      for (const card of action.payload) {
        if (!state.hand[card.rank]) {
          state.hand[card.rank] = [];
        }

        state.hand[card.rank].push(card);
      }
    },
    removeFromHand: (state, action) => {

      const cards = state.hand[action.payload.rank].filter((c) => c.id !== action.payload.id);
      state.hand[action.payload.rank] = cards;

      return true;
    },
  },
});


export const { initCards, shuffle, setTrump, pop } = deckSlice.actions;

export default deckSlice.reducer;
