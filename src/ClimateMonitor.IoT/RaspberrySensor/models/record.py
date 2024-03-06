from uuid import UUID


class Record:
    def __init__(
        self, temperature: float | None, humidity: float | None, sensor_id: UUID
    ):
        self.temperature = temperature
        self.humidity = humidity
        self.sensor_id = sensor_id
