import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.min.css';
import "./App.css";
import { LocationBody } from "./components/location-body";
import { LocationHeader } from "./components/location-header";
import { NavigationPanel } from "./components/navigation-panel";
import { useMyUser } from "./hooks/useMyUser";

function App() {
  const user = useMyUser();
  return (
    <div className="App">
      <ToastContainer />
      <Box sx={{ diplay: "flex" }}>
        <CssBaseline />
        <NavigationPanel myUser={user} />
      </Box>
      <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
        <Container>
          <LocationHeader />
          <LocationBody />
        </Container>
      </Box>
    </div>
  );
}

export default App;
