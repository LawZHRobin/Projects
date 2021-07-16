#include <WiFi.h> // Library for Wi-Fi
#include "ThingSpeak.h" 

#include <DHT.h>
#define DHTTYPE DHT11  // Defines the type of DHT sensor you are using (DHT11/DHT22)
const int DHTPin = 15; // Change accordingly to the GPIO pin used on your ESP32
                       // This pin is also connected to the serial data pin of your DHT sensor

DHT dht(DHTPin, DHTTYPE); // Declare an object

const char* ssid = "yourssid";   // Replace with your SSID
const char* password = "yourwifipassword";   // Replace with your WIFI password

WiFiClient  client;

unsigned long Channel_ID = 10*****;    // Replace with your Channel ID
const char * API_key = "B2*********7"; // Replace with your API key

#define LEDonBoard 2

void setup() {
  Serial.begin(115200);  
  
  WiFi.mode(WIFI_STA);   // Devices that connect to Wi-Fi networks are called stations (STA).
  
  ThingSpeak.begin(client);  // Initialize ThingSpeak
}

void loop() {

    // Connect or reconnect to WiFi
    if(WiFi.status() != WL_CONNECTED){
      Serial.print("Attempting to connect to SSID: ");

      pinMode(LEDonBoard,OUTPUT); 
      digitalWrite(LEDonBoard, HIGH); // Turn off on board LED
      
      while(WiFi.status() != WL_CONNECTED){
        WiFi.begin(ssid, password);  // Connect to WPA/WPA2 network. Change this line if using open or WEP network
        Serial.print(".");

        //---------------------------------------- On Board Flashing LED to signify process of connecting to wifi
        digitalWrite(LEDonBoard, LOW);
        delay(250);
        digitalWrite(LEDonBoard, HIGH);
        delay(250);
        //----------------------------------------
        
        delay(5000);     
      } 
      Serial.println("\nConnected.");
      digitalWrite(LEDonBoard, LOW);
    }

    //---------------------------------------- On board LED blinks to indicate the program is running.
    digitalWrite(LEDonBoard, HIGH);
    delay(750);
    digitalWrite(LEDonBoard, LOW);
    delay(250);
    //----------------------------------------
    
    delay(15000); // Wait 15 seconds to update the channel again
  
    float h = dht.readHumidity();
    float t = dht.readTemperature(); // Note that this is in degree celsius
      
    if (isnan(h) || isnan(t)) {
      Serial.println("Failed to read from DHT sensor!");
      return;
    }

    ThingSpeak.setField(1, t);
    ThingSpeak.setField(2, h);

    Serial.print("Temperature: ");
    Serial.print(t);
    Serial.print(" degrees Celcius, Humidity: ");
    Serial.print(h);
    Serial.println("%. Sending to Thingspeak.");

    int x = ThingSpeak.writeFields(Channel_ID, API_key);

    if(x == 200){
      Serial.println("Channel update successful.");
    }
    else{
      Serial.println("Problem updating channel. HTTP error code " + String(x));
    }
    
}
