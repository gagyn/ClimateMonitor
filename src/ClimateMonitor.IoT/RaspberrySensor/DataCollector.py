import json
import os
import threading
import time

from SensorReader import establish_websocket_connection, send_sensor_data_periodically

# Steps realized by this program
# 1. Request to API for downloading currect configuration
# 2. Register websockets to API, listening for configuration changes
# 3. Read jobs are scheduled for each sensor seperately, using schedule package
#
# TODO:
# https://schedule.readthedocs.io/en/stable/exception-handling.html

if __name__ == "__main__":
    if not os.path.exists("config.json"):
        default_config = {"input_port": "GPIO4", "frequency_seconds": 60}
        with open("config.json", "w") as config_file:
            json.dump(default_config, config_file)

    # Start a thread to establish websocket connection for config updates
    websocket_thread = threading.Thread(target=establish_websocket_connection)
    websocket_thread.daemon = True
    websocket_thread.start()

    # Read configuration from file
    with open("config.json", "r") as config_file:
        config_data = json.load(config_file)
        input_port = config_data["input_port"]
        frequency_seconds = config_data["frequency_seconds"]

    # Start a thread to periodically send sensor data to API
    sensor_data_thread = threading.Thread(
        target=send_sensor_data_periodically, args=(frequency_seconds)
    )
    sensor_data_thread.daemon = True
    sensor_data_thread.start()

    # Keep the main thread running
    while True:
        time.sleep(1)
