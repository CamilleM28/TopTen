import { redirect } from "react-router-dom";

export async function authAction({ request }) {
  const data = await request.formData();
  let intent = data.get("submit");

  if (intent === "Login") {
    const res = await fetch("https://localhost:7038/Auth/Login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify({
        email: data.get("email"),
        password: data.get("password"),
      }),
    });

    if (res.ok) {
      const response = await res.json();
      localStorage.setItem("id", response.id);
      localStorage.setItem("token", response.token);
      return redirect("/");
    } else {
      alert("Wrong Credentials");
      return null;
    }
  }

  if (intent === "Register") {
    const res = await fetch("https://localhost:7038/Auth/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        name: data.get("username"),
        email: data.get("email"),
        password: data.get("password"),
      }),
    });

    const response = await res.text();
    if (res.ok) {
      return redirect("/sucess");
    } else {
      alert(`${response}`);
      return null;
    }
  }
}
