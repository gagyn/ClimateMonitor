import json
from uuid import UUID


class AppConfiguration:
    def __init__(
        self,
        baseApiUrl: str,
        uploadDataPath: str,
        websocketAddress: str,
        registerDevicePath: str,
        userId: UUID,
    ):
        self.baseApiUrl = baseApiUrl
        self.uploadDataPath = uploadDataPath
        self.websocketAddress = websocketAddress
        self.registerDevicePath = registerDevicePath
        self.userId = userId


def read_configuration_file(file_path) -> AppConfiguration:
    with open(file_path, "r") as file:
        config_data = json.load(file)
        return AppConfiguration(
            baseApiUrl=config_data.get("baseApiUrl"),
            uploadDataPath=config_data.get("uploadDataPath"),
            websocketAddress=config_data.get("websocketAddress"),
            registerDevicePath=config_data.get("registerDevicePath"),
            userId=config_data.get("userId"),
        )
