import { Bounce, toast } from "react-toastify";

export type ToastProps = {
    message: string;
}

export function ErrorToast({ message }: ToastProps) {
    return (toast.error(message, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "colored",
        transition: Bounce,
    }));
}