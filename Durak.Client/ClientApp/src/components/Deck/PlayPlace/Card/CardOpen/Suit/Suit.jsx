import React from "react";
import s from './Suit.module.css'
import Spades from  './../../../../../../assets/suitsIMG/Spades.svg'
import Diamonds from './../../../../../../assets/suitsIMG/Diamonds.png'
import Hearts from './../../../../../../assets/suitsIMG/Hearts.png'
import Clubs from './../../../../../../assets/suitsIMG/Clubs.png'

const Suit = ({suit}) => {

    const getSuit = (suit) =>{
       
        switch(suit){
            case 'diamonds':
                return Diamonds
            case 'spades':
                return Spades
            case "hearts":
                return Hearts
            case 'clubs':
                return Clubs
            default:
                return 
        }
    }

    return (
        <div className={s.suit}>
            
            <img src={getSuit(suit)} alt={suit} />
        </div>
    )
}

export default Suit