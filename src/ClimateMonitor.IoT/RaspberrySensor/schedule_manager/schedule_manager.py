import time
from typing import NoReturn
from uuid import UUID
from api.configuration_reciever import ConfigurationObserver
from api.safe_data_sender import SafeDataSender
from models.app_configuration import AppConfiguration
from models.sensors_configuration import SensorsConfiguration
from schedule import schedule
from sensor.configuration_service import ConfigurationService
from sensor.sensor_reader import SensorReader


class ScheduleManager(ConfigurationObserver):
    _configuration_service: ConfigurationService
    _sensor_reader: SensorReader
    _app_configuration: AppConfiguration
    _safe_data_sender: SafeDataSender

    def __init__(
        self,
        app_configuration: AppConfiguration,
        configuration_service: ConfigurationService,
        sensor_reader: SensorReader,
        safe_data_sender: SafeDataSender,
    ):
        self._configuration_service = configuration_service
        self._sensor_reader = sensor_reader
        self._app_configuration = app_configuration
        self._safe_data_sender = safe_data_sender
        self.handle_configuration_update(configuration_service.current_configuration)

    def start_executing(self) -> NoReturn:
        while True:
            schedule.run_pending()
            time.sleep(1)

    def handle_configuration_update(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        for sensor_id in (
            new_configuration.pins_dht11
            | new_configuration.pins_dht22
            | new_configuration.pins_dallas_18b20
        ):
            self._schedule_handler(
                sensor_id, self._app_configuration, self._configuration_service
            )

    def _schedule_handler(
        self,
        sensor_id: UUID,
        new_config: SensorsConfiguration,
    ):
        cron = new_config.reading_frequency_cron[sensor_id]
        schedule.clear()
        schedule.every().crontab_expression(cron).do(
            self._read_and_save, sensor_id, self._sensor_reader, self._safe_data_sender
        )

    def _read_and_save(
        sensor_id: UUID, sensor_reader: SensorReader, safe_data_sender: SafeDataSender
    ):
        record = sensor_reader.read_sensor_data(sensor_id)
        safe_data_sender.send_record(record)
