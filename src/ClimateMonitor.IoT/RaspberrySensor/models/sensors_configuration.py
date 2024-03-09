from uuid import UUID


class SensorsConfiguration:
    def __init__(
        self,
        reading_frequency_cron=dict[UUID, str](),
        pins_dht11=dict[UUID, int](),
        pins_dht22=dict[UUID, int](),
        pins_dallas_18b20=dict[UUID, int](),
    ):
        self.reading_frequency_cron = reading_frequency_cron or {}
        self.pins_dht11 = pins_dht11 or {}
        self.pins_dht22 = pins_dht22 or {}
        self.pins_dallas_18b20 = pins_dallas_18b20 or {}
