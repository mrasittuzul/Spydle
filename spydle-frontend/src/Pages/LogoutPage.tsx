import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useAuthContext } from "../Contexts/AuthContext";

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
    })

    return(
        <>
            <h1>You have been logged out!</h1>
            <p>You will be returned to the homepage shortly. If you're not automatically redirected you can click <Link to="/">here.</Link></p>
        </>
    );
}