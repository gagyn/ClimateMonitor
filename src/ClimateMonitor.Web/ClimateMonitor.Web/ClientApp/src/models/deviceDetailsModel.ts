import { UUID } from "crypto";

export type DeviceDetailsModel = {
  deviceId: UUID;
  readingFrequencyCrons: [UUID, string],
  pinsDHT11: [UUID, string],
  pinsDHT22: [UUID, string],
  pinsDallas18b20: [UUID, string]
};
