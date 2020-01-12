#include <Wire.h>



int val;
int LED = 13;
String input_string = "";
int serv = 0;
int analogPin = 0;
byte state = false;

void setup()
{
  Serial.begin(9600);
  pinMode(LED, OUTPUT);
  digitalWrite(LED, HIGH);
  pinMode(7, OUTPUT);
  digitalWrite(7, LOW);
  myservo.attach(10);
}

void loop()
{
  while (Serial.available() > 0) {
    char c = Serial.read();
    if (c == '\n') { 
        Command();
        input_string = "";
    } else {
        input_string += c;
    }
  }
}

void Command()
{
    //Serial.print("Input_string is: ");
    //Serial.println(input_string);
    
    if (input_string.equals("1") == true)   // При символе "1" включаем светодиод
    {
      digitalWrite(LED, HIGH);
      Serial.println("LED is ON");
    }
    
    else if (input_string.equals("0") == true)   // При символе "0" выключаем светодиод
    {
      digitalWrite(LED, LOW);
      Serial.println("LED is OFF");
    }

    else if (input_string.equals("t") == true)
    {
      switch(th.Read()) {
        case 2:
          Serial.println("CRC failed");
          break;
        case 1:
          Serial.println("Sensor offline");
          break;
        case 0:
          Serial.print("Temperature: ");
          Serial.print(th.t);
          Serial.println("*C");
          break;
      }
    }
    else if (input_string.equals("h") == true)
    {
      switch(th.Read()) {
        case 2:
          Serial.println("CRC failed");
          break;
        case 1:
          Serial.println("Sensor offline");
          break;
        case 0:
          Serial.print("Humidity: ");
          Serial.print(th.h);
          Serial.println("%");
          break;
      }
    }

    else if (input_string.startsWith("s") == true)
    {
        input_string.replace("s", "");    // заменяем строку
        serv = input_string.toInt();      // преобразовываем строку в число
        if (serv >170) serv = 170;
        if (serv <10) serv = 10;
        myservo.write(serv);
        Serial.print("Servo set to ");
        Serial.println(serv);
        delay(15);
    }

    else if (input_string.startsWith("a") == true)
    {
        input_string.replace("a", "");    // заменяем строку
        analogPin = input_string.toInt();      // преобразовываем строку в число
        if (analogPin >7) analogPin = 7;
        if (analogPin <0) analogPin = 0;
        serv = analogRead(analogPin);     // считываем значение
        Serial.print("AnalogRead PIN A");
        Serial.print(analogPin);
        Serial.print("= ");
        Serial.println(serv);
        delay(15);
    }

    else if (input_string.startsWith("d7") == true)
    {
        state = !state; 
        digitalWrite(7, state);
        Serial.print("State of 7: ");
        Serial.println(state);
    }
}
