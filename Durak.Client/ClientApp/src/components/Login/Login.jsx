import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Navigate  } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import s from "./Login.module.css";

import { login } from "../../react-redux/ProfileSlices/authSlice";
import { clearMessage } from "../../react-redux/ProfileSlices/messageSlice";

const Login = (props) => {
  const [loading, setLoading] = useState(false);
  const [toAbout, setToAbout] = useState(false)

  const { isLoggedIn } = useSelector((state) => state.auth);
  const { message } = useSelector((state) => state.message);
  

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(clearMessage());
  }, [dispatch]);

  const initialValues = {
    username: "",
    password: "",
  };

  const validationSchema = Yup.object().shape({
    username: Yup.string().required("This field is required!"),
    password: Yup.string().required("This field is required!"),
  });

  const handleLogin = (formValue) => {
    const { username, password } = formValue;
    setLoading(true);
    dispatch(login({ username, password }))
      .unwrap()
      .then(() => {
        props.history.push("/profile");
        window.location.reload();
      })
      .catch(() => {
        setLoading(false);
      });
      setLoading(false);
  };

  if (isLoggedIn) {
    return <Navigate  to="/profile" />;
  }
  
  if (toAbout)  {
    return <Navigate to='/about' />
  }

  return (
    <div className={s.loginForm}>
      <style>
        @import url('https://fonts.googleapis.com/css2?family=Open+Sans&display=swap');
      </style>
      <p className={s.about} onClick={() => {setToAbout(true)}}>About</p>
      <h3 className={s.h3}>Login in to <br />your account</h3>
    <div>
        <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleLogin}
        
      >
        <Form className={s.loginForm_inner}>
          <div>
            {/*<label htmlFor="username">Login</label>*/}
            <Field className={s.login}
              type="text"
              name="username"
              placeholder={"Enter login"}
            />
            <ErrorMessage className={s.validating} name="username" component="div" />
          </div>

          <div>
            {/*<label className={s.password} htmlFor="password">Password</label>*/}
            <Field className={s.password}  type="password" name="password" placeholder={"Enter password"} />
            <ErrorMessage className={s.validating} name="password" component="div" />
          </div>

          <div className={s.submit} >
            <button className={s.button}
              type="submit"

              disabled={loading}
              
            >
              {loading && (
                <span >loading</span>
              )}
              <span >Log in</span>
            </button>
          </div>

        </Form>
      </Formik>
    </div>
    {message && (
        <div >
          <div  role="alert">
            {message}
          </div>
        </div>
      )}
    </div>
  );
};

export default Login;
