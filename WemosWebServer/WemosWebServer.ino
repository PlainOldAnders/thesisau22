
// Load Wi-Fi library
#include <ESP8266WiFi.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

// Replace with your network credentials
const char* ssid     = "FaberNet";
const char* password = "DatBoiPepe";

// Set web server port number to 80
WiFiServer server(80);
// Variable to store the HTTP request
String header;

// Auxiliar variables to store the current output state
String ledState = "off";



// Current time
unsigned long currentTime = millis();
// Previous time
unsigned long previousTime = 0;
// Define timeout time in milliseconds (example: 2000ms = 2s)
const long timeoutTime = 2000;

bool isDebugMode = false;
bool useMag = true;
const unsigned int mysine[] = {1, 39, 156, 345, 600, 910, 1264, 1648, 2048, 2447, 2831, 3185, 3495, 3750, 3939, 4056, 4095, 4056, 3939, 3750, 3495, 3185, 2831, 2447, 2048, 1648, 1264, 910, 600, 345, 156, 39};
int IN1 = 0;      //D3
int IN2 = 2;      //D4
int en1 = 13;     //D7
int vibMotor = 15;//D8
int ledStrip = 12;//D6
int funcBut = 14; //D5

#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 // OLED display height, in pixels
#define OLED_RESET     -1 // Reset pin # (or -1 if sharing Arduino reset pin)
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

bool vibState = true;
bool rhythmState = true;
unsigned long previousMillisVib = 0;
unsigned long previousMillis = 0;
int commonIntensity = 0;
int commonIntensityTarget = 0;
long commonRhythmFreq = 1000;
long intervalVib = 10;

long debugStart = 0;

void setup() {
  Serial.begin(115200);
  pinMode(IN1, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(en1, OUTPUT);
  pinMode(vibMotor, OUTPUT);
  pinMode(ledStrip, OUTPUT);
  pinMode(funcBut, INPUT);
  digitalWrite(ledStrip, LOW);
  digitalWrite(vibMotor, LOW);

  if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    Serial.println(F("SSD1306 allocation failed"));
    for (;;); // Don't proceed, loop forever
  }
  display.display();
  delay(2000); // Pause for 2 seconds

  // Clear the buffer
  display.clearDisplay();
  display.display();
  display.setTextSize(1);
  display.setTextColor(WHITE);

  // Connect to Wi-Fi network with SSID and password
  Serial.print("Connecting to ");
  Serial.println(ssid);
  display.setCursor(0, 0);
  display.println("Connecting...");
  display.display();
  WiFi.begin(ssid, password);
  int i = 0;
  while (WiFi.status() != WL_CONNECTED) {
    delay(80);
    analogWrite(ledStrip, map(mysine[i], 0, 4096, 0, 200));
    i++;
    if (i > 32) i = 0;
    Serial.print(".");
  }
  analogWrite(ledStrip, 200);
  // Print local IP address and start web server
  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  refreshDis("Welcome!", "");
  server.begin();
}

