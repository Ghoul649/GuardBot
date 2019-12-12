#define A1 12
#define A2 11
#define B1 10
#define B2 9
#define TR 8
#define TL 7
#define R1 6
#define R2 5
byte maxSpeed = 200;
byte maxASpeed = 50;
int Speed  = 0;
int ASpeed = 0;
int RSpeed = 0;
int LSpeed = 0;

void changeSpeed(){
  RSpeed = Speed + ASpeed;
  LSpeed = SPeed - ASpeed;
  if (RSpeed < -255){
    RSpeed = 255;
  }else if (RSpeed > 255){
      RSpeed = 255;
  }
  if (LSpeed < -255){
    LSpeed = 255;
  }else if (LSpeed > 255){
      LSpeed = -255;
  }

  if (RSpeed >= 0){
      digitalWrite(A1,HIGH);
      digitalWrite(A2,LOW);
      analogWrite(R2,RSpeed);
    }else{
      digitalWrite(A1,LOW);
      digitalWrite(A2,HIGH);
      analogWrite(R2,-RSpeed);
    }
    if (RSpeed >= 0){
      digitalWrite(B1,HIGH);
      digitalWrite(B2,LOW);
      analogWrite(R1,RSpeed);
    }else{
      digitalWrite(B1,LOW);
      digitalWrite(B2,HIGH);
      analogWrite(R1,-RSpeed);
    }
}
byte add(int val,int v,int m){
  if (val + v > m){
    return m;
  }else if (val + v < -m){
    return -m;
  }else{
    return val + v;
  }
}
void setup() {
  pinMode(A1,OUTPUT);
  pinMode(A2,OUTPUT);
  pinMode(B1,OUTPUT);
  pinMode(B2,OUTPUT);
  pinMode(R1,OUTPUT);
  pinMode(R2,OUTPUT);
  pinMode(TR,INPUT);
  pinMode(TL,INPUT);
  Serial.begin(9600);
  analogWrite(R1,150);
  analogWrite(R2,150);
}
  
void LineTracing(){
    
}

void Right(int v){
  changeSpeed();
    
}

void Left(int v){
  
    ASpeed = -10;
    changeSpeed();
    
}

void Forward(int v){
  if(v < 200){
    ASpeed = 0;
    changeSpeed();
  }else{
    Stop();
    delay(1000);
  }
}

void Stop(){
  digitalWrite(A1,LOW);
  digitalWrite(A2,LOW);
  digitalWrite(B1,LOW);
  digitalWrite(B2,LOW);
}
bool l = false;
bool r = false;
int a = 0;
int b = 0;
int c = 0;
int d = 0;
void loop() {
  delay(10);
  l = !digitalRead(TL);
  r = !digitalRead(TR);
  
    
  if (l && r){
    Forward(d);
    a = 0;
    b = 0;
    c = 0;
    d += 1;
  }else if (!(l || r)){
    a += 1;
    b = 0;
    c = 0;
    d = 0;
    Forward(a);
  }else if (l){
    a = 0;
    b += 1;
    c = 0;
    d = 0;
    Left(b);
  }else if (r){
    a = 0;
    b = 0;
    c += 1;
    d = 0;
    Right(c);
  }
    
}
