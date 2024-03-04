import json
import os
import threading
import time
from api.configuration_reciever import ConfigurationReciever
from api.safe_data_sender import SafeDataSender
from models.app_configuration import AppConfiguration, read_configuration_file
from models.sensors_configuration import SensorsConfiguration
from schedule import schedule
from sensor.configuration_service import ConfigurationService
from sensor.sensor_reader import SensorReader

# from RaspberrySensor.sc
# Steps realized by this program
# 1. Request to API for downloading currect configuration
# 2. Register websockets to API, listening for configuration changes
# 3. Read jobs are scheduled for each sensor seperately, using schedule package
#
# TODO:
# https://schedule.readthedocs.io/en/stable/exception-handling.html


def read_and_save(sensor_reader: SensorReader, safe_data_sender: SafeDataSender):
    record = sensor_reader.read_dht11_sensor_data()
    safe_data_sender.send_record(record)


def schedule_handler(
    app_config: AppConfiguration, config_service: ConfigurationService
):
    sensor_reader = SensorReader(config_service)
    safe_data_sender = SafeDataSender(app_config)
    cron = "* * * * *"
    schedule.every().crontab_expression(cron).do(
        read_and_save, sensor_reader, safe_data_sender
    )


if __name__ == "__main__":
    config_file_path = "config.json"
    app_config = read_configuration_file(config_file_path)

    sensors_config = SensorsConfiguration()
    sensors_config_filename = "sensors_config.json"
    if os.path.exists(sensors_config_filename):
        with open(sensors_config_filename, "r") as file:
            sensors_config = json.load(file)

    config_service = ConfigurationService()
    configuration_reciver = ConfigurationReciever(app_config, config_service)
    configuration_reciver.connect()

    schedule_handler(app_config, config_service)

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
    while True:
        schedule.run_pending()
        time.sleep(1)