void loop() {
  //Handle server stuff
  unsigned long currentMillis = millis();
  WiFiClient client = server.available();

  debugStart = millis();
  if (client) {

    //analogWrite(en1, 0);
    //Serial.println("New Client." + String(millis()));

    String currentLine = "";
    currentTime = millis();
    previousTime = currentTime;

    while (client.connected() && currentTime - previousTime <= timeoutTime) {
      currentTime = millis();
      if (client.available()) {
        char c = client.read();
        //Serial.write(c);
        header += c;
        if (c == '\n') {

          if (currentLine.length() == 0) {
            // HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
            // and a content-type so the client knows what's coming, then a blank line:
            client.println("HTTP/1.1 200 OK");
            client.println("Content-type:text/html");
            client.println("Connection: close");
            client.println();

            // turns the GPIOs on and off
            if (header.indexOf("GET /debug/") >= 0) {
              isDebugMode = !isDebugMode;
              String debugState = isDebugMode ? "ON" : "OFF";
              refreshDis("Debug Mode", debugState);
            } else if (header.indexOf("GET /toggleMagnet/") >= 0) {
              useMag = !useMag;
              if (useMag) {
                analogWrite(vibMotor, 0);
              } else {
                analogWrite(en1, 0);
              }
              String magState = magState ? "ON" : "OFF";
              refreshDis("Magnet", magState);
            } else if (header.indexOf("GET /rhythm/") >= 0) {
              //Serial.println("DEBUG CLIENT CONN: " + String(millis() - debugStart));
              String headerGETLine = header.substring(12, header.indexOf("\n") - 9);//Should be in format of: {value}
              Serial.println("In rhythm?: " + headerGETLine);
              commonRhythmFreq = headerGETLine.toInt();
            } else if (header.indexOf("GET /vibVal/") >= 0) {
              String headerGETLine = header.substring(12, header.indexOf("\n") - 9);//Should be in format of: {value}
              Serial.println("In val?: " + headerGETLine);
              if (headerGETLine.toInt() == 0) {
                analogWrite(vibMotor, 0);
                analogWrite(en1, 0);
              }
              commonIntensityTarget = headerGETLine.toInt();
            } else if (header.indexOf("GET /vibFreq/") >= 0) {
              String headerGETLine = header.substring(13, header.indexOf("\n") - 9);//Should be in format of: {value}
              Serial.println("In freq?: " + headerGETLine);
              intervalVib = headerGETLine.toInt();

            } else if (header.indexOf("GET /hard/") >= 0) {
              String headerGETLine = header.substring(9, header.indexOf("\n") - 9); //Should be in format of: {value}/{direction}/{*wait time}
              Serial.println(headerGETLine);
              String valueString = headerGETLine.substring(1); //{value}/{direction}/{wait time}
              String directionString = valueString.substring(valueString.indexOf("/") + 1); //{direction}/{wait time}
              String waitString = directionString.substring(directionString.indexOf("/") + 1); //{wait time}

              int pwmValue = valueString.substring(0, valueString.indexOf("/")).toInt();
              String dir = directionString.substring(0, directionString.indexOf("/"));
              int waitTime = waitString.substring(0, waitString.indexOf("/")).toInt();
              Serial.println("Hard Actuate: " + valueString);
              useMag ? hardActuate(dir, pwmValue, waitTime) : hardActuateMotor(pwmValue, waitTime);
            } else if (header.indexOf("GET /soft/") >= 0) {
              String headerGETLine = header.substring(9, header.indexOf("\n") - 9); //Should be in format of: {value}/{direction}/{wait time}/{raise value}
              Serial.println(headerGETLine);
              String valueString = headerGETLine.substring(1); //{value}/{wait time}/{no. of rounds}/{direction}
              String directionString = valueString.substring(valueString.indexOf("/") + 1); //{wait time}/{no. of rounds}/{direction}
              String waitString = directionString.substring(directionString.indexOf("/") + 1); //no. of rounds}/{direction}
              String raiseValueString = waitString.substring(waitString.indexOf("/") + 1); //{direction}

              int pwmValue = valueString.substring(0, valueString.indexOf("/")).toInt();
              String dir = directionString.substring(0, directionString.indexOf("/"));
              int waitTime = waitString.substring(0, waitString.indexOf("/")).toInt();
              int raiseVal = raiseValueString.substring(0, raiseValueString.indexOf("/")).toInt();
              //String direc, int targetVal, int waitT, int raiseVal
              softActuate(dir, pwmValue, waitTime, raiseVal);
            } else if (header.indexOf("GET /vibrate/") >= 0) {
              String headerGETLine = header.substring(12, header.indexOf("\n") - 9); //Should be in format of: {value}/{wait time}/{no. of rounds}/{direction}
              Serial.println(headerGETLine);
              String valueString = headerGETLine.substring(1); //{value}/{wait time}/{no. of rounds}/{direction}
              String waitString = valueString.substring(valueString.indexOf("/") + 1); //{wait time}/{no. of rounds}/{direction}
              String noRoundsString = waitString.substring(waitString.indexOf("/") + 1); //no. of rounds}/{direction}
              String directionString = noRoundsString.substring(noRoundsString.indexOf("/") + 1); //{direction}
              int pwmValue = valueString.substring(0, valueString.indexOf("/")).toInt();
              int waitTime = waitString.substring(0, waitString.indexOf("/")).toInt();
              int noOfRounds = noRoundsString.substring(0, noRoundsString.indexOf("/")).toInt();
              useMag ? vibrate(pwmValue, waitTime, noOfRounds, directionString) : vibrateMotor(pwmValue, waitTime, noOfRounds);
            } else if (header.indexOf("GET /sine/") >= 0) {
              String headerGETLine = header.substring(9, header.indexOf("\n") - 9); //Should be in format of: {value}/{wait time}/{no. of rounds}/{direction}
              Serial.println(headerGETLine);
              String valueString = headerGETLine.substring(1); //{value}/{wait time}/{no. of rounds}/{direction}
              String waitString = valueString.substring(valueString.indexOf("/") + 1); //{wait time}/{no. of rounds}/{direction}
              String noRoundsString = waitString.substring(waitString.indexOf("/") + 1); //no. of rounds}/{direction}
              String directionString = noRoundsString.substring(noRoundsString.indexOf("/") + 1); //{direction}
              int pwmValue = valueString.substring(0, valueString.indexOf("/")).toInt();
              int waitTime = waitString.substring(0, waitString.indexOf("/")).toInt();
              int noOfRounds = noRoundsString.substring(0, noRoundsString.indexOf("/")).toInt();
              sineVibrate(pwmValue, waitTime, noOfRounds, directionString);
            }

            // Display the HTML web page
            client.println("Success");

            // The HTTP response ends with another blank line
            client.println();
            // Break out of the while loop
            break;
          } else { // if you got a newline, then clear currentLine
            currentLine = "";
          }
        } else if (c != '\r') {  // if you got anything else but a carriage return character,
          currentLine += c;      // add it to the end of the currentLine
        }
      }

    }

    // Clear the header variable
    header = "";
    // Close the connection

    //client.stop();
    //Serial.println("DEBUG WHOLE: " + String(millis() - debugStart));
    //Serial.println("Client disconnected." + String(millis()));
    Serial.println("");

  }


  //Do vibration and such and soforth
  //intervalVib
  //commonIntensity
  //commonIntensityTarget
  //previousMillisVib
  //rhythmState
  if (commonIntensityTarget > 0) {  //Should it even actuate?

    if (millis() - previousMillis >= commonRhythmFreq) {
      previousMillis = millis();

      if (rhythmState) {
        if (useMag) {
          analogWrite(en1, 0);
          digitalWrite(IN1, LOW);
          digitalWrite(IN2, LOW);
        } else {
          analogWrite(vibMotor, 0);
        }
      } else {

        if (!useMag) analogWrite(vibMotor, commonIntensityTarget / 2); //Divided for motor
      }
      rhythmState = !rhythmState;
    }

    if (millis() - previousMillisVib >= intervalVib && rhythmState && useMag) {
      previousMillisVib = millis();
      //Add if for vib motor ma guy
      analogWrite(en1, commonIntensityTarget);
      vibState ? digitalWrite(IN1, HIGH) : digitalWrite(IN1, LOW);
      vibState ? digitalWrite(IN2, LOW) : digitalWrite(IN2, HIGH);
      vibState = !vibState;
    }

  }

}

