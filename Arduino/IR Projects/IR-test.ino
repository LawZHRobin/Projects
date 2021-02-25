// Library by shirriff, z3t0, ArminJo (ver-2.8.0)
#include <IRremote.h>
 
// Include Arduino Servo Library
#include <Servo.h>
 
// Define Arduino-Sensor Pin
const int recv = 4;
 
// Define Servo Pin (Actuator)
const int servo = 6;

// Align Servo to 90 Degrees (Center position)
int pos = 90;         
 
// Define variable to store last keypress received
unsigned long lastKey = 0; 
 
// Define IR Receiver and Results Objects
IRrecv irc(recv);
decode_results results;
 
// Create servo object
Servo motor;          
 
void setup()
{
  Serial.begin(9600);
  
  // Start the receiver
  irc.enableIRIn();
   
  // Attach the servo
  motor.attach(servo); 
 
  // Start with Servo in Center-90 degrees
  motor.write(pos); 
}
 
void loop() {
  if(irc.decode()) //this checks to see if a key was pressed
  {
    //Serial.println(irc.results.value, HEX);
    
      if(irc.results.value == 0xFFFFFFFF)   
      {
         // If button is held down, then use last keypress received
         irc.results.value = lastKey;
      }
   
      if(irc.results.value == 0xFF22DD)    
      {
          // Left Button Pressed
          lastKey = irc.results.value;
          // Move left 5 degrees     
          pos += 5; 
          // Prevent position above 180
          if(pos > 180)
            {pos = 180;}                     
          motor.write(pos);      
      }
        
      if(irc.results.value == 0xFFC23D)     
      {
         // Right Button Pressed
         lastKey = irc.results.value;
         // Move Right 5 degrees  
         pos -= 5; 
         // Prevent position below 0
         if(pos < 0)
          {pos = 0;}                   
         motor.write(pos);     
      }
   
     if(irc.results.value == 0xFF02FD)     
      {
         // Center Button Pressed
         lastKey = irc.results.value;
         // Move to Center  
         pos = 90;          
         motor.write(pos);     
      }
   
      // Delay used to prevent noise from inputs
      delay(30); 
      
      //receive the next keypress value  
      irc.resume(); 
  }
    
}
