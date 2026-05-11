import { useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Post } from "../Helpers/RequestHelper";
import type { ApiErrorResponse } from "../Helpers/RequestHelper";

import styles from '../Styles/SignupPage.module.css';

export default function SignupPage(){

    const [loginErrors, setSignUpErrors] = useState<string[]>();
    const emailRef = useRef<HTMLInputElement>(null);
    const usernameRef = useRef<HTMLInputElement>(null);
    const passwordRef = useRef<HTMLInputElement>(null);
    const passwordConfirmRef = useRef<HTMLInputElement>(null);
    const navigate = useNavigate();

    async function onSignUpButtonClicked(){
        if (!emailRef.current || !usernameRef.current || !passwordRef.current || !passwordConfirmRef.current){
            setSignUpErrors(["Email or Password input field reference is null"]);
            return;
        };

        if(passwordRef.current.value !== passwordConfirmRef.current.value){
            setSignUpErrors(["Both password fields must match."]);
            return;
        }

        const email: string = emailRef.current.value;
        const username: string = usernameRef.current.value;
        const password: string = passwordRef.current.value;
        const response = await Post<string>("Account/Register", { Email: email, Username: username, Password: password });
        if(response.status == 200){
            navigate("/login");
        } else{
            setSignUpErrors((response.data as ApiErrorResponse).errors)
        }
    }

    return(
        <div className={styles.page}>
            <div className={styles.card}>
                <h1 className={styles.heading}>Sign Up</h1>
                <ul className={styles.fieldList}>
                    <li className={styles.fieldItem}>
                        <label className={styles.label}>Email</label>
                        <input className={styles.input} ref={emailRef} />
                    </li>
                    <li className={styles.fieldItem}>
                        <label className={styles.label}>Username</label>
                        <input className={styles.input} ref={usernameRef} />
                    </li>
                    <li className={styles.fieldItem}>
                        <label className={styles.label}>Password</label>
                        <input className={styles.input} ref={passwordRef} type="password" />
                    </li>
                    <li className={styles.fieldItem}>
                        <label className={styles.label}>Confirm Password</label>
                        <input className={styles.input} ref={passwordConfirmRef} type="password" />
                    </li>
                    <li className={styles.submitItem}>
                        <button className={styles.button} onClick={onSignUpButtonClicked}>Submit</button>
                    </li>
                </ul>
                {loginErrors && (
                    <ul className={styles.errorList}>
                        {loginErrors.map((err, index) => (
                            <li key={index} className={styles.errorItem}>{err}</li>
                        ))}
                    </ul>
                )}
            </div>
        </div>
    );
}