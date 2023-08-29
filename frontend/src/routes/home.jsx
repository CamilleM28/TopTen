import { redirect, useLoaderData } from "react-router-dom";
import getUser from "../getUser";
import { GoogleLogin } from "@react-oauth/google";

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

  return (
    <>
      <h1>Home</h1>
    </>
  );
}
