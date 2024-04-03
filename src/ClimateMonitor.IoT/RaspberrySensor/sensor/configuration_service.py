import json
import os
import logging
from uuid import UUID
import requests
from api.configuration_receiver import ConfigurationObserver
from models.app_configuration import AppConfiguration
from models.sensors_configuration import SensorsConfiguration


class ConfigurationService(ConfigurationObserver):
    current_configuration: SensorsConfiguration
    _device_user_filename = "device-user.json"
    _sensors_config_filename = "sensors_config.json"

    def __init__(self, app_configuration: AppConfiguration):
        self._app_configuration = app_configuration

        if os.path.exists(self._device_user_filename):
            with open(self._device_user_filename, "r") as file:
                deviceId = UUID(file.readline())
        else:
            deviceId = self._register_device()
            with open(self._device_user_filename, "w") as file:
                file.write(str(deviceId))

        self.current_configuration = SensorsConfiguration()
        if os.path.exists(self._sensors_config_filename):
            with open(self._sensors_config_filename, "r") as file:
                try:
                    self.current_configuration = SensorsConfiguration(**json.load(file))
                except:
                    logging.exception("message")

    def handle_configuration_update(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        self.current_configuration = new_configuration
        with open(self._sensors_config_filename, "w") as file:
            file.write(json.dumps(new_configuration.__dict__))
            logging.info("Saved new sensors configuration.")

    def _register_device(self):
        url = (
            self._app_configuration.baseApiUrl
            + self._app_configuration.registerDevicePath
        )
        payload = json.dumps({"userId": self._app_configuration.userId})
        response = requests.post(url, json=payload)
        return UUID(response.text)
