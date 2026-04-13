import { NavLink } from "react-router-dom";

export default function Header(){

    const jwt = localStorage.getItem('jwt');

    return(
        <nav>
            <NavLink to="/" className={({ isActive }) => isActive ? "active" : ""}>Home</NavLink>
            {
                jwt ? <NavLink to="/Logout" className={({ isActive }) => isActive ? "active" : ""}>Log Out</NavLink>
                    : <NavLink to="/Login" className={({ isActive }) => isActive ? "active" : ""}>Log In</NavLink>
            }
        </nav>
    );
}