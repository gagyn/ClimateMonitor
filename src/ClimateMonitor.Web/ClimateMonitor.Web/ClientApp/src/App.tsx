import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import "./App.css";
import { LocationBody } from "./components/location-body";
import { LocationHeader } from "./components/location-header";
import { NavigationPanel } from "./components/navigation-panel";

function App() {
  return (
    <div className="App">
      <Box sx={{ diplay: "flex" }}>
        <CssBaseline />
        <NavigationPanel />
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
