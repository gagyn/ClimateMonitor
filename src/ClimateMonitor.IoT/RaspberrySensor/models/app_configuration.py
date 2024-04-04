import json
from uuid import UUID


class AppConfiguration:
    def __init__(
        self,
        baseApiUrl: str,
        uploadDataPath: str,
        websocketAddress: str,
        registerDevicePath: str,
        loginPath: str,
        userId: UUID,
    ):
        self.baseApiUrl = baseApiUrl
        self.uploadDataPath = uploadDataPath
        self.websocketAddress = websocketAddress
        self.registerDevicePath = registerDevicePath
        self.loginPath = loginPath
        self.userId = userId

    @staticmethod
    def read_configuration_file(file_path: str):
        with open(file_path, "r") as file:
            config_data = json.load(file)
            return AppConfiguration(
                baseApiUrl=config_data.get("baseApiUrl"),
                uploadDataPath=config_data.get("uploadDataPath"),
                websocketAddress=config_data.get("websocketAddress"),
                registerDevicePath=config_data.get("registerDevicePath"),
                loginPath=config_data.get("loginPath"),
                userId=config_data.get("userId"),
            )
