import socket

# rpi's static pi
host = ''
# port has to the same
port = 5560

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((host, port))

while True:
    command = input("Enter your command: ")
    s.send(str.encode(command))
    # received reply
    reply = s.recv(2048)
    print(reply.decode('utf-8'))

s.close()
