import { useLoaderData } from "react-router-dom";
import getUser from "../users";

export async function loader(id) {
  const user = await getUser(id);
  return { user };
}

export default function Home() {
  const { user } = useLoaderData();

  return (
    <>
      <h1>Home</h1>
    </>
  );
}
