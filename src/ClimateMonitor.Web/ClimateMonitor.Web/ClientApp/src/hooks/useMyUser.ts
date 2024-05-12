import { useEffect, useState } from "react";
import { User } from "../models/user";
import axios from "axios";

export const useMyUser = () => {
  const [user, setUser] = useState<User | undefined>();

  useEffect(() => {
    axios.get("/users/me").then((response) => {
      setUser(response.data);
    }).catch(() => setUser(undefined));
  }, []);

  return user;
};
