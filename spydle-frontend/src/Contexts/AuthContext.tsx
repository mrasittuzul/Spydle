import { createContext, useState } from 'react';
import { useContext } from 'react';

type AuthContextType = {
    isAuthorized: boolean,
    setIsAuthorized: React.Dispatch<React.SetStateAction<boolean>>
};

const AuthContext = createContext<AuthContextType | null>(null);

export function useAuthContext() {
    const context = useContext(AuthContext);
    if (!context){
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
}

export function AuthProvider({ children } : React.PropsWithChildren<{}>){
    const [isAuthorized, setIsAuthorized] = useState(!!localStorage.getItem("jwt"));

    return(
        <AuthContext.Provider value={{isAuthorized, setIsAuthorized}}>
            {children}
        </AuthContext.Provider>
    )
}