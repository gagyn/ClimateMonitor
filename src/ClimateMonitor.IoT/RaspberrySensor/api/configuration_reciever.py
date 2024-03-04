import json
import websockets
from websockets.sync.client import connect
from models.app_configuration import AppConfiguration
from sensor.configuration_service import ConfigurationService


class ConfigurationReciever:
    # implement subscribe/notify pattern
    _app_configuration: AppConfiguration
    _configuration_service: ConfigurationService

    def __init__(
        self,
        app_configuration: AppConfiguration,
        configuration_service: ConfigurationService,
    ) -> None:
        self._app_configuration = app_configuration
        self._configuration_service = configuration_service

    async def connect(self) -> None:
        async for websocket in websockets.connect(
            self._app_configuration.websocketAddress
        ):
            try:
                self._handler(websocket)
            except websockets.ConnectionClosed:
                continue

    async def _handler(self, websocket) -> None:
        async for message in websocket:
            print(message)
