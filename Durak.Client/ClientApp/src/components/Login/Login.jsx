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

  return (
    <div className={s.loginForm}>
      <h3 className={s.h3}>Login</h3>
    <div>
        <Formik
        initialValues={initialValues}
        validationSchema={validationSchema}
        onSubmit={handleLogin}
        
      >
        <Form className={s.loginForm_inner}>
          <div>
            <label htmlFor="username">Login</label>
            <Field
              className={s.login}
              type="text"
              name="username"
              placeholder={"Enter login"}
            />
            <ErrorMessage name="username" component="div" />
          </div>

          <div>
            <label htmlFor="password">Password</label>
            <Field className={s.password} type="password" name="password" placeholder={"Enter password"} />
            <ErrorMessage name="password" component="div" />
          </div>

          <div className="">
            <button
              type="submit"
              className=""
              disabled={loading}
              
            >
              {loading && (
                <span className="">loading</span>
              )}
              <span>Login</span>
            </button>
          </div>

        </Form>
      </Formik>
    </div>
    {message && (
        <div className="">
          <div className="" role="alert">
            {message}
          </div>
        </div>
      )}
    </div>
  );
};

export default Login;
