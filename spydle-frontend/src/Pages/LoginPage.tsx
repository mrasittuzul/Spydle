import { useRef, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { Post } from "../Helpers/RequestHelper";
import type { ApiErrorResponse } from "../Helpers/RequestHelper";
import { useAuthContext } from "../Contexts/AuthContext";

import styles from '../styles/LoginPage.module.css';

type ApiLoginResponse = { token: string, tokenExpireDateInMiliseconds: number };

export default function LoginPage(){

    const [loginErrors, setLoginErrors] = useState<string[]>();
    const emailRef = useRef<HTMLInputElement>(null);
    const passwordRef = useRef<HTMLInputElement>(null);
    const navigate = useNavigate();
    const authContext = useAuthContext()

    async function onLoginButtonClicked(){
        if (!emailRef.current || !passwordRef.current){
            console.error("Email or Password input field reference is null");
            return;
        };

        const email: string = emailRef.current.value;
        const password: string = passwordRef.current.value;
        const response = await Post<ApiLoginResponse>("Account/Login", { Email: email, Password: password });
        if (response.status == 200){
            const loginResponse = response.data as ApiLoginResponse;
            localStorage.setItem("jwt", loginResponse.token);
            localStorage.setItem("jwtExpireDate", loginResponse.tokenExpireDateInMiliseconds.toString());
            authContext.setIsAuthorized(true);
            navigate("/");
            setTimeout(() => navigate("/Logout"), loginResponse.tokenExpireDateInMiliseconds - Date.now())
        } else{
            setLoginErrors((response.data as ApiErrorResponse).errors)
        }
    }

    return(
        <div className={styles.page}>
            <div className={styles.card}>
                <h1 className={styles.heading}>Log In</h1>
                <p className={styles.subheading}>Identify yourself</p>
                <ul className={styles.fieldList}>
                    <li className={styles.fieldItem}>
                        <label className={styles.label}>Email</label>
                        <input className={styles.input} ref={emailRef} />
                    </li>
                    <li className={styles.fieldItem}>
                        <label className={styles.label}>Password</label>
                        <input className={styles.input} ref={passwordRef} type="password" />
                    </li>
                    <li className={styles.submitItem}>
                        <button className={styles.button} onClick={onLoginButtonClicked}>Enter</button>
                    </li>
                    <li className={styles.submitItem}>
                        <Link to="/Signup" className={styles.signup} onClick={onLoginButtonClicked}>Don't have an account? Sign Up</Link>
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