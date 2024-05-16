import { Grid, Button } from "@mui/material";
import { Link } from "react-router-dom";
import { useDevice } from "../../hooks/useDevice";
import { UUID } from "crypto";

export function DeviceDetails({ id }: { id: UUID }) {
    const device = useDevice(id);
    return (
        <Grid container direction="column" spacing={1}>

        </Grid>
    )
}