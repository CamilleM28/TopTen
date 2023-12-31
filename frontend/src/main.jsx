import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Login from "./routes/login";
import Home from "./routes/home";
import Sucess from "./routes/success";
import ErrorPage from "./error-page";
import { loader as rootLoader } from "./Loaders&Actions/RootLoader";
import { action as rootAction } from "./Loaders&Actions/RootAction";
import { authAction } from "./Loaders&Actions/AuthAction";
import "./Main.css";
import { GoogleOAuthProvider } from "@react-oauth/google";

const router = createBrowserRouter([
  {
    path: "login",
    element: <Login />,
    action: authAction,
    errorElement: <ErrorPage />,
  },
  {
    path: "/",
    element: <Home />,
    loader: rootLoader,
    action: rootAction,
  },
  {
    path: "/sucess",
    element: <Sucess />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}>
    <React.StrictMode>
      <RouterProvider router={router} />
    </React.StrictMode>
  </GoogleOAuthProvider>
);
