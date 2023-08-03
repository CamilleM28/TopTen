export default function Login() {
  const handleSubmit = async (e) => {
    e.preventDefault();
    const res = await fetch("https://localhost:7038/Auth/Login", {
      method: "POST",
      body: JSON.stringify({
        email: e.target.email.value,
        password: e.target.password.value,
      }),
      headers: {
        "Content-Type": "application/json",
      },
    });
    const response = await res.json();

    console.log(response);
  };

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
