export default async function getUser() {
  const id = localStorage.getItem("id");
  let token = localStorage.getItem("token");

  let userResponse = await getUserData(id, token);

  if (!userResponse.ok) {
    const newTokenResponse = await fetch(
      `https://localhost:7038/Auth/refresh-token?Id=${id}`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      }
    );

    if (!newTokenResponse.ok) {
      return null;
    }

    const newToken = await newTokenResponse.text();

    localStorage.setItem("token", newToken);
    token = newToken;

    userResponse = await getUserData(id, token);
  }

  const user = await userResponse.json();

  console.log(user);

  const listsResponse = await fetch(`https://localhost:7038/Lists?id=${id}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  const lists = await listsResponse.json();

  console.log(lists);
  return { user, lists };
}

async function getUserData(id, token) {
  return await fetch(`https://localhost:7038/Users/user?id=${id}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });
}
