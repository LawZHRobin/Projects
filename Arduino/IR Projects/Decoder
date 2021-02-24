/*
 * Code serves as a decoder for your infrared remote controller
 */

// Library by shirriff, z3t0, ArminJo (ver-2.8.0)
#include <IRremote.h>

// Define Arduino-Sensor Pin
int recv = 4;

// Define IR Receiver object
IRrecv irc(recv);

void setup () {
  // Check your serial monitor for the hexadecimal values
  Serial.begin(9600);

  // Start the receiver
  irc.enableIRIn();
}

void loop () {

  // Decodes the keypress received from the infrared remote controller
  if (irc.decode()) {
    Serial.println(irc.results.value, HEX);

    // Delay used to prevent noise from inputs
    delay(60);

    // Receive the next keypress value
    irc.resume();  
  }
}
