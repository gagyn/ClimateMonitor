import adafruit_dht  # type: ignore
from RaspberrySensor.models.record import Record
from RaspberrySensor.sensor.configuration_service import ConfigurationService


class SensorReader:
    _configuration_service: ConfigurationService

    def __init__(self, configuration_service: ConfigurationService) -> None:
        self._configuration_service = configuration_service

    def read_dht11_sensor_data(self) -> Record:
        dhtSensor = adafruit_dht.DHT11()
        return Record(dhtSensor.temperature, dhtSensor.humidity)
