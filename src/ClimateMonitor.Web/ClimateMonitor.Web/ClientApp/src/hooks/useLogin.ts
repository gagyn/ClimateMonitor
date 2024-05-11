import { useEffect, useState } from "react";
import axios from "axios";
import { LoginForm } from "../models/loginForm";

export const useLogin = () => {
  const [loginFrom, setLoginForm] = useState<LoginForm>();
  useEffect(() => {
    if (!loginFrom?.username || !loginFrom.password) return;

    axios.post(`/users/login?useCookies=true`, loginFrom, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }, [loginFrom]);

  return {
    login: (login: LoginForm) => setLoginForm(login)
  };
}