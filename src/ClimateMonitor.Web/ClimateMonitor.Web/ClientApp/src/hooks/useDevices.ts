import axios from "axios";
import { useEffect, useState } from "react";
import { DeviceModel } from "../models/deviceModel";

export const useDevices = () => {
  const [devices, setDevices] = useState<DeviceModel[]>();

  useEffect(() => {
    axios.get("https://localhost:7248/Sensors/devices").then((response) => {
      setDevices(response.data);
    });
  }, []);

  return devices;
};
