import { Button } from "@mui/material";
import Grid from "@mui/material/Grid";
import { Link } from "react-router-dom";
import { DeviceModel } from "../../models/deviceModel";

type DevicesProps = {
    devices: DeviceModel[]
}

export function Devices({ devices }: DevicesProps) {
    return (
        <Grid>
            {devices?.map((x) => (
                <Button variant="contained" component={Link} to={`/settings/device/${x.deviceId}`}>Edit device {x.deviceId}</Button>
            ))}
        </Grid>)
}