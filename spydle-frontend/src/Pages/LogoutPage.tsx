import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

export default function LogoutPage(){
    localStorage.removeItem("jwt");
    const navigate = useNavigate();
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