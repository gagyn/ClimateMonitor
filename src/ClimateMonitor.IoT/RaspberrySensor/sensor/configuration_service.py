import json
import os
import logging
from uuid import UUID
from api.configuration_receiver import ConfigurationObserver
from models.sensors_configuration import SensorsConfiguration


class ConfigurationService(ConfigurationObserver):
    current_configuration: SensorsConfiguration
    _sensors_config_filename = "sensors_config.json"

    def __init__(self) -> None:
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
