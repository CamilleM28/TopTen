import "../styles/listItem.css";

import { Form } from "react-router-dom";

export default function ListItem({ number, item, category, userId }) {
  return (
    <>
      <li>
        {item === null ? (
          <>
            <span>{number}</span>
            <Form method="put" action="/">
              <input type="hidden" name="category" value={category} />
              <input type="hidden" name="userId" value={userId} />
              <input type="hidden" name="position" value={number - 1} />
              <input type="text" name="item" />
              <input type="submit" name="submit" value="add" />
            </Form>
          </>
        ) : (
          <>
            <span>{number}</span>
            <Form method="put" action="/">
              <input type="hidden" name="category" value={category} />
              <input type="hidden" name="userId" value={userId} />
              <input type="hidden" name="position" value={number - 1} />
              <span class="d">{item}</span>
              <input type="submit" name="submit" value="remove" />
            </Form>
          </>
        )}
      </li>
      <br />
    </>
  );
}
