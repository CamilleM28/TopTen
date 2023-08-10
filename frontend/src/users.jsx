export default async function getUser(id, token) {
  const user = await fetch(`https://localhost:7038/Users/user?id=${id}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });

  const User = await user.json();

  console.log(User);

  const lists = await fetch(`https://localhost:7038/Lists?id=${id}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${localStorage.getItem("token")}`,
    },
  });

  const Lists = await lists.json();

  console.log(Lists);
  return { User, Lists };
}
