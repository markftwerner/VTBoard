#!/usr/bin/python

import logging
import logging.handlers
import argparse
import sys
import os
import time
from bluetooth import *


class LoggerHelper(object):
    def __init__(self, logger, level):
        self.logger = logger
        self.level = level

    def write(self, message):
        if message.rstrip() != "":
            self.logger.log(self.level, message.rstrip())


def setup_logging():
    # Location for log
    LOG_FILE = "/home/pi/vtboardsrv/vtboardsrv.log"
    LOG_LEVEL = logging.INFO

    # Check arguments from daemon starter
    argp = argparse.ArgumentParser(description="VTBoard Bluetooth Server")
    argp.add_argument("-l", "--log", help="log (default '" + LOG_FILE + "')")

    # Grab the log file from arguments
    args = argp.parse_args()
    if args.log:
        LOG_FILE = args.log

    # Setup the logger
    logger = logging.getLogger(__name__)
    # Set the log level
    logger.setLevel(LOG_LEVEL)
    # Make an event log that resets at midnight and keeps the last 3 logs
    handler = logging.handlers.TimedRotatingFileHandler(LOG_FILE,
        when="midnight",
        backupCount=3)

    # Log messages include time stamp and log level
    formatter = logging.Formatter('%(asctime)s %(levelname)-8s %(message)s')
    # Attach the formatter to the handler
    handler.setFormatter(formatter)
    # Attach the handler to the logger
    logger.addHandler(handler)

    # Send stdout for info to the log
    sys.stdout = LoggerHelper(logger, logging.INFO)
    # Send sdterr to log
    sys.stderr = LoggerHelper(logger, logging.ERROR)


def main():
    # Setup logging
    setup_logging()

    # Wait until Bluetooth initialization is done
    time.sleep(10)

    # Make device visible
    os.system("sudo hciconfig hci0 piscan")

    # Create a new server socket with RFCOMM protocol
    server_sock = BluetoothSocket(RFCOMM)
    # Bind to any port
    server_sock.bind(("", PORT_ANY))
    # Start listening
    server_sock.listen(1)

    # Get the port the server socket is listening to
    port = server_sock.getsockname()[1]

    # The service UUID to advertise (same as in App)
    uuid = "7be1fcb3-5776-42fb-91fd-2ee7b5bbb86d"

    # Start advertising the service
    advertise_service(server_sock, "VTBoardSrv",
                       service_id=uuid,
                       service_classes=[uuid, SERIAL_PORT_CLASS],
                       profiles=[SERIAL_PORT_PROFILE])

    # Operations the service supports
    operations = ["ping", "test", "test2"]

    # Main Bluetooth server loop
    while True:
		
	#Log activity
        print "Waiting for connection on RFCOMM channel %d" % port

        try:
            client_sock = None

            # This will block until we get a new connection
            client_sock, client_info = server_sock.accept()
            #Log connection message
	    print "Accepted connection from ", client_info

            # Read the data sent by the client
            data = client_sock.recv(1024)
            if len(data) == 0:
                break

            #Prints data received if length is more than zero
	    print "Received [%s]" % data

            # Handle the requests
            if data == "getop":
                response = "op:%s" % ",".join(operations)
            elif data == "ping":
                response = "msg:Pong"
            elif data == "test":
                response = "msg:This is just a test."
            elif data == "test2":
                response = "msg:It works!"
            else:
                response = "msg:Not supported"

            client_sock.send(response)
            #Logs response sent to client
	    print "Sent back [%s]" % response

        except IOError:
            pass

        except KeyboardInterrupt:

            if client_sock is not None:
                client_sock.close()

            server_sock.close()

            print "Server going down"
            break

#Execution of above code
main()
