from api.configuration_reciever import ConfigurationReciever
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


if __name__ == "__main__":
    config_file_path = "config.json"
    app_config = read_configuration_file(config_file_path)

    config_service = ConfigurationService()
    configuration_reciver = ConfigurationReciever(app_config)
    configuration_reciver.connect()

    sensor_reader = SensorReader(config_service)
    safe_data_sender = SafeDataSender(app_config)
    schedule_manager = ScheduleManager(
        app_config, config_service, sensor_reader, safe_data_sender
    )
    configuration_reciver.add_observer(config_service)
    configuration_reciver.add_observer(schedule_manager)
    schedule_manager.start_executing()

    # Start a thread to establish websocket connection for config updates
    # websocket_thread = threading.Thread(target=)
    # websocket_thread.daemon = True
    # websocket_thread.start()

    # Start a thread to periodically send sensor data to API
    # sensor_data_thread = threading.Thread(
    #     target=send_sensor_data_periodically, args=(frequency_seconds)
    # )
    # sensor_data_thread.daemon = True
    # sensor_data_thread.start()

    # Keep the main thread running
