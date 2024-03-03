from uuid import UUID

class SensorsConfiguration:
    reading_frequency_cron: dict[UUID, str]
    pins_dht11: dict[UUID, int]
    pins_dht22: dict[UUID, int]
    pins_dallas_18b20: dict[UUID, int]