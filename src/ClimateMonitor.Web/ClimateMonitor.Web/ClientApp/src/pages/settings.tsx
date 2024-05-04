import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import { LocationBody } from "../components/location-body";
import { LocationHeader } from "../components/location-header";
import { NavigationPanel } from "../components/navigation-panel";

export function Settings() {
    return (
        <>
            <Box sx={{ diplay: "flex" }}>
                <CssBaseline />
                <NavigationPanel />
            </Box>
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <Container>
                    <p>Settings</p>

                </Container>
            </Box>
        </>)
}