import axios from "axios";
import { useEffect, useState } from "react";
import { DeviceModel } from "../models/deviceModel";

export const useDevices = () => {
  const [devices, setDevices] = useState<DeviceModel[]>();

  useEffect(() => {
    axios.get("/sensors/devices").then((response) => {
      setDevices(response.data);
    });
  }, []);

  return devices;
};
