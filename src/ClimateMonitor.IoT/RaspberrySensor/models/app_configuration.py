import json
from uuid import UUID


class AppConfiguration:
    def __init__(
        self,
        baseApiUrl: str,
        uploadDataPath: str,
        websocketAddress: str,
        deviceId: UUID,
    ):
        self.baseApiUrl = baseApiUrl
        self.uploadDataPath = uploadDataPath
        self.websocketAddress = websocketAddress
        self.deviceId = deviceId


def read_configuration_file(file_path) -> AppConfiguration:
    with open(file_path, "r") as file:
        config_data = json.load(file)
        return AppConfiguration(
            baseApiUrl=config_data.get("baseApiUrl"),
            uploadDataPath=config_data.get("uploadDataPath"),
            websocketAddress=config_data.get("websocketAddress"),
            deviceId=config_data.get("deviceId"),
        )
