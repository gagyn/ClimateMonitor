class Configuration:
    reading_frequency_seconds: int
    pins_dht11: list[int]
    pins_dht22: list[int]
    pins_dallas_18b20: list[int]


class ConfigurationService:
    current_configuration: Configuration

    def update_configuration_handler(self, new_configuration: Configuration) -> None:
        self.current_configuration = new_configuration
