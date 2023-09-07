import { redirect, useLoaderData } from "react-router-dom";
import getUser from "../getUser";
import "../styles/home.css";
import ListItem from "../components/ListItem";

export async function loader() {
  const user = await getUser();
  if (!user) {
    return redirect("/login");
  }
  return { user };
}

export default function Home() {
  const { user } = useLoaderData();
  console.log(user);
  console.log(user.lists.music);

  const mapper = (list, category) => {
    return (
      <ul>
        {list.map((item, index) => (
          <ListItem
            key={index}
            number={index + 1}
            item={item}
            category={category}
            userId={user.user.id}
          />
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
          {mapper(user.lists.music, "music")}
        </div>
        <div class="category">
          <h2 class="heading">Movies</h2>
          {mapper(user.lists.movies, "movies")}
        </div>
        <div class="category" id="tv">
          <h2 class="heading">TV</h2>
          {mapper(user.lists.tv, "tv")}
        </div>
        <div class="category">
          <h2 class="heading">Books</h2>
          {mapper(user.lists.books, "books")}
        </div>
      </div>
    </>
  );
}
