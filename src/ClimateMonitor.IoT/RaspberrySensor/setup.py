import json
import os
import threading
import time
from RaspberrySensor.models.app_configuration import read_configuration_file
from RaspberrySensor.models.sensors_configuration import SensorsConfiguration

# Steps realized by this program
# 1. Request to API for downloading currect configuration
# 2. Register websockets to API, listening for configuration changes
# 3. Read jobs are scheduled for each sensor seperately, using schedule package
#
# TODO:
# https://schedule.readthedocs.io/en/stable/exception-handling.html

if __name__ == "__main__":
    config_file_path = "config.json"
    config = read_configuration_file(config_file_path)

    sensors_config = SensorsConfiguration()
    sensors_config_filename = "sensors_config.json"
    if os.path.exists(sensors_config_filename):
        with open(sensors_config_filename, "r") as file:
            sensors_config = json.load(file)

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
        time.sleep(1)
