import { Outlet, useNavigate } from "react-router-dom";
import { AuthProvider } from "../Contexts/AuthContext";
import { useEffect } from "react";
import Header from "../Components/Header";

export default function RootPage() {
  // Create a logout timer for users opening the app that are already logged in
  const expireDateString = localStorage.getItem("jwtExpireDate");
  const navigate = useNavigate();
  const timeoutCallback = expireDateString != null ? () => navigate("/Logout") : () => {};
  const timeoutLength = expireDateString != null ? parseInt(expireDateString) - Date.now() : 0;
  useEffect(() => {
      var timer = setTimeout(timeoutCallback, timeoutLength);
      return () => clearTimeout(timer);
  }, []);

  return (
    <>
      <AuthProvider>
        <Header />
        <main>
          <Outlet />
        </main>
      </AuthProvider>
    </>
  );
}