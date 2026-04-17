import { NavLink } from "react-router-dom";
import { useAuthContext } from "../Contexts/AuthContext";

export default function Header(){
    const { isAuthorized } = useAuthContext();

    return(
        <nav>
            <NavLink to="/" className={({ isActive }) => isActive ? "active" : ""}>Home</NavLink>
            {
                isAuthorized ? <NavLink to="/Logout" className={({ isActive }) => isActive ? "active" : ""}>Log Out</NavLink>
                             : <NavLink to="/Login" className={({ isActive }) => isActive ? "active" : ""}>Log In</NavLink>
            }
        </nav>
    );
}