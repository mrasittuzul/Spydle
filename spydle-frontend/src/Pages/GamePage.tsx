import SpydleGame from "../Components/SpydleGame";
import styles from "../Styles/GamePage.module.css";

export default function GamePage(){
    return(
        <div className={styles.page}>
            <SpydleGame/>
            <div className={styles.gameDescription}>
                <p>
                    The goal of the game is to find the spy.
                    You can select a character to interrogate them to find out if they share a trait (old/young, male/female/ skin color, eye&chest color) with the spy or not.
                    The white pip next to the interrogation row will be colored green if the interrogated character shares a trait with the spy, red if not.
                    After 5 interrogations you will perform a final interrogation on a group of 5 characters, if the spy is among them you will win the game.
                    The spy character and whether you were successful or not will be revealed after the last interrogation.
                </p>
            </div>
        </div>
    );
}