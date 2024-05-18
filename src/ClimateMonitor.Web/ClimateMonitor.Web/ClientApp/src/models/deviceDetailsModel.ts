import { UUID } from "crypto";

export type DeviceDetailsModel = {
  deviceId: UUID;
  readingFrequencyCrons: { [key: UUID]: string };
  pinsDHT11: { [key: UUID]: string };
  pinsDHT22: { [key: UUID]: string };
  pinsDallas18b20: { [key: UUID]: string };
};
