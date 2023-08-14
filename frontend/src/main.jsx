import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Login from "./routes/login";
import Home from "./routes/home";
import Sucess from "./routes/success";
import ErrorPage from "./error-page";
import { loader as rootLoader } from "./routes/home.jsx";
import "./Main.css";

const router = createBrowserRouter([
  {
    path: "login",
    element: <Login />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/",
    element: <Home />,
    loader: rootLoader,
  },
  {
    path: "/sucess",
    element: <Sucess />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
