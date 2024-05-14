import axios, { AxiosError, isAxiosError } from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { User } from "../models/user";

export const useMyUser = () => {
  const [user, setUser] = useState<User | undefined>();
  const navigate = useNavigate();

  useEffect(() => {
    axios.get("/users/me", { maxRedirects: 0 })
      .then((response) => setUser(response.status === 200 ? response.data : undefined))
      .catch((err: Error | AxiosError) => {
        if (!isAxiosError(err)) {
          throw err;
        }
        if (err.response?.status === 401) {
          navigate("/login");
        }
        return setUser(undefined);
      });
  }, []);

  return user;
};
