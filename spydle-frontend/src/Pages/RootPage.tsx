import { Outlet } from "react-router-dom";
import Header from "../Components/Header";
import { AuthProvider } from "../Contexts/AuthContext";

export default function RootPage() {
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