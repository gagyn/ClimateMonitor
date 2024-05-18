import { Grid, Button, TextField } from "@mui/material";
import { Link } from "react-router-dom";
import { useDevice } from "../../hooks/useDevice";
import { UUID } from "crypto";

export function DeviceDetails({ id }: { id: UUID }) {
    const device = useDevice(id);
    return (
        <Grid container direction="column" spacing={1}>
            <Grid item>
                <TextField>{device?.deviceId}</TextField>
            </Grid>
            <Grid item>
                {device && Object.keys(device.pinsDHT11)
                    .map((sensorId) => (
                        <Grid item>
                            {device.pinsDHT11[sensorId]}
                        </Grid>))}
            </Grid>
        </Grid>
    )
}