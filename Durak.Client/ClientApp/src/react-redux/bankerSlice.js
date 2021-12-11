import { createSlice } from "@reduxjs/toolkit";
import { initCards, shuffle,setTrump } from "./deckSlice";



export const bankerSlice = createSlice({
  name: "banker",
  initialState: {
    attacker: null,
    deck: null,
    openedCards: [],
    discardPile: [],
  },
  reducers: {
    setAttacker: (state, action) => {
      state.attacker = action.payload;
    },
    swapAttacker: (state) => {
      //     let nextIndex = this.attacker.index + 1;
      // if (nextIndex >= this.store.gamblers.length) {
      //   nextIndex = 0;
      // }
      // this.setAttacker(this.store.gamblers[nextIndex]);
    },
    setDeck: (state, action) => {
     state.deck = action.payload
    },
  },
  
  
});

// Action creators are generated for each case reducer function
export const { setAttacker, swapAttacker, setDeck } = bankerSlice.actions;

export default bankerSlice.reducer;

