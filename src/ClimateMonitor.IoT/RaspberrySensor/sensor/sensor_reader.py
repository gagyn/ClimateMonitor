from uuid import UUID
import adafruit_dht  # type: ignore
from models.exceptions.sensor_is_not_configured import SensorIsNotConfigured
from models.record import Record
from sensor.configuration_service import ConfigurationService


class SensorReader:
    _configuration_service: ConfigurationService

    def __init__(self, configuration_service: ConfigurationService) -> None:
        self._configuration_service = configuration_service

    def read_sensor_data(self, sensor_id: UUID) -> Record:
        app_config = self._configuration_service.current_configuration

        if sensor_id in app_config.pins_dht11:
            pin = app_config.pins_dht11[sensor_id]
            dht_sensor = adafruit_dht.DHT11(pin)
            return Record(dht_sensor.temperature, dht_sensor.humidity, sensor_id)

        elif sensor_id in app_config.pins_dht22:
            pin = app_config.pins_dht22[sensor_id]
            dht_sensor = adafruit_dht.DHT22(pin)
            return Record(dht_sensor.temperature, dht_sensor.humidity, sensor_id)

        # todo: implement dallas reading
        elif sensor_id in app_config.pins_dallas_18b20:
            pin = app_config.pins_dallas_18b20[sensor_id]
            dallas_sensor = Record(10.0, None, sensor_id)
            return dallas_sensor

        raise SensorIsNotConfigured
