import asyncio
import logging
from api.configuration_receiver import ConfigurationReceiver
from api.data_sender import DataSender
from api.safe_data_sender import SafeDataSender
from api.send_record_retryer import SendRecordRetryer
from api.token_provider import TokenProvider
from models.app_configuration import AppConfiguration
from models.device_id_provider import DeviceIdProvider
from schedule_manager.schedule_manager import ScheduleManager
from sensor.configuration_service import ConfigurationService
from sensor.sensor_reader import SensorReader

# Steps realized by this program
# 1. Request to API for downloading currect configuration
# 2. Register websockets to API, listening for configuration changes
# 3. Read jobs are scheduled for each sensor seperately, using schedule package
#
# TODO:
# https://schedule.readthedocs.io/en/stable/exception-handling.html


async def main():
    logging.basicConfig(
        level=logging.INFO,
        format="%(asctime)s [%(levelname)s] %(message)s",
        handlers=[logging.FileHandler("sensor.log"), logging.StreamHandler()],
    )

    loop = asyncio.new_event_loop()
    asyncio.set_event_loop(loop)

    config_file_path = "config.json"
    app_config = AppConfiguration.read_configuration_file(config_file_path)
    device_id_provider = DeviceIdProvider(app_config)

    config_service = ConfigurationService()
    configuration_receiver = ConfigurationReceiver(app_config, device_id_provider)

    token_provider = TokenProvider()
    sensor_reader = SensorReader(config_service)
    data_sender = DataSender(app_config, token_provider)
    send_record_retryer = SendRecordRetryer(data_sender)
    safe_data_sender = SafeDataSender(app_config, data_sender, send_record_retryer)
    schedule_manager = ScheduleManager(
        app_config, config_service, sensor_reader, safe_data_sender
    )

    configuration_receiver.add_observer(config_service)
    configuration_receiver.add_observer(schedule_manager)

    await asyncio.gather(
        configuration_receiver.connect(), schedule_manager.start_executing()
    )


if __name__ == "__main__":
    asyncio.run(main())
