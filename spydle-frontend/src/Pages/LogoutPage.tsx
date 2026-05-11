import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useAuthContext } from "../Contexts/AuthContext";

import styles from "../styles/LogoutPage.module.css";

export default function LogoutPage(){
    const navigate = useNavigate();
    const authContext = useAuthContext();
    authContext.setIsAuthorized(false);
    localStorage.removeItem("jwt");
    localStorage.removeItem("jwtExpireDate");
    useEffect(() => {
        var timer = setTimeout(() => navigate("/"), 1000);
        return () => {
            clearTimeout(timer);
        };
    });

    return(
        <div className={styles.page}>
            <h1 className={styles.heading}>You have been logged out!</h1>
            <p className={styles.message}>
                You will be returned to the homepage shortly. If you're not automatically redirected you can click <Link to="/">here.</Link>
            </p>
        </div>
    );
}