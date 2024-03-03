import adafruit_dht
import websocket
import json
import time
import requests
import threading
import os
from ConfigurationService import ConfigurationService


class SensorReader:
    _configuration_service: ConfigurationService

    def __init__(self, configuration_service: ConfigurationService) -> None:
        self._configuration_service = configuration_service

    def read_dht11_sensor_data(self):
        dhtSensor = adafruit_dht.DHT11()
        return dhtSensor.temperature, dhtSensor.humidity


def send_data_to_api(temperature, humidity):
    url = "http://api.example.com/data"
    payload = {"temperature": temperature, "humidity": humidity}
    try:
        response = requests.post(url, json=payload)
        if response.status_code == 200:
            print("Data sent to API successfully")
        else:
            print("Failed to send data to API. Status code:", response.status_code)
            save_data_to_file(temperature, humidity)
    except Exception as e:
        print("Exception occurred while sending data to API:", str(e))
        save_data_to_file(temperature, humidity)


def save_data_to_file(temperature, humidity):
    with open("failed_data.txt", "a") as file:
        file.write(f"Temperature: {temperature}, Humidity: {humidity}\n")


def on_message(ws, message):
    try:
        config_data = json.loads(message)
        # Handle config update, for example, update local config file
        with open("config.json", "w") as config_file:
            json.dump(config_data, config_file)
        print("Configuration updated:", config_data)
    except Exception as e:
        print("Error handling websocket message:", str(e))


def on_error(ws, error):
    print("WebSocket Error:", error)


def on_close(ws):
    print("WebSocket connection closed")


def establish_websocket_connection():
    websocket.enableTrace(True)
    ws = websocket.WebSocketApp(
        "ws://api.example.com/websocket",
        on_message=on_message,
        on_error=on_error,
        on_close=on_close,
    )
    ws.run_forever()


def send_sensor_data_periodically(interval_seconds):
    while True:
        temperature, humidity = read_sensor_data()
        send_data_to_api(temperature, humidity)
        time.sleep(interval_seconds)
