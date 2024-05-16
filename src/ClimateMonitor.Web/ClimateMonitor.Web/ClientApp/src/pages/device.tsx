import { Box, Container, CssBaseline } from "@mui/material";
import { useParams } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { NavigationPanel } from "../components/navigation-panel";
import { DeviceDetails } from "../components/settings/device-details";
import { useMyUser } from "../hooks/useMyUser";
import { UUID } from "crypto";

export function Device() {
    const { id } = useParams();
    const user = useMyUser();
    return (
        <Box sx={{ display: "flex" }}>
            <Box sx={{ diplay: "flex" }}>
                <ToastContainer />
                <CssBaseline />
                <NavigationPanel myUser={user} />
            </Box>
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <Container>
                    <p>Device</p>
                    <DeviceDetails id={id as UUID} />
                </Container>
            </Box>
        </Box>
    );
}