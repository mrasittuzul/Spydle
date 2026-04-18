import { useAuthContext } from "../Contexts/AuthContext"
import { Link } from "react-router-dom";

export default function HomePage(){
    const authContext = useAuthContext();
    let playButton;
    if(authContext.isAuthorized){
        playButton = <Link to="/Game">Play</Link>;
    }

    return(
        <div>
            <h1>Home Page</h1>
            { playButton }
        </div>
    )
}