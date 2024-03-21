from api.data_sender import DataSender
from api.send_record_retryer import SendRecordRetryer
from models.app_configuration import AppConfiguration
from models.record import Record
import logging


class SafeDataSender:
    def __init__(self, appConfiguration: AppConfiguration, dataSender: DataSender, sendRecordRetryer: SendRecordRetryer):
        self._app_configuration = appConfiguration
        self._data_sender = dataSender
        self._send_data_retryer = sendRecordRetryer

    def send_record(self, record: Record) -> None:
        try:
            response = self._data_sender.send_record(record)
            if response.status_code == 200:
                logging.info("Data sent to API successfully.")
            else:
                logging.error(
                    "Failed to send data to API. Status code:", response.status_code
                )
                self._send_data_retryer.save_failed_request(record)
        except Exception as e:
            logging.error("Exception occurred while sending data to API:", str(e))
            self._send_data_retryer.save_failed_request(record)

    
