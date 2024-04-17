import logging
import os
from models.app_configuration import AppConfiguration
import requests
import json
from uuid import UUID


class DeviceIdProvider:
    device_id: UUID
    _device_user_filename = "device_user.json"

    def __init__(self, app_configuration: AppConfiguration):
        self._app_configuration = app_configuration

        if os.path.exists(self._device_user_filename):
            self.device_id = self._read_device_id_file()
        else:
            self.device_id = self._register_device()
            self._write_device_id_file()

    def _read_device_id_file(self):
        with open(self._device_user_filename, "r") as file:
            config_data = json.load(file)
            return UUID(config_data.get("deviceId"))

    def _write_device_id_file(self):
        with open(self._device_user_filename, "w") as file:
            file.write(json.dumps({"deviceId": str(self.device_id)}))

    def _register_device(self):
        url = (
            self._app_configuration.baseApiUrl
            + self._app_configuration.registerDevicePath
        )
        payload = {"userId": self._app_configuration.userId}
        response = requests.post(
            url,
            data=json.dumps(payload),
            verify=False,
        )
        if response.status_code != 200:
            logging.error(response.status_code)
        return UUID(response.text.strip('"'))
