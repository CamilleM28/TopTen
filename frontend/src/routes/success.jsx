import { Link } from "react-router-dom";

export default function Success() {
  return (
    <>
      <h1>Account Created</h1>
      <h2>please log in</h2>
      <Link to={"/login"}> Login</Link>
    </>
  );
}
