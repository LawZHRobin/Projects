import serial 

class Arduino(object):
    def __init__(self):
        #self.serial_port = '/dev/ttyACM0'
        #self.serial_port = 'COM8'
        # Symbolic Link from Arduino to RPi
        self.serial_port = '/dev/serial/by-id/<Your Arduino ID>'
        self.baud_rate = 9600
        self.connection = None

    def connect(self):
        try:
            print('Establishing connection with Arduino...')

            self.connection = serial.Serial(self.serial_port, self.baud_rate)

            if self.connection is not None:
                print('Successfully connected with Arduino. ')

        except Exception as e:
            print('Connection with Arduino failed: %s ' % str(e))

    def disconnect(self):
        try:
            if self.connection is not None:
                self.connection.close()
                self.connection = None

                print('Successfully closed connection with Arduino.')

        except Exception as e:
            print('Arduino close connection failed: %s ' % str(e))
            
    def read(self):
        try:
            if self.connection is not None:
                self.connection.flush()
                message = self.connection.readline()
                print('')
                print('From Arduino:')
                print(message)

                if len(message) > 0:
                    return message

            return None
       
        except Exception as e:
            print('Arduino read failed: %s ' % str(e))
            raise e
    
    def write(self, message):
        try:
            #print('To Arduino:')
            #print(message)
            #self.connection.write(str.encode(message))
            self.connection.flush()
            print('To Arduino: ' + message)
            self.connection.write(str(message).encode('utf-8'))

        except Exception as e:
            print('Arduino write failed: %s ' % str(e))
            raise e

# If you want this to be the only process, run this.
if __name__ == "__main__":
    ser = Arduino()
    ser.__init__()
    ser.connect()
    while True:
        try:
            ser.read()
            ser.write('S')
        except KeyboardInterrupt:
            print('Serial Communication Interrupted.')
            ser.disconnect()
            break
