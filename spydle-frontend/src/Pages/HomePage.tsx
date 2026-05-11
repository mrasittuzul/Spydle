import { useAuthContext } from "../Contexts/AuthContext"
import { Link } from "react-router-dom";

import styles from "../styles/HomePage.module.css";

export default function HomePage(){
    const authContext = useAuthContext();
    const playButtonLink = authContext.isAuthorized ? "/Game" : "/Login";

    return(
        <div className={styles.page}>
            <p className={styles.eyebrow}>A Daily Deduction Game</p>
            <h1 className={styles.title}>Spydle</h1>
            <p className={styles.subtitle}>Find The Spy</p>
            <div className={styles.divider}></div>
            <Link to={playButtonLink} className={styles.playButton}>Play</Link>
        </div>
    )
}