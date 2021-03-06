import time
import threading
from rpi_pc import *
from rpi_arduino import *


class Main(threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)
        
        self.pc = PC()
        self.sr = Arduino()

        self.pc.connect()
        self.sr.connect()
        
        fmessage = '\n-----Main Thread Connected-----'
        print(fmessage)

    def readPCMsg(self):
        while True:
            try:
                pMsg = self.pc.read()
                if not pMsg:
                    break
                self.pc.write(str(sMsg))
            except Exception as e:
                fmessage = '\nError in PC read: ' + str(e)
                print(fmessage)

    def readSerialMsg(self):
        while True:
            try:
                sMsg = self.sr.read()
                if not sMsg:
                    break
                self.pc.write(str(sMsg))
            except Exception as e:
                fmessage = '\nError in Serial read: ' + str(e)
                print(fmessage)

    def close(self):
        try:
            self.pc.disconnect()
            fmessage = '\nClosing PC'
            print(fmessage)
            self.sr.disconnect()
            fmessage = '\nClosing Serial'
            print(fmessage)
        except Exception as e:
            fmessage = '\nError in Closing at MAIN :' + str(e)
            print(fmessage)
            

if __name__ == "__main__":
    testMain = Main()

    pcReadThread = threading.Thread(target=testMain.readPCMsg, name="PC Read Thread")
    serReadThread = threading.Thread(target=testMain.readSerialMsg, name="Serial Read Thread")
    pcReadThread.daemon = True
    serReadThread.daemon = True

    pcReadThread.start()
    serReadThread.start()

    fmessage = '-------Begin testing now-------\n'
    print(fmessage)
    
    while True:
        try:
            pcReadThread.join(0.1)
            serReadThread.join(0.1)
            if not pcReadThread.isAlive():
                break
            if not serReadThread.isAlive():
                break
            
        except KeyboardInterrupt:
            print('\nInterrupted! Closing program.')
            testMain.close()
            break
        
    print('\nInterrupted! Closing program.')
    testMain.close()
