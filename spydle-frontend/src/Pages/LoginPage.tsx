import { useRef, useState } from "react";
import { Post } from "../Helpers/RequestHelper";
import type { ApiLoginResponse, ApiErrorResponse } from "../Helpers/RequestHelper";

export default function LoginPage(){

    const [loginErrors, setLoginErrors] = useState<string[]>();
    const emailRef = useRef<HTMLInputElement>(null);
    const passwordRef = useRef<HTMLInputElement>(null);

    console.log(loginErrors);
    async function onLoginButtonClicked(){
        if (!emailRef.current || !passwordRef.current){
            console.error("Email or Password input field reference is null");
            return;
        };

        const email: string = emailRef.current.value;
        const password: string = passwordRef.current.value;
        const response = await Post("Account/Login", { Email: email, Password: password });
        if(response.status == 200){
            const loginResponse = response.data as ApiLoginResponse;
            localStorage.setItem("jwt", loginResponse.token);
            localStorage.setItem("jwtExpireDate", loginResponse.tokenExpireDate);

        } else{
            setLoginErrors((response.data as ApiErrorResponse).errors)
        }
    }

    return(
        <div>
            <ul>
                <li>
                    <label>Email</label>
                    <input ref={emailRef}></input>
                </li>
                <li>
                    <label>Password</label>
                    <input ref={passwordRef} type="password"></input>
                </li>
                <li>
                    <button onClick={onLoginButtonClicked}>Login</button>
                </li>
            </ul>
            {loginErrors && <ul>{loginErrors.map((err, index) => <li key={index}>{err}</li>)}</ul>}
        </div>
    );
}