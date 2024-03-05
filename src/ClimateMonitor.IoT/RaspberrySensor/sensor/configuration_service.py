from api.configuration_reciever import ConfigurationObserver
from models.sensors_configuration import SensorsConfiguration


class ConfigurationService(ConfigurationObserver):
    current_configuration: SensorsConfiguration

    def __init__(self, sensors_configuration: SensorsConfiguration) -> None:
        self.current_configuration = sensors_configuration

    def update_configuration_handler(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        self.current_configuration = new_configuration
