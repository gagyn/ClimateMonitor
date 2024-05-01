import SettingsIcon from "@mui/icons-material/Settings";
import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import Drawer from "@mui/material/Drawer";
import "./App.css";
import { LocationBody } from "./components/location-body";
import { LocationHeader } from "./components/location-header";
import { Locations } from "./components/locations";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import List from "@mui/material/List";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import InboxIcon from "@mui/icons-material/MoveToInbox";
import MailIcon from "@mui/icons-material/Mail";
import Container from "@mui/material/Container";

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
                <ListItemText primary="Settings" />
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
