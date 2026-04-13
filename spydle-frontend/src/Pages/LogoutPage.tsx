import { Link } from "react-router-dom";

export default function LogoutPage(){
    return(
        <>
            <h1>You have been logged out!</h1>
            <p>You will be returned to the homepage shortly. If you're not automatically redirected you can click <Link to="/">here.</Link></p>
        </>
    );
}