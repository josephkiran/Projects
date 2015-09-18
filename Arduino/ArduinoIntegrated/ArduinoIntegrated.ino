/*
 PIN 12 - INPUT - PIR
 PIN 9 - PWM OUTPUT - BUZZ
 PIN 13 - INTERNAL LIGHT

 */

///////////////PINS
int OUT_BUZZ = 9;
int IN_PIR = 12;
int OUT_LED = 13;
//////////////


void setup() {
  //start serial connection
  Serial.begin(9600);
  //configure pin2 as an input and enable the internal pull-up resistor
  pinMode(IN_PIR, INPUT_PULLUP);
  pinMode(OUT_LED, OUTPUT);
  pinMode(OUT_BUZZ, OUTPUT);
}

void loop() {
  //read the pushbutton value into a variable
  int sensorVal = digitalRead(IN_PIR);
  //print out the value of the pushbutton
  Serial.println(sensorVal);
 
  if (sensorVal == LOW) {
    digitalWrite(OUT_LED, LOW);
  }
  else {
    digitalWrite(OUT_LED, HIGH);
    beep(100);
  }
}

void beep(unsigned char delayms){
  analogWrite(OUT_BUZZ, 100);      // Almost any value can be used except 0 and 255
                           // experiment to get the best tone
  delay(delayms);          // wait for a delayms ms
  analogWrite(OUT_BUZZ, 0);       // 0 turns it off
  delay(delayms);          // wait for a delayms ms   
}  




