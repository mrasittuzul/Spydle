import { createBrowserRouter, RouterProvider } from "react-router-dom";
import HomePage from './Pages/HomePage';
import LoginPage from './Pages/LoginPage';
import LogoutPage from './Pages/LogoutPage';
import RootPage from './Pages/RootPage';
import GamePage from "./Pages/GamePage";
import SignupPage from "./Pages/SignupPage";
import ErrorPage from "./Pages/ErrorPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootPage/>,
    errorElement: <ErrorPage/>,
    children: [
      {
        index: true,
        element: <HomePage/>,
      },
      {
        path: "/Signup",
        element: <SignupPage/>
      },
      {
        path: "/Login",
        element: <LoginPage/>
      },
      {
        path: "/Logout",
        element: <LogoutPage/>
      },
      {
        path: "/Game",
        element: <GamePage/>
      }
    ]
  },
]);

export default function App() {
  return (
      <RouterProvider router={router} />
  )
}