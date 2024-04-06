from datetime import datetime, timedelta

import requests

from models.app_configuration import AppConfiguration


class TokenProvider:
    def __init__(self, app_configuration: AppConfiguration) -> None:
        self._app_configuration = app_configuration

    def getAccessToken(self):
        if self._valid_till < datetime.now:
            accessToken, expiresIn = self._login()
            self._access_token = accessToken
            self._valid_till = datetime.now + timedelta(seconds=expiresIn)
        return self._access_token

    def _login(self) -> tuple[str, int]:
        login_url = (
            self._app_configuration.baseApiUrl + self._app_configuration.loginPath
        )
        deviceId = self._device_id_provider.device_id
        login_payload = {
            "username": "Device" + str(deviceId).replace("-", ""),
            "password": deviceId,
        }
        response = requests.post(url=login_url, data=login_payload)
        responseContent = response.json()
        return responseContent["accessToken"], responseContent["expiresIn"]
