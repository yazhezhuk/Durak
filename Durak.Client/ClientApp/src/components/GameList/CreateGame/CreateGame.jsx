import { ErrorMessage, Field, Form, Formik } from "formik";
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { clearMessage } from "../../../react-redux/ProfileSlices/messageSlice";
import { createGame } from "../../../react-redux/gameSlice";
import * as Yup from "yup";
import s from "./CreateGame.module.css";

const CreateGame = ({setLoading,loading,games,setGames}) => {


  const dispatch = useDispatch();
  const initialValues = {
    name: "",

  };

  const validationSchema = Yup.object().shape({
    name: Yup.string().required("This field is required!"),
  });
  useEffect(() => {
    dispatch(clearMessage());
  }, [dispatch]);

  const handleLogin = (formValue) => {
    const { name } = formValue;
    setLoading(true);
    dispatch(createGame({ name}))
      .unwrap()
      .then((response) => {
        console.log( response.game)



        setGames(prevState => [...prevState, response.game])

        setLoading(false);
      })
      .catch(() => {
        setLoading(false);
      });
    
  };
  return (
    <div className={s.createGame}>
      {/*<h3 className={s.createGame_h3}>create new game</h3>*/}
      <div className={s.container}>
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={handleLogin}
        >
          <Form>
            <div className={s.addGame}>
              <Field clssName={s.addGame_field}
                
                type="text"
                name="name"
                placeholder={"Enter login"}
              />
              <button className={s.addGame_button} type="submit" disabled={loading}>
                {loading && <span>loading...</span>}
                <span>create</span>
              </button>
              {/*<label htmlFor="name">create</label>*/}
            </div>
            
          </Form>
        </Formik>
      </div>
     
    </div>
  );
};

export default CreateGame;
