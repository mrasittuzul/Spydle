import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import HomePage from './Pages/HomePage';
import LoginPage from './Pages/LoginPage';
import LogoutPage from './Pages/LogoutPage';
import RootPage from './Pages/RootPage';

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootPage/>,
    children: [
      {
        index: true,
        element: <HomePage/>,
      },
      {
        path: "/Login",
        element: <LoginPage/>
      },
      {
        path: "/Logout",
        element: <LogoutPage/>
      }
    ]
  },
]);

export default function App() {
  return (
      <RouterProvider router={router} />
  )
}