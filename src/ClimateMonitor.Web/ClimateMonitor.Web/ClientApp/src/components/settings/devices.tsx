import { Button } from "@mui/material";
import Grid from "@mui/material/Grid";
import { Link } from "react-router-dom";
import { DeviceModel } from "../../models/deviceModel";

type DevicesProps = {
    devices: DeviceModel[]
}

export function Devices({ devices }: DevicesProps) {
    return (
        <Grid container direction="column" spacing={1}>
            {devices?.map((x) => (
                <Grid item key={x.deviceId}>
                    <Button variant="contained" component={Link} to={`/settings/device/${x.deviceId}`}>
                        Edit device {x.deviceId}
                    </Button>
                </Grid>
            ))}
        </Grid>
    )
}