import json
import requests
from models.app_configuration import AppConfiguration
from models.record import Record


class SafeDataSender:
    _app_configuration: AppConfiguration

    def __init__(self, appConfiguration: AppConfiguration):
        self._app_configuration = appConfiguration

    def send_record(self, record: Record) -> None:
        url = (
            self._app_configuration.baseApiUrl + self._app_configuration.uploadDataPath
        )
        payload = json.dumps(record)
        try:
            response = requests.post(url, json=payload)
            if response.status_code == 200:
                print("Data sent to API successfully.")
            else:
                print("Failed to send data to API. Status code:", response.status_code)
                self._backup_request_on_fail(record)
        except Exception as e:
            print("Exception occurred while sending data to API:", str(e))
            self._backup_request_on_fail(record)

    def _backup_request_on_fail(self, record: Record):
        with open("failed_data.txt", "a") as file:
            file.write(f"{record.temperature}|{record.humidity}|{record.sensor_id}\n")
