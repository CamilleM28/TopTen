import { useNavigate } from "react-router-dom";

export default function Login(setUser, setLists) {
  const navigate = useNavigate();
  const handleSubmit = async (e) => {
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

  // const getUser = async (id, token) => {
  //   const res = await fetch(`https://localhost:7038/Users/user?id=${id}`, {
  //     method: "GET",
  //     headers: {
  //       "Content-Type": "application/json",
  //       Authorization: `Bearer ${token}`,
  //     },
  //   });

  //   const response = await res.json();
  //   setUser(response);
  //   console.log(response);
  // };

  // const getLists = async (id, token) => {
  //   const res = await fetch(`https://localhost:7038/Lists?id=${id}`, {
  //     method: "GET",
  //     headers: {
  //       "Content-Type": "application/json",
  //       Authorization: `Bearer ${token}`,
  //     },
  //   });
  //   const response = await res.json();
  //   setLists(response);
  //   console.log(response);
  // };

  return (
    <>
      <h1>Login</h1>
      <form onSubmit={handleSubmit}>
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
    </>
  );
}
