import json
import websockets
from websockets.sync.client import connect

from RaspberrySensor.models.app_configuration import AppConfiguration


class ConfigurationReciever:
    _app_configuration: AppConfiguration

    def __init__(self, app_configuration: AppConfiguration) -> None:
        self._app_configuration = app_configuration

    async def connect(self):
        async for websocket in websockets.connect(
            self._app_configuration.websocketAddress
        ):
            try:
                self._handler(websocket)
            except websockets.ConnectionClosed:
                continue

    async def _handler(self, websocket):
        async for message in websocket:
            print(message)
