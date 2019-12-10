#include <Wire.h>
#include <Servo.h>

#define MLX90614_ADDR 0x5A
#define MLX90614_TAMB 0x06
#define MLX90614_TOBJ1 0x07

#define SI7021_ADDR 0x40
#define SI7021_TAMB 0xF3
#define SI7021_H 0xF5

//Піни
#define RARM 50 	//Праве плече
#define RELBOW 48   //Правий лікоть
#define RHAND 46	//Права клешня
#define LARM 51		//Ліве плече
#define LELBOW 49	//Лівий лікоть
#define LHAND 47	//Ліва клешня
#define SERVOTX 8	//Горизонтальна серва
#define SERVOTY 7	//Вертикальна серва
#define RLINE 22
#define LLINE 23

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
				Serial1.write(1);
				Serial1.write(temp);
			}
		}else if(temp == 14){
			temp = safeReadByte(100);
			if (temp != -1){
				Serial1.write(2);
				Serial1.write(temp);
			}
		}else if(temp == 15){
			temp = safeReadByte(100);
			if (temp == 1){
				Serial1.write(3);
				Serial1.write(1);
			}else if(temp == 2){
				Serial1.write(3);
				Serial1.write(2);
			}else{
				Serial1.write(3);
				Serial1.write(1);
			}
		}else if(temp == 16){
			temp = safeReadByte(100);
			if (temp == 1){
				Serial1.write(4);
				Serial1.write(1);
			}else if(temp == 2){
				Serial1.write(4);
				Serial1.write(2);
			}else{
				Serial1.write(4);
				Serial1.write(1);
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
			tempf = SI7021_ReadT();
			Serial2.write((byte)(80 + tempf));
			Serial2.write((byte)((int)(tempf*100)%100));
		}else if(temp == 102){
			tempf = SI7021_ReadH();
			Serial2.write((byte)(80 + tempf));
			Serial2.write((byte)((int)(tempf*100)%100));
		}else if(temp == 103){
			tempf = MLX90614_Read(MLX90614_TAMB);
			Serial2.write((byte)(80 + tempf));
			Serial2.write((byte)((int)(tempf*100)%100));
		}else if(temp == 104){
			tempf = MLX90614_Read(MLX90614_TOBJ1);
			Serial2.write((byte)(80 + tempf));
			Serial2.write((byte)((int)(tempf*100)%100));
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

void LineFollowing(){
	Serial1.write(3);
	Serial1.write(1);
	Serial1.write(4);
	Serial1.write(1);
	Serial1.write(1);
	Serial1.write(20);
	Serial1.write(2);
	Serial1.write(20);
	while(true){
		if (Serial2.available() > 0){
			if (Serial2.read() == 0){
				Serial1.write(1);
				Serial1.write(0);
				Serial1.write(2);
				Serial1.write(0);
				break;
			}
		}
		if(!analogRead(RLINE)){
			
		}
	}
}

void setup(){
	pinMode(RLINE,INPUT);
	pinMode(LLINE,INPUT);
	Serial2.begin(9600);
	Serial1.begin(9600);
	Wire.begin();
	ServoTX.attach(SERVOTX);
	ServoTY.attach(SERVOTY);
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
	}else if (mode == 3){
		
	}
}