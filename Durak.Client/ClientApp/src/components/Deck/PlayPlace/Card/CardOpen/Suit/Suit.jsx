import React from "react";
import s from './Suit.module.css'
import Spades from  './../../../../../../assets/suitsIMG/Spades.svg'
import Diamonds from './../../../../../../assets/suitsIMG/Diamonds.png'
import Hearts from './../../../../../../assets/suitsIMG/Hearts.png'
import Clubs from './../../../../../../assets/suitsIMG/Clubs.png'

const Suit = ({suit}) => {

    const getSuit = (suit) =>{
       
        switch(suit){
            case 0:
                return Spades
            case 1:
                return Diamonds
            case 2:
                return Hearts
            case 3:
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