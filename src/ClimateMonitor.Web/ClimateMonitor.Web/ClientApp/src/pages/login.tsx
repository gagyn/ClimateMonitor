import { yupResolver } from '@hookform/resolvers/yup';
import { Input } from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import * as yup from "yup";
import { useLogin } from "../hooks/useLogin";
import { LoginForm } from "../models/loginForm";

const schema = yup.object({
    username: yup.string().required(),
    password: yup.string().required()
}).required();

export function Login() {
    const { register, handleSubmit, watch, control, formState: { errors } } = useForm<LoginForm>({
        defaultValues: {
            username: "",
            password: ""
        },
        resolver: yupResolver(schema)
    });

    const { login } = useLogin();

    const onSubmit = () => {
        login(watch());
    }
    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <Controller
                name="username"
                control={control}
                render={({ field }) => <Input {...field} />}
            />
            <Controller
                name="password"
                control={control}
                render={({ field }) => <Input {...field} />}
            />
            <input type="submit" />
        </form>
    )
}