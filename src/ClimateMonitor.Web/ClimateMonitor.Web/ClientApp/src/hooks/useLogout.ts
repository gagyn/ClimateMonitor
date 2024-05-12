import axios from "axios";

export const useLogout = () => {
    return async () => (await axios.get("/users/logout")).status === 200;
};
