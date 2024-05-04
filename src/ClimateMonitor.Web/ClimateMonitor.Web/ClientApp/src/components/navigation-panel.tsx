import SettingsIcon from "@mui/icons-material/Settings";
import Divider from "@mui/material/Divider";
import Drawer from "@mui/material/Drawer";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import { Link } from "react-router-dom";

export function NavigationPanel() {
    type Location = {
        sensorId: string;
        sensorLocationName: string;
        temperature: number;
    };
    const locations: Location[] = [];

    return (<Drawer
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
    </Drawer>)
}