import { redirect, useLoaderData } from "react-router-dom";
import getUser from "../getUser";
import "../styles/home.css";

export async function loader(id) {
  const user = await getUser(id);
  if (!user) {
    return redirect("/login");
  }
  return { user };
}

export default function Home() {
  const { user } = useLoaderData();
  console.log(user);
  console.log(user.lists.music);

  const mapper = (list) => {
    return (
      <ul>
        {list.map((item, index) => (
          <li key={index}>
            {item === null ? `${index + 1}: ` : `${index + 1}: ${item}`}
          </li>
        ))}
      </ul>
    );
  };

  return (
    <>
      <h1 class="heading">TopTens</h1>
      <div id="lists">
        <div class="category" id="music">
          <h2 class="heading">Music</h2>
          {mapper(user.lists.music)}
        </div>
        <div class="category">
          <h2 class="heading">Movies</h2>
          {mapper(user.lists.movies)}
        </div>
        <div class="category" id="tv">
          <h2 class="heading">TV</h2>
          {mapper(user.lists.tv)}
        </div>
        <div class="category">
          <h2 class="heading">Books</h2>
          {mapper(user.lists.books)}
        </div>
      </div>
    </>
  );
}
