#define MA1 11
#define MA2 10
#define MA3 9
#define MA4 8
#define MB1 7
#define MB2 6
#define MB3 5
#define MB4 4

int DelA = 500;
byte StageA = 0;
boolean DirA = true;
boolean Stopped = true;
void NextA(){
	if (Stopped){
		digitalWrite(MA1,LOW);
		digitalWrite(MA2,LOW);
		digitalWrite(MA3,LOW);
		digitalWrite(MA4,LOW);
	}
	if(StageA == 0){
		digitalWrite(MA1,LOW);
		digitalWrite(MA2,HIGH);
		digitalWrite(MA3,LOW);
		digitalWrite(MA4,LOW);
	}else if (StageA == 1){
		digitalWrite(MA1,LOW);
		digitalWrite(MA2,LOW);
		digitalWrite(MA3,LOW);
		digitalWrite(MA4,HIGH);
	}else if (StageA == 2){
		digitalWrite(MA1,HIGH);
		digitalWrite(MA2,LOW);
		digitalWrite(MA3,LOW);
		digitalWrite(MA4,LOW);
	}else if (StageA == 3){
		digitalWrite(MA1,LOW);
		digitalWrite(MA2,LOW);
		digitalWrite(MA3,HIGH);
		digitalWrite(MA4,LOW);
	}
	
	if (DirA){
		StageA = (StageA + 1)%4;
	}else{
		if (StageA == 0){
			StageA = 3;
		}else{
			StageA -= 1;
		}
	}
}

void setup() {
  pinMode(MA1,OUTPUT);
  pinMode(MA2,OUTPUT);
  pinMode(MA3,OUTPUT);
  pinMode(MA4,OUTPUT);
  pinMode(MB1,OUTPUT);
  pinMode(MB2,OUTPUT);
  pinMode(MB3,OUTPUT);
  pinMode(MB4,OUTPUT);
  digitalWrite(MA1,LOW);
  digitalWrite(MA2,LOW);
  digitalWrite(MA3,LOW);
  digitalWrite(MA4,LOW);
  digitalWrite(MB1,LOW);
  digitalWrite(MB2,LOW);
  digitalWrite(MB3,LOW);
  digitalWrite(MB4,LOW);
  Serial.begin(9600);
}
byte temp = 0;
void loop() {
	if (Serial.available() > 1){
		temp = Serial.read();
		if (temp == 1){
			temp = Serial.read();
			if (temp == 0){
				Stopped = true;
				DelA = 500;
			}else{
				DelA = temp;
			}
		}else if (temp == 2){
			temp = Serial.read();
		}else if (temp == 3){
			temp = Serial.read();
			if (temp == 1){
				DirA = true;
			}else{
				DirA = false;
			}
		}else if (temp == 4){
			temp = Serial.read();
		}
	}
	
	NextA();
	delay(DelA);
}
