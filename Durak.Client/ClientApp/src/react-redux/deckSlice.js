import { createSlice } from '@reduxjs/toolkit'

export const deckSlice = createSlice({
    name: 'deck',
    initialState: {
        cards: [],
        trump: null,
    },
    reducers: {
     
    initCards: (state) => {
        const SUITS = ["spades", "diamonds", "clubs", "hearts"];
        const RANKS = ["6", "7", "8", "9", "10", "J", "Q", "K", "A"];
        for (let suit of SUITS) {
            for (let rank of RANKS) {
                state.cards.push(({id: `card-${suit}-${rank}` , suit , rank, trump:false }));
            }
          }
      },
      shuffle: (state) => {
        for (let i = state.cards.length - 1; i > 0; i--) {
            let j = Math.floor(Math.random() * (i + 1));
            [state.cards[i], state.cards[j]] = [state.cards[j], state.cards[i]];
          }
      },
      setTrump: (state)  => {
        state.trump = state.cards[0];

       state.cards
          .filter((card) => card.suit === state.trump.suit)
          .forEach((card) => card.trump = true);

      },
      pop: (state) => {
       return  state.card.pop();
      },
    },
  })
  
  // Action creators are generated for each case reducer function
  export const { initCards, shuffle,setTrump, pop } = deckSlice.actions
  
  export default deckSlice.reducer