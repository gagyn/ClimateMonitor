import { yupResolver } from '@hookform/resolvers/yup';
import { Box, Button, Grid, TextField } from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import { useNavigate } from 'react-router-dom';
import * as yup from "yup";
import { ErrorToast } from '../components/shared/toast';
import { useLogin } from "../hooks/useLogin";
import { LoginForm } from "../models/loginForm";
import { ToastContainer } from 'react-toastify';

const schema = yup.object({
    username: yup.string().required(),
    password: yup.string().required()
}).required();

export function Login() {
    const navigate = useNavigate();
    const { handleSubmit, control } = useForm<LoginForm>({
        defaultValues: {
            username: "",
            password: ""
        },
        resolver: yupResolver(schema)
    });

    const login = useLogin();

    const onSubmit = async (form: LoginForm) => {
        const success = await login(form);
        if (success) {
            navigate("/");
            return;
        }
        ErrorToast({ message: "Provided credentials are incorrect. Please check your username and password." })
    }
    return (
        <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
            <ToastContainer />
            <form onSubmit={handleSubmit(onSubmit)}>
                <Grid
                    container
                    spacing={3}
                    direction={"column"}
                    justifyContent={"center"}
                    alignItems={"center"}
                >
                    <Grid item xs={12}>
                        <Controller
                            name="username"
                            control={control}
                            render={({ field, formState: { errors } }) =>
                                <TextField label="Username" {...field} error={!!errors.username} />}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <Controller
                            name="password"
                            control={control}
                            render={({ field, formState: { errors } }) =>
                                <TextField label="Password" type='password' {...field} error={!!errors.password} />}
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <Button type="submit">Login</Button>
                    </Grid>
                </Grid>
            </form>
        </Box>
    )
}