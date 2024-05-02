import SettingsIcon from "@mui/icons-material/Settings";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import CssBaseline from "@mui/material/CssBaseline";
import Divider from "@mui/material/Divider";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import { Link } from "react-router-dom";
import "./App.css";
import { LocationBody } from "./components/location-body";
import { LocationHeader } from "./components/location-header";

function App() {
  type Location = {
    sensorId: string;
    sensorLocationName: string;
    temperature: number;
  };
  const locations: Location[] = [];

  return (
    <div className="App">
      <Box sx={{ diplay: "flex" }}>
        <CssBaseline />
        <Drawer
          variant="permanent"
          sx={{
            width: 250,
            flexShrink: 0,
            [`& .MuiDrawer-paper`]: { width: 250, boxSizing: "border-box" },
          }}
        >
          <List>
            <ListItem key="settings" disablePadding>
              <ListItemButton>
                <ListItemIcon>
                  <SettingsIcon />
                </ListItemIcon>
                <ListItemText primary={<Link to="/settings">Settings</Link>} />
              </ListItemButton>
            </ListItem>
          </List>
          <Divider />
          <List>
            {locations.map((location) => (
              <ListItem key={location.sensorId} disablePadding>
                <ListItemButton>
                  <ListItemIcon>
                    <SettingsIcon />
                  </ListItemIcon>
                  <ListItemText primary={location.sensorLocationName} />
                </ListItemButton>
              </ListItem>
            ))}
          </List>
        </Drawer>
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
