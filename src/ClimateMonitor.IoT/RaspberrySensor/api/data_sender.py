import json
import requests
from requests import Response
from models.app_configuration import AppConfiguration
from models.record import Record


class DataSender:
    _app_configuration: AppConfiguration

    def __init__(self, appConfiguration: AppConfiguration):
        self._app_configuration = appConfiguration

    def send_record(self, record: Record) -> Response:
        url = (
            self._app_configuration.baseApiUrl + self._app_configuration.uploadDataPath
        )
        payload = json.dumps(record)
        accessToken = ""  # todo: use correct access token
        headers = {"Authorization": "Bearer " + accessToken}
        response = requests.post(url, json=payload, headers=headers)
        return response
