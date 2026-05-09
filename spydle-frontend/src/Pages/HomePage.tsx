import { useAuthContext } from "../Contexts/AuthContext"
import { Link } from "react-router-dom";

import styles from "../styles/HomePage.module.css";

export default function HomePage(){
    const authContext = useAuthContext();
    let playButton;
    if(authContext.isAuthorized){
        playButton = <Link to="/Game" className={styles.playButton}>Play</Link>;
    }

    return(
        <div className={styles.page}>
            <p className={styles.eyebrow}>A Daily Deduction Game</p>
            <h1 className={styles.title}>Spydle</h1>
            <p className={styles.subtitle}>Find The Spy</p>
            <div className={styles.divider}></div>
            { playButton }
        </div>
    )
}