#include <LiquidCrystal.h>
const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);

#define joyX1 A0
#define joyX2 A1
float x1Read = 0;
float x2Read = 0;
float x1Value = 0;
float x2Value = 0;
String time = "";

void setup()
{
  lcd.begin(16, 2);
  lcd.print("Timer:");
  Serial.setTimeout(20);
  Serial.begin(9600);
}

void loop()
{
  //joysticks
  // put your main code here, to run repeatedly:
  x1Read = analogRead(joyX1);
  x2Read = analogRead(joyX2);
  float bias1 = 507;
  float bias2 = 526;
  x1Value = (x1Read)-bias1;
  x1Value = x1Value >= 0 ? x1Value / (1023 - bias1) : x1Value / bias1;
  x2Value = (x2Read)-bias2;
  x2Value = x2Value >= 0 ? x2Value / (1023 - bias2) : x2Value / bias2;

  Serial.print(x1Value);
  Serial.print("\t");
  Serial.println(x2Value);

  //lcd
  String time = Serial.readString();
  lcd.setCursor(7, 0);
  lcd.print(time);
}