//For later...
/*if (vibState) {
        digitalWrite(IN1, HIGH);
        digitalWrite(IN2, LOW);
        //commonIntensity++;
        //if(commonIntensity >= commonIntensityTarget) vibState = false;
      } else {
        digitalWrite(IN1, LOW);
        digitalWrite(IN2, HIGH);
        //commonIntensity--;
        //if(commonIntensity <= 0) vibState = true;
      }*/

void vibrateMotor(int val, int waitT, int noOfR) {
  refreshDis("Vibrate", " w.:" + String(val));
  analogWrite(vibMotor, val / 2); //Divided for motor
  delay(waitT * noOfR);
  digitalWrite(vibMotor, LOW);
  refreshDis("Vibrate", "Stop");
}

void vibrate(int val, int waitT, int noOfR, String direc) {
  analogWrite(en1, val);
  refreshDis("Vibrate", direc + " w.:" + String(val));
  //Serial.println(val);
  if (direc.indexOf("repel") > -1) {
    for (int i = 0; i < noOfR; i++) {
      digitalWrite(IN1, HIGH);
      digitalWrite(IN2, LOW);
      delay(waitT / 2);
      digitalWrite(IN1, LOW);
      digitalWrite(IN2, LOW);
      delay(waitT / 2);
    }
  } else if (direc.indexOf("attract") > -1) {
    for (int i = 0; i < noOfR; i++) {
      digitalWrite(IN1, LOW);
      digitalWrite(IN2, HIGH);
      delay(waitT / 2);
      digitalWrite(IN1, LOW);
      digitalWrite(IN2, LOW);
      delay(waitT / 2);
    }
  } else if (direc.indexOf("both") > -1) {
    for (int i = 0; i < noOfR; i++) {
      digitalWrite(IN1, HIGH);
      digitalWrite(IN2, LOW);
      delay(waitT / 2);
      digitalWrite(IN1, LOW);
      digitalWrite(IN2, HIGH);
      delay(waitT / 2);
    }
  }
  refreshDis("Vibrate", "Stop");
  analogWrite(en1, 0);
}

