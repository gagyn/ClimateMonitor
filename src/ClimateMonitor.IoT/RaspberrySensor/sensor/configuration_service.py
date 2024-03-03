from RaspberrySensor.models.sensors_configuration import SensorsConfiguration


class ConfigurationService:
    current_configuration: SensorsConfiguration

    def update_configuration_handler(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        self.current_configuration = new_configuration
