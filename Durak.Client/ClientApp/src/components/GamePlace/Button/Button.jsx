import React from "react";
import s from "./Button.module.css";

const Button = ({ label, onClick }) => {
  return (
    <div className={s.btn} onClick={onClick}>
      {label}
    </div>
  );
};
export default Button;
