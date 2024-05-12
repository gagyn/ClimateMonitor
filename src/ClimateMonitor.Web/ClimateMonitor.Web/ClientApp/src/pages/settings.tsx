import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import { NavigationPanel } from "../components/navigation-panel";
import { useDevices } from "../hooks/useDevices";
import { useMyUser } from "../hooks/useMyUser";
import { Devices } from "../components/settings/devices";
import { ToastContainer } from "react-toastify";

export function Settings() {
  const devices = useDevices();
  const user = useMyUser();
  return (
    <>
      <Box sx={{ diplay: "flex" }}>
        <ToastContainer />
        <CssBaseline />
        <NavigationPanel myUser={user} />
      </Box>
      <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
        <Container>
          <p>Settings</p>
          <Devices devices={devices ?? []} />
        </Container>
      </Box>
    </>
  );
}
