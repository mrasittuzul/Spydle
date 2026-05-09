import styles from "../styles/ErrorPage.module.css";

export default function ErrorPage(){
    return(
        <div className={styles.page}>
            <h1 className={styles.heading}>Error</h1>
            <p className={styles.message}>Something went wrong. Please try again.</p>
        </div>
    );
}