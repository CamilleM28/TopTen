export async function action({ request }) {
  const data = await request.formData();

  if (data.get("submit") === "add" && data.get("item").trim() === "") {
    return { error: "Field Empty" };
  }

  const res = await fetch(
    `https://localhost:7038/Lists?id=${data.get("userId")}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      credentials: "include",
      body: JSON.stringify({
        userId: data.get("userId"),
        category: data.get("category"),
        position: data.get("position"),
        item: data.get("submit") !== "remove" ? data.get("item") : "",
        action: data.get("submit"),
      }),
    }
  );
  if (res.ok) {
    return null;
  } else {
    console.log(res);
    return { error: "List update failed! Please try again" };
  }
}
