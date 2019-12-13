#include <Wire.h>
#include <Servo.h>

#define MLX90614_ADDR 0x5A
#define MLX90614_TAMB 0x06
#define MLX90614_TOBJ1 0x07

#define SI7021_ADDR 0x40
#define SI7021_TAMB 0xF3
#define SI7021_H 0xF5

//Піни
#define RARM 12 	//Праве плече
#define RELBOW 3   //Правий лікоть
#define RHAND 	8	//Права клешня
#define LARM 11		//Ліве плече
#define LELBOW 10	//Лівий лікоть
#define LHAND 9		//Ліва клешня
#define SERVOTX 7	//Горизонтальна серва
#define SERVOTY 6	//Вертикальна серва
#define ENA 4		//шим мотору1
#define ENB 5		//шим мотору2
#define EA1 24		//мотор1S
#define EA2 25		//мотор1O
#define EB1 26		//мотор2G
#define EB2 27		//мотор2Z
#define FIRE 33
#define LIGHT A15
#define ALCHO A12
#define DUSTD 35
#define DUSTA A11


Servo RArm;
Servo RElbow;
Servo RHand;
Servo LArm;
Servo LElbow;
Servo LHand;
Servo ServoTX;
Servo ServoTY;
int temp = 0;
float tempf = 0;
float MLX90614_Read(byte reg){ 					//Повертає температурні по цельсію з датчика MLX90614. Вхідні данні - адреса регістру (MLX90614_TAMB - температура оточення; MLX90614_TOBJ1 - Температура об'єкта).
	Wire.beginTransmission(MLX90614_ADDR);
	Wire.write(reg);
	Wire.endTransmission(false);
	Wire.requestFrom(MLX90614_ADDR,3,false);
	temp = safeReadWire(100);
	if (temp == -1){
		return 0;
	}
	float result = temp;
	temp = safeReadWire(100);
	if (temp == -1){
		return 0;
	}
	result += temp*256;
	result = (result*0.02 - 0.01) - 273.15;
	byte pec = safeReadWire(100);
	Wire.endTransmission();
	return result;
}

float SI7021_ReadT(){							//Повертає температуру по цельсію з датчика SI7021.
	return SI7021_Read(SI7021_TAMB)*175.72/65536.0 - 46.85;
}

float SI7021_ReadH(){							//Повертає вологість у % з датчика SI7021.
	return SI7021_Read(SI7021_H)*125/65536.0 - 6.0;
}

int SI7021_Read(byte reg){						//Повертає данні з вказаного регістру датчика SI7021. 
	Wire.beginTransmission(SI7021_ADDR);
	Wire.write(reg);
	Wire.endTransmission(false);
	delay(30);
	Wire.requestFrom(SI7021_ADDR,3,false);
	int result = Wire.read()*256 + Wire.read();
	byte pec = Wire.read();
	Wire.endTransmission();
	return result;
}
void FireCheck(){
  if (digitalRead(FIRE)){
    Serial.write(250);
    Serial.write(250);
  }
}
void IRScan(){									//Функція сканування області інфрачервоним датчиком. 
	while(Serial2.available() < 8)					// Очікування вхідних данних.
	{
		delay(10);
	}
	byte xfrom = Serial2.read();					//Початковий кут по Х.
	byte xto = Serial2.read();						//Кінцевий кут по Х.
	byte yfrom = Serial2.read();					//Початковий кут по .
	byte yto = Serial2.read();						//Кінцевий кут по .
	byte k = Serial2.read();						//Крок.
	byte del = Serial2.read();						//Затримка перед зчитуванням.
	byte adddel = Serial2.read();					//Додаткова затримка при зміні напрямку руху.
	byte subdel = Serial2.read();					//Прискорення при змыны напрямку руху.
  
	int temp = 0;
	int tempdel = 0;
	ServoTY.write(yfrom);
	ServoTX.write(xfrom);
	delay(500);
	for(int y = yfrom; y < yto; y+=k){
		ServoTY.write(y);
		tempdel = adddel;
		for(int x = xfrom; x < xto; x += k){
		ServoTX.write(x);
		delay(del + tempdel);
		if (tempdel > 0){
			tempdel -= subdel;
		}else{
			tempdel = 0;
		}
		Wire.beginTransmission(MLX90614_ADDR);
		Wire.write(MLX90614_TOBJ1);
		Wire.endTransmission(false);
		Wire.requestFrom(MLX90614_ADDR,3,false);
		while (Wire.available() < 3);
		temp = Wire.read();
		Serial2.write(Wire.read());
		Serial2.write(temp);
		Wire.read();
		Wire.endTransmission();
    }
	if (Serial2.available() > 0){
		if (Serial2.read() == 200){
			Wire.endTransmission();
			ServoTX.write(90);
			ServoTY.write(90);
			return;
		} 
	}
    y += k;
    ServoTY.write(y);
    tempdel = adddel;
    for(int x = xto - k; x >= xfrom; x -= k){
		ServoTX.write(x);
		delay(del + tempdel);
		if (tempdel > 0){
			tempdel -= subdel;
		}else{
			tempdel = 0;
		}
		Wire.beginTransmission(MLX90614_ADDR);
		Wire.write(MLX90614_TOBJ1);
		Wire.endTransmission(false);
		Wire.requestFrom(MLX90614_ADDR,3,false);
		while (Wire.available() < 3);
		temp = Wire.read();
		Serial2.write(Wire.read());
		Serial2.write(temp);
		Wire.read();
		Wire.endTransmission();
    }
	if (Serial2.available() > 0){
		if (Serial2.read() == 200){
			ServoTX.write(90);
			ServoTY.write(90);
			return;
		} 
	}
  }
  Wire.endTransmission();
}

