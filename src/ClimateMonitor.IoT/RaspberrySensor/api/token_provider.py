from datetime import datetime, timedelta

import requests

from models.app_configuration import AppConfiguration
from models.device_id_provider import DeviceIdProvider


class TokenProvider:
    def __init__(
        self, app_configuration: AppConfiguration, device_id_provider: DeviceIdProvider
    ) -> None:
        self._app_configuration = app_configuration
        self._device_id_provider = device_id_provider
        self._valid_till = datetime.min

    def getAccessToken(self):
        if self._valid_till < datetime.now():
            accessToken, expiresIn = self._login()
            self._access_token = accessToken
            self._valid_till = datetime.now() + timedelta(seconds=expiresIn)
        return self._access_token

    def _login(self) -> tuple[str, int]:
        login_url = (
            self._app_configuration.baseApiUrl + self._app_configuration.loginPath
        )
        deviceId = self._device_id_provider.device_id
        login_payload = {
            "username": "Device" + str(deviceId).replace("-", ""),
            "password": str(deviceId),
        }
        response = requests.post(
            url=login_url,
            json=login_payload,
            verify=False,
        )
        responseContent = response.json()
        return responseContent["accessToken"], responseContent["expiresIn"]
