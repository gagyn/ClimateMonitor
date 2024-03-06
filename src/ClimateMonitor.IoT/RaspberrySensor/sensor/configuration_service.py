import json
import os
from api.configuration_reciever import ConfigurationObserver
from models.sensors_configuration import SensorsConfiguration


class ConfigurationService(ConfigurationObserver):
    current_configuration: SensorsConfiguration

    def __init__(self) -> None:
        self.current_configuration = SensorsConfiguration()
        sensors_config_filename = "sensors_config.json"
        if os.path.exists(sensors_config_filename):
            with open(sensors_config_filename, "r") as file:
                self.current_configuration = json.load(file)

    def update_configuration_handler(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        self.current_configuration = new_configuration
