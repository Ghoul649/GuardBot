void setup() {
  Serial.begin(115200);

}

void loop() {
  for(int i = 0 ; i< 255; i++){
  Serial.println(i);
  }
  delay(200);
}
