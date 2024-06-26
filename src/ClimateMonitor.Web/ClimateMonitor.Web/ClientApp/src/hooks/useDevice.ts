import axios from "axios";
import { useEffect, useState } from "react";
import { DeviceDetailsModel } from "../models/deviceDetailsModel";
import { UUID } from "crypto";

export const useDevice = (id: UUID) => {
  const [device, setDevice] = useState<DeviceDetailsModel>();

  useEffect(() => {
    axios.get(`/sensors/devices/${id}`).then((response) => {
      setDevice(response.data);
    });
  }, [id]);

  return device;
};
