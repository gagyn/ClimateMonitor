import axios from "axios";
import { LoginForm } from "../models/loginForm";
import { useLogout } from "./useLogout";
import { useEffect } from "react";

export const useLogin = () => {
  const logout = useLogout();
  useEffect(() => { logout() }, [logout]);

  return async (login: LoginForm) => {
    if (!login?.username || !login.password) {
      return false;
    }
    const response = await axios.post(`/users/login?useCookies=true`, login, {
      headers: {
        'Content-Type': 'application/json'
      },
      validateStatus: _ => true
    });
    return response.status === 200;
  }
}
