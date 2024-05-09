import Grid from "@mui/material/Grid";
import { DeviceModel } from "../../models/deviceModel";
import { Button } from "@mui/material";

type DevicesProps = {
    devices: DeviceModel[]
}

export function Devices({ devices }: DevicesProps) {
    return (
        <Grid>
            {devices?.map((x) => (
                <p><Button variant="contained">Edit device {x.deviceId}</Button></p>
            ))}
        </Grid>)
}