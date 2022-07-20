int SensorPin = A0;

void setup() {
  Serial.begin(9600);
}

void loop() {
    if (Serial.available() > 0) {
    int opcion = Serial.read();

    if (opcion == 'a' && opcion != '-1') {
      int humedad = analogRead(SensorPin);
      Serial.println(humedad);
      Serial.flush();
    }
  }
}