void sineVibrate(int val, int waitT, int noOfR, String direc) {
  bool isAttract;

  if (direc.indexOf("repel") > -1) {
    isAttract = false;
  } else if (direc.indexOf("attract") > -1) {
    isAttract = true;
  } else {
    return;
  }

  if (isAttract) {
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);
  } else {
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);
  }
  //analogWrite(en1, val);
  for (int i = 0; i < noOfR; i++) {
    for (int j = 0; j < 32; j++) {
      analogWrite(en1, map(mysine[j], 0, 4096, 0, val));
      //Serial.println(map(mysine[j], 0, 4096, 0, val));
      delay(10);
    }
    delay(waitT);
  }
}

void hardActuateMotor(int targetVal, int waitTime) {
  if (waitTime > 0) {
    analogWrite(vibMotor, targetVal / 2); //Divided for motor
    isDebugMode ? refreshDis("Hard Actuate", "") : refreshDis("Hard Actuate", " w.:" + String(targetVal));
    delay(waitTime);
    digitalWrite(vibMotor, LOW);
    refreshDis("Hard Actuate", "Stopped...");
  } else {
    analogWrite(vibMotor, targetVal / 2); //Divided for motor
    isDebugMode ? refreshDis("Hard Actuate", "") : refreshDis("Hard Actuate", " w.:" + String(targetVal));
  }
}

void hardActuate(String direc, int targetVal, int waitTime) {
  int direct = direc.indexOf("CCW") > -1;
  if (waitTime > 0) {
    String directionString;
    if (direct) {
      directionString = "Attract";
      digitalWrite(IN1, HIGH);
      digitalWrite(IN2, LOW);
    } else {
      directionString = "Repel";
      digitalWrite(IN1, LOW);
      digitalWrite(IN2, HIGH);
    }
    analogWrite(en1, targetVal);
    isDebugMode ? refreshDis("Hard Actuate", directionString) : refreshDis("Hard Actuate", directionString + " w.:" + String(targetVal));
    delay(waitTime);
    analogWrite(en1, 0);
    refreshDis("Hard Actuate", "Stopped...");
  } else {
    String directionString;
    if (direct) {
      directionString = "Attract";
      digitalWrite(IN1, HIGH);
      digitalWrite(IN2, LOW);
    } else {
      directionString = "Repel";
      digitalWrite(IN1, LOW);
      digitalWrite(IN2, HIGH);
    }
    analogWrite(en1, targetVal);
    isDebugMode ? refreshDis("Hard Actuate", directionString) : refreshDis("Hard Actuate", directionString + " w.:" + String(targetVal));
  }

}

void softActuate(String direc, int targetVal, int waitT, int raiseVal) {
  //Serial.println("Soft Actuate!");
  //Serial.println(direc);
  //Serial.println(direc.indexOf("CCW"));
  int direct = direc.indexOf("CCW") > -1;
  String directionString;
  if (direct) {
    directionString = "Attract";
    digitalWrite(IN1, HIGH);
    digitalWrite(IN2, LOW);
  } else {
    directionString = "Repel";
    digitalWrite(IN1, LOW);
    digitalWrite(IN2, HIGH);
  }
  for (int i = 0; i < targetVal; i += raiseVal) {
    delay(waitT);
    analogWrite(en1, i);
  }

  refreshDis("Soft Actuate", directionString + " w.:" + String(targetVal));
}

void refreshDis(String magMsg1, String magMsg2) {
  display.clearDisplay();
  display.drawCircle(15, 37, 10, WHITE);
  display.drawCircle(15, 37, 3, WHITE);
  display.setCursor(0, 5);
  display.println("IP: " + WiFi.localIP().toString());
  display.setCursor(0, 16);
  //useMag
  //isDebugMode
  String debugString = isDebugMode ? "Debug: ON" : "Debug: OFF";
  String useMagString = useMag ? "Magnetic" : "Vibro";
  display.println(debugString + " | " + useMagString);
  display.setCursor(32, 28);
  display.println(magMsg1);
  display.setCursor(32, 38);
  display.println(magMsg2);
  display.display();
}