void MControl(){
	while(true){
		temp = safeReadByte(1000);
		if(temp == -1){
		}else if (temp == 0){
			break;
		}else if (temp == 11){
			temp = safeReadByte(100);
			if (temp != -1){
				ServoTX.write(temp);
			}
		}else if(temp == 12){
			temp = safeReadByte(100);
			if (temp != -1){
				ServoTY.write(temp);
			}
		}else if(temp == 13){
			temp = safeReadByte(100);
			if (temp != -1){
				analogWrite(ENA, temp);
			}
		}else if(temp == 14){
			temp = safeReadByte(100);
			if (temp != -1){
				analogWrite(ENB, temp);
			}
		}else if(temp == 15){
			temp = safeReadByte(100);
			if (temp == 1){
				digitalWrite(EA1,HIGH);
				digitalWrite(EA2,LOW);
			}else if(temp == 2){
				digitalWrite(EA1,LOW);
				digitalWrite(EA2,HIGH);
			}else{
				digitalWrite(EA1,LOW);
				digitalWrite(EA2,LOW);
			}
		}else if(temp == 16){
			temp = safeReadByte(100);
			if (temp == 1){
				digitalWrite(EB1,HIGH);
				digitalWrite(EB2,LOW);
			}else if(temp == 2){
				digitalWrite(EB1,LOW);
				digitalWrite(EB2,HIGH);
			}else{
				digitalWrite(EB1,LOW);
				digitalWrite(EB2,LOW);
			}
		}else if(temp == 17){
			temp = safeReadByte(100);
			if (temp != -1){
				RArm.write(temp);
			}
		}else if(temp == 18){
			temp = safeReadByte(100);
			if (temp != -1){
				RElbow.write(temp);
			}
		}else if(temp == 19){
			temp = safeReadByte(100);
			if (temp != -1){
				RHand.write(temp);
			}
		}else if(temp == 20){
			temp = safeReadByte(100);
			if (temp != -1){
				LArm.write(temp);
			}
		}else if(temp == 21){
			temp = safeReadByte(100);
			if (temp != -1){
				LElbow.write(temp);
			}
		}else if(temp == 22){
			temp = safeReadByte(100);
			if (temp != -1){
				LHand.write(temp);
			}
		}else if(temp == 101){
			temp = SI7021_Read(SI7021_TAMB);
			Serial2.write((byte)(temp/256));
			Serial2.write((byte)(temp%256));
		}else if(temp == 102){
			temp = SI7021_Read(SI7021_H);
			Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
		}else if (temp == 103){
		  temp = analogRead(ALCHO);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
		}else if (temp == 104){
     temp = analogRead(LIGHT);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
    }else if (temp == 105){
      digitalWrite(DUSTD,LOW);
      delayMicroseconds(280);
      temp = analogRead(DUSTA);
      delayMicroseconds(40);
      digitalWrite(DUSTD,HIGH); 
      delayMicroseconds(9680);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
    }else if (temp == 106){
      temp = digitalRead(FIRE);
      Serial2.write((byte)(temp));
    }else if (temp == 107){
      temp = SI7021_Read(SI7021_TAMB);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
      delay(10);
      temp = SI7021_Read(SI7021_H);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
      delay(10);
      temp = analogRead(ALCHO);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
      delay(10);
      temp = analogRead(LIGHT);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
      delay(10);
      digitalWrite(DUSTD,LOW);
      delayMicroseconds(280);
      temp = analogRead(DUSTA);
      delayMicroseconds(40);
      digitalWrite(DUSTD,HIGH); 
      delayMicroseconds(9680);
      Serial2.write((byte)(temp/256));
      Serial2.write((byte)(temp%256));
      delay(10);
      temp = digitalRead(FIRE);
      Serial2.write(0);
      Serial2.write((byte)temp);
    }
	}
}

long temptime = 0;
int safeReadWire(int timeout){
	temptime = millis();
	while(Wire.available() < 1 && millis() - temptime < timeout);
	if (Wire.available() > 0){
		return Wire.read();
	}
	return 0;
}
int safeReadByte(int timeout){
	temptime = millis();
	while(Serial2.available() < 1 && millis() - temptime < timeout);
	if (Serial2.available() > 0){
		return Serial2.read();
	}
	return -1;
}



void setup(){
pinMode(ENA,OUTPUT);
	pinMode(ENB, OUTPUT);
	pinMode(EA1, OUTPUT);
	pinMode(EA2, OUTPUT);
	pinMode(EB1, OUTPUT);
	pinMode(EB2, OUTPUT);
  pinMode(FIRE, INPUT);
  pinMode(DUSTD, OUTPUT);
  pinMode(DUSTA, INPUT);
  pinMode(ALCHO,INPUT);
  pinMode(LIGHT,INPUT);
  
	Serial2.begin(9600);
	Serial1.begin(9600);
	Wire.begin();
	ServoTX.attach(SERVOTX);
	ServoTY.attach(SERVOTY);
  ServoTX.write(60);
  ServoTY.write(110);
	RArm.attach(RARM);
	LArm.attach(LARM);
	RElbow.attach(RELBOW);
	LElbow.attach(LELBOW);
	RHand.attach(RHAND);
	LHand.attach(LHAND);
	
}

void loop(){
	Serial2.write((byte)100);
	while(Serial2.available() < 1){
		delay(500);
	}
	byte mode = Serial2.read();
	if (mode == 1){
		Serial2.write((byte)101);
		IRScan();
	}else if (mode == 2){
		Serial2.write((byte)102);
		MControl();
	}
}
