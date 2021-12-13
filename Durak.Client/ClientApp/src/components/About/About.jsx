import React, {useState} from "react";
import s from './About.module.css'
import durak from '../../assets/img/durak.png'
import {Navigate} from "react-router-dom";

const About = () => {
    const [toLogin, setToLogin] = useState(false)
    
    if (toLogin)  {
        return <Navigate to='/login' />
    }
    


    return (
        <div className={s.aboutGame}>
            <div className={s.container_1170}>
                <h1 className={s.aboutGame_h1}>Про гру "Дурак"
                    <button className={s.button} onClick={() => {setToLogin(true)}}>X</button>
                </h1>
                <div className={s.aboutGame_inner}>
                    <img className={s.aboutGame_inner_img} src={durak}/>
                    <p className={s.aboutGame_inner_p}><span className={s.paragraph}></span>У грі використовується
                        колода з 36 карт і беруть
                        участь від двох до шести гравців. Кожному роздається по 6 карт,
                        наступна карта відкривається і її масть визначає козир для даної гри, решта колоди кладеться
                        зверху так,
                        щоб козирну карту було видно всім. <br/><br/>

                        <span className={s.paragraph}></span>У грі є нічия: коли за ігровим столом залишається
                        двоє гравців, гравець,
                        що ходить кидає карти противнику так,
                        щоб противник відбив усі карти і в обох гравців не залишається карт — тоді в грі
                        оголошується нічия.
                        <br/><br/>

                        <span className={s.paragraph}></span>Ціль гри — позбутися всіх карт. Останній гравець,
                        у якого залишилися
                        карти, залишається в «дурнях».
                        У першій здачі першим ходить гравець із молодшим козирем; у подальших здачах ходять
                        або «під дурня»,
                        тобто гравець праворуч від «дурня», або «з-під дурня», тобто гравець ліворуч, за
                        домовленістю.
                        Хід робиться завжди ліворуч, і складається з викладання однієї або більше карт, і
                        спроби гравця,
                        під якого ходять, їх покрити або старшою картою тієї ж масті, або картою козирної
                        масті, якщо карта,
                        що криється, сама є козирною — у такому випадку її можна покрити тільки старшим
                        козирем.
                    </p>
                </div>

                <div className={s.aboutGame_rules}>
                    <h2 className={s.aboutGame_rules_h2}>Як почати гру?</h2>
                    <ul className={s.aboutGame_rules_ul}>
                        <li>Щоб почати грпу потрібно ввійти в систему під
                            своїм логінем та паролем
                        </li>

                        <li>Після входу в систему почніть гру чи оберіть вже існуюючу</li>
                        <li>Ви перейдете на ігровий стіл та почне гру за всіма правилами "Дурака"</li>

                    </ul>
                </div>


            </div>
        </div>
    )
}

export default About
