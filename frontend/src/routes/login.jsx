import { useNavigate } from "react-router-dom";
import { useGoogleLogin } from "@react-oauth/google";
import GoogleG from "/btn_google_signin_light_normal_web@2x.png";
import "../styles/login.css";
import { useState } from "react";

export default function Login() {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
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

    if (confirmPassword !== password) {
      alert("Passwords do not match");
      return;
    }

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
      if (res.ok) {
        const response = await res.json();
        console.log(response);
        localStorage.setItem("id", response.id);
        localStorage.setItem("token", response.token);
        navigate("/");
      } else {
        alert("Wrong Credentials");
      }
    },
    flow: "auth-code",
  });

  const emailRegex = String.raw`^\S+@\S+\.\S{2,3}$`;
  const emailTitle = "Email must be in the format example@email.com";
  const passwordRegex = String.raw`^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$`;
  const passwordTitle =
    "Password must have at least 8 characters and conatin at east one uppercase character, lowercase character and digit";

  return (
    <div id="container">
      <div class="section" id="login">
        <h1>Login</h1>

        <form onSubmit={handleLogin}>
          <label>
            Email{" "}
            <input
              type="text"
              name="email"
              pattern={emailRegex}
              title={emailTitle}
              required
            />
          </label>

          <br />
          <br />
          <label>
            Password{" "}
            <input
              type="password"
              name="password"
              pattern={passwordRegex}
              title={passwordTitle}
              required
            />
          </label>
          <br />
          <br />
          <input class="submit" type="submit" value="Login" />
          <br />
          <br />
        </form>
        <hr />
        <br />
        <div id="google-button" onClick={googleLogin}>
          <img src={GoogleG} height="50px" />
        </div>
      </div>

      <div class="section">
        <h1>Register</h1>
        <form onSubmit={handleRegister}>
          <label>
            Username <input type="text" name="username" required />
          </label>
          <br />
          <br />
          <label>
            Email{" "}
            <input
              type="email"
              name="email"
              pattern={emailRegex}
              title={emailTitle}
              required
            />
          </label>
          <br />
          <br />
          <label>
            Password{" "}
            <input
              type="password"
              name="password"
              pattern={passwordRegex}
              title={passwordTitle}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </label>
          <br />
          <br />
          <label>
            Confirm Password{" "}
            <input
              type="password"
              name="confirm"
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
            />
          </label>
          <br />
          <br />
          <input class="submit" type="submit" value="Register" />
        </form>
      </div>
    </div>
  );
}
