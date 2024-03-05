import time
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

    def __init__(
        self,
        app_configuration: AppConfiguration,
        configuration_service: ConfigurationService,
        sensor_reader: SensorReader,
    ) -> None:
        self._configuration_service = configuration_service
        self._sensor_reader = sensor_reader
        self._app_configuration = app_configuration

    def start_executing(self):
        while True:
            schedule.run_pending()
            time.sleep(1)

    def handle_configuration_update(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        for sensor_id in new_configuration.pins_dht11:
            self._schedule_handler(
                sensor_id, self._app_configuration, self._configuration_service
            )

    def _schedule_handler(
        self,
        sensor_id: UUID,
        app_config: AppConfiguration,
        config_service: ConfigurationService,  # todo: use new config instead
    ):
        sensor_reader = SensorReader(config_service)
        safe_data_sender = SafeDataSender(app_config)
        cron = config_service.current_configuration.reading_frequency_cron[sensor_id]
        schedule.clear()
        schedule.every().crontab_expression(cron).do(
            self._read_and_save, sensor_reader, safe_data_sender
        )

    def _read_and_save(sensor_reader: SensorReader, safe_data_sender: SafeDataSender):
        record = sensor_reader.read_dht11_sensor_data()
        safe_data_sender.send_record(record)
