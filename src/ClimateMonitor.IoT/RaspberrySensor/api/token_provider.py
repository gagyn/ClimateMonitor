from datetime import datetime


class TokenProvider:
    def __init__(self) -> None:
        pass

    def getAccessToken(self):
        if self._valid_till < datetime.now:
            self._access_token = ""
            self._valid_till = datetime.now + 1  # todo: add one hour
        return self._access_token
