#define joyX1 A0
#define joyX2 A1

float x1Read = 0;
float x2Read = 0;
float x1Value = 0;
float x2Value = 0;
void setup() {
  Serial.begin(9600);
}
 
void loop() { 
  // put your main code here, to run repeatedly:
  x1Read = analogRead(joyX1);
  x2Read = analogRead(joyX2);
  float bias1 = 507;
  float bias2 = 526;
  x1Value = (x1Read) - bias1;
  x1Value = x1Value >=0 ? x1Value / (1023-bias1) : x1Value / bias1;
  x2Value = (x2Read) - bias2;
  x2Value = x2Value >=0 ? x2Value / (1023-bias2) : x2Value / bias2;

  Serial.print(x1Value);
  Serial.print("\t");
  Serial.println(x2Value);

}
