import { NavLink } from "react-router-dom";

export default function Header(){
    return(
        <nav>
            <NavLink to="/" className={({ isActive }) => isActive ? "active" : ""}>Home</NavLink>
            <NavLink to="/Login" className={({ isActive }) => isActive ? "active" : ""}>Log In</NavLink>
            <NavLink to="/Logout" className={({ isActive }) => isActive ? "active" : ""}>Log Out</NavLink>
        </nav>
    );
}