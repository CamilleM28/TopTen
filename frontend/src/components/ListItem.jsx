import "../styles/listItem.css";

export default function ListItem({ number, item, category, userId }) {
  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(e.target.submit.value);
    const res = await fetch(`https://localhost:7038/Lists?id=${userId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      credentials: "include",
      body: JSON.stringify({
        userId: userId,
        category: category,
        position: number - 1,
        item: e.target.submit.value !== "remove" ? e.target.item.value : "",
        action: e.target.submit.value,
      }),
    });
    if (res.ok) {
      const response = await res.json();
      console.log(response);
    } else {
      console.log(res);
    }
  };

  return (
    <>
      <li>
        {item === null ? (
          <>
            <span>{number}</span>
            <form onSubmit={handleSubmit}>
              <input type="text" name="item" />
              <input type="submit" name="submit" value="add" />
            </form>
          </>
        ) : (
          <>
            <span>{number}</span>
            <form onSubmit={handleSubmit}>
              <span class="d">{item}</span>
              <input type="submit" name="submit" value="remove" />
            </form>
          </>
        )}
      </li>
      <br />
    </>
  );
}
