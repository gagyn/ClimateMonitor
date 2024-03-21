import logging
import time
import dateutil
from uuid import UUID
from api.data_sender import DataSender
from models.record import Record


class SendRecordRetryer:
    def __init__(self, dataSender: DataSender):
        self._data_sender = dataSender
        self._backup_file_name = "failed_data.txt"

    def save_failed_request(self, record: Record):
        with open(self._backup_file_name, "a") as file:
            file.write(
                f"{record.temperature}|{record.humidity}|{record.sensor_id}|{record.read_at}\n"
            )

    async def run(self):
        while True:
            try:
                with open(self._backup_file_name, "w") as file:
                    for line in file:
                        record = self._read_line(line)
                        self._data_sender.send_record(record)
            except Exception:
                logging.exception("message")
                time.sleep(3)

    def _read_line(self, line: str):
        values = line.split("|")
        return Record(
            float(values[0]),
            float(values[1]),
            UUID(values[2]),
            dateutil.parser(values[3]),
        )
