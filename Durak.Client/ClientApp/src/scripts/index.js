const suits = ["spades", "diamonds", "clubs", "hearts"];
const ranks = ["6", "7", "8", "9", "10", "J", "Q", "K", "A"];

export const shuffleDeck = (deck) => {
  const deckCopy = [...deck];
  for (let i = 0; i < 1000; i++) {
    const location1 = Math.floor(Math.random() * deckCopy.length);
    const location2 = Math.floor(Math.random() * deckCopy.length);
    let tmp = deckCopy[location1];

    deckCopy[location1] = deckCopy[location2];
    deckCopy[location2] = tmp;
  }
  return deckCopy;
};

const getDeck = () => {
    let deck = [];
    let id = 0
    for (let suit of suits) {
      for (let rank of ranks) {
        let card = { id:id++, rank: rank, suit: suit };
        deck.push(card);
      }
    }
    return deck;
  };

  
export const deck = getDeck()

// export const deck = getDeck();

// export const dealCard = (deck) => {
//   return deck.pop();
// };
// export const getTrump = (deck) => {
//   return dealCard(deck);
// };

const Player1 = {
  Cards: [],
};
const Player2 = {
  Cards: [],
};
