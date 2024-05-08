import { useEffect, useState } from "react";
import { User } from "../models/user";
import axios from "axios";

export const useMyUser = () => {
  const [user, setUser] = useState<User>();

  useEffect(() => {
    axios.get("https://localhost:7248/Users/me").then((response) => {
      setUser(response.data);
    });
  }, []);

  return user;
};
