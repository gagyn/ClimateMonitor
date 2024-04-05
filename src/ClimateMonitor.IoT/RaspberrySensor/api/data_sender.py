import json
import requests
from requests import Response
from api.token_provider import TokenProvider
from models.app_configuration import AppConfiguration
from models.record import Record


class DataSender:
    def __init__(
        self, appConfiguration: AppConfiguration, token_provider: TokenProvider
    ):
        self._app_configuration = appConfiguration
        self._token_provider = token_provider

    def send_record(self, record: Record) -> Response:
        url = (
            self._app_configuration.baseApiUrl + self._app_configuration.uploadDataPath
        )
        payload = json.dumps(record)
        accessToken = self._token_provider.getAccessToken()
        headers = {"Authorization": "Bearer " + accessToken}
        response = requests.post(url, json=payload, headers=headers)
        return response
