import asyncio
import json
import ssl
from typing import NoReturn
from uuid import UUID
from signalrcore.hub_connection_builder import HubConnectionBuilder  # type: ignore
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

    def __init__(
        self,
        app_configuration: AppConfiguration,
        token_provider: TokenProvider,
    ):
        self._app_configuration = app_configuration
        self._token_provider = token_provider

    async def connect(self) -> NoReturn:
        hub_connection = (
            HubConnectionBuilder()
            .with_url(
                self._app_configuration.websocketAddress,
                options={
                    "access_token_factory": lambda: self._token_provider.getAccessToken(),
                    "verify_ssl": False,
                },
            )
            .configure_logging(logging.DEBUG)
            .with_automatic_reconnect(
                {
                    "type": "raw",
                    "keep_alive_interval": 30,
                    "reconnect_interval": 20,
                    "max_attempts": 10,
                }
            )
            .build()
        )
        print("Waiting for connection...")
        hub_connection.on("ConfigurationRecieved", self._handle_new_config)
        hub_connection.start()
        while True:
            await asyncio.sleep(1)

    def add_observer(self, observer: ConfigurationObserver) -> None:
        self._observers.append(observer)

    def remove_observer(self, observer: ConfigurationObserver) -> None:
        self._observers.remove(observer)

    def _handle_new_config(self, message: list[dict[str, dict[UUID, int]]]):
        config = self._deserialize_response(message)
        self._notify_observers(config)
        print("All observers notified.")

    def _deserialize_response(self, message: list[dict[str, dict[UUID, int]]]):
        configuration = message[0]
        sensors_config = SensorsConfiguration(
            configuration["readingFrequencyCrons"],
            configuration["pinsDHT11"],
            configuration["pinsDHT22"],
            configuration["pinsDallas18b20"],
        )
        return sensors_config

    def _notify_observers(self, config: SensorsConfiguration):
        for observer in self._observers:
            observer.handle_configuration_update(config)
