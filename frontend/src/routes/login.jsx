import { useNavigate } from "react-router-dom";
import { useGoogleLogin } from "@react-oauth/google";

export default function Login() {
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    const res = await fetch("https://localhost:7038/Auth/Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify({
        email: e.target.email.value,
        password: e.target.password.value,
      }),
    });

    if (res.ok) {
      const response = await res.json();
      localStorage.setItem("id", response.id);
      localStorage.setItem("token", response.token);
      navigate("/");
    } else {
      alert("Wrong Credentials");
    }
  };

  const handleRegister = async (e) => {
    e.preventDefault();
    const res = await fetch("https://localhost:7038/Auth/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: e.target.username.value,
        email: e.target.email.value,
        password: e.target.password.value,
      }),
    });
    console.log(e.target.password.value);
    const response = await res.text();
    if (res.ok) {
      navigate("/sucess");
    } else {
      alert(`${response}`);
    }
  };

  const googleLogin = useGoogleLogin({
    onSuccess: async (CodeResponse) => {
      console.log(CodeResponse);
      var code = CodeResponse.code;
      console.log(code);
      const res = await fetch("https://localhost:7038/Auth/auth-google", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          code: code,
          uri: "http://localhost:3000",
          Id: import.meta.env.VITE_GOOGLE_CLIENT_ID,
          Secret: import.meta.env.VITE_GOOGLE_CLIENT_SECRET,
        }),
      });
      console.log(await res.text());
    },
    flow: "auth-code",
  });

  return (
    <>
      <h1>Login</h1>
      <form onSubmit={handleLogin}>
        <label>
          Email:
          <input type="text" name="email" />
        </label>
        <label>
          Password:
          <input type="text" name="password" />
        </label>
        <input type="submit" value="Submit" />
      </form>
      <h1>Register</h1>
      <form onSubmit={handleRegister}>
        <label>
          Username:
          <input type="text" name="username" />
        </label>
        <label>
          Email:
          <input type="text" name="email" />
        </label>
        <label>
          Password:
          <input type="text" name="password" />
        </label>
        <label>
          Confirm Password:
          <input type="text" name="confirm" />
        </label>
        <input type="submit" value="Submit" />
      </form>

      <h2>Sign in with Google</h2>
      <button onClick={googleLogin} />
    </>
  );
}
