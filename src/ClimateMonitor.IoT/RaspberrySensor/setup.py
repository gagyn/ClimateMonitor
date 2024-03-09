import asyncio
from api.configuration_receiver import ConfigurationReceiver
from api.safe_data_sender import SafeDataSender
from models.app_configuration import read_configuration_file
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
    loop = asyncio.new_event_loop()
    asyncio.set_event_loop(loop)

    config_file_path = "RaspberrySensor/config.json"
    app_config = read_configuration_file(config_file_path)

    config_service = ConfigurationService()
    configuration_receiver = ConfigurationReceiver(app_config)

    sensor_reader = SensorReader(config_service)
    safe_data_sender = SafeDataSender(app_config)
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
