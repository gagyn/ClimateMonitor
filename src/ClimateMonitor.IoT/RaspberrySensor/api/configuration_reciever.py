import websockets
from models.app_configuration import AppConfiguration
from models.sensors_configuration import SensorsConfiguration


class ConfigurationObserver:
    def handle_configuration_update(
        self, new_configuration: SensorsConfiguration
    ) -> None:
        pass


class ConfigurationReciever:
    _app_configuration: AppConfiguration
    _observers: list[ConfigurationObserver] = []

    def __init__(
        self,
        app_configuration: AppConfiguration,
    ):
        self._app_configuration = app_configuration

    async def connect(self) -> None:
        try:
            async for websocket in websockets.connect(
                self._app_configuration.websocketAddress
            ):
                await websocket.send("connected")
                try:
                    await self._handler(websocket)
                except websockets.ConnectionClosed:
                    continue
        except Exception as e:
            print(e)

    async def _handler(self, websocket):
        async for message in websocket:
            self._notify_observers(message)

    def add_observer(self, observer: ConfigurationObserver) -> None:
        self._observers.append(observer)

    def remove_observer(self, observer: ConfigurationObserver) -> None:
        self._observers.remove(observer)

    async def _notify_observers(self, message):
        for observer in self._observers:
            observer.handle_configuration_update(message)
