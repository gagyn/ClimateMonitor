import asyncio
import json
import ssl
import time
import websockets
import logging
from api.token_provider import TokenProvider
from models.app_configuration import AppConfiguration
from models.device_id_provider import DeviceIdProvider
from models.sensors_configuration import SensorsConfiguration


class ConfigurationObserver:
    def handle_configuration_update(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        pass


class ConfigurationReceiver:
    _app_configuration: AppConfiguration
    _observers: list[ConfigurationObserver] = []
    _get_configuration_message: str
    _terminating_character = chr(0x1E)

    def __init__(
        self,
        app_configuration: AppConfiguration,
        device_id_provider: DeviceIdProvider,
        token_provider: TokenProvider,
    ):
        self._app_configuration = app_configuration
        self._device_id_provider = device_id_provider
        self._token_provider = token_provider
        token = token_provider.getAccessToken()
        self._get_configuration_message = (
            json.dumps(
                {
                    "type": 1,
                    "headers": {"Authorization": "Bearer " + token},
                    "target": "GetConfiguration",
                    "arguments": [],
                }
            )
            + self._terminating_character
        )
        self._handshakeMessage = (
            json.dumps({"protocol": "json", "version": 1}) + self._terminating_character
        )

    async def connect(self):
        ssl_context = ssl.SSLContext(ssl.PROTOCOL_TLSv1_2)
        localhost_pem = "test_localhost.pem"
        ssl_context.load_verify_locations(localhost_pem)

        headers = {"Authorization": f"Bearer {self._token_provider.getAccessToken()}"}
        print("Waiting for connection...")
        while True:
            try:
                async for websocket in websockets.connect(
                    uri=self._app_configuration.websocketAddress,
                    ssl=ssl_context,
                    extra_headers=headers,
                ):
                    print("Websocket connected.")
                    await websocket.send(self._handshakeMessage)

                    print("Sent handshake.")
                    print(await websocket.recv())

                    await websocket.send(self._get_configuration_message)
                    print("DeviceId sent.")

                    await self._handler(websocket)

            except Exception:
                logging.exception("message")
                await asyncio.sleep(3)

    def add_observer(self, observer: ConfigurationObserver) -> None:
        self._observers.append(observer)

    def remove_observer(self, observer: ConfigurationObserver) -> None:
        self._observers.remove(observer)

    async def _handler(self, websocket):
        async for message in websocket:
            print("Got message. Content:", message)
            if "UpdateConfiguration" not in message:
                continue

            config = self._deserialize_response(message)
            self._notify_observers(config)
            print("All observers notified.")

    def _deserialize_response(self, message: str):
        message = message.rstrip(self._terminating_character)
        parsedJson = json.loads(message)["arguments"][0]
        sensors_config = SensorsConfiguration(
            parsedJson["readingFrequencyCrons"],
            parsedJson["pinsDHT11"],
            parsedJson["pinsDHT22"],
            parsedJson["pinsDallas18b20"],
        )
        return sensors_config

    def _notify_observers(self, config: SensorsConfiguration):
        for observer in self._observers:
            observer.handle_configuration_update(config)
