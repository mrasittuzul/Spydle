import { NavLink, Link } from "react-router-dom";
import { useAuthContext } from "../Contexts/AuthContext";
import styles from "../styles/Header.module.css";

export default function Header(){
    const { isAuthorized } = useAuthContext();

    return(
        <nav className={styles.nav}>
            <span className={styles.logo}><Link to="/">Spydle</Link></span>
            <div className={styles.navLinks}>
                {isAuthorized ? <NavLink to="/Logout" className={({ isActive }) => `${styles.navLink} ${isActive ? styles.active : ""}`}>Log Out</NavLink>
                              : <NavLink to="/Login" className={({ isActive }) => `${styles.navLink} ${isActive ? styles.active : ""}`}>Log In</NavLink>
                }
            </div>
        </nav>
    );
}