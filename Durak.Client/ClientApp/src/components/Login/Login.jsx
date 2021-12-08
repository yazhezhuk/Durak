import React from "react";
import s from './Login.module.css'

const Login = () => {
    return (
        <div className={s.loginForm}>
            <h3 className={s.h3}>
                Login
            </h3>
            <form className={s.loginForm_inner}>
                Login: <input className={s.login} type = "text"  name = "login" placeholder={'Input login'} /> <br/>
                Password: <input className={s.password} type = "password" name = "password" placeholder={'Input password'} /> <br/>
                <input className={s.submit} type = "button" name = "submit" value = "Submit" />
            </form>
        </div>
    )
}

export default Login