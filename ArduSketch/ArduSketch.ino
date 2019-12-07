#include <Wire.h>
#include <Servo.h>

#define MLX90614_ADDR 0x5A
#define MLX90614_TAMB 0x06
#define MLX90614_TOBJ1 0x07

#define SI7021_ADDR 0x40
#define SI7021_TAMB 0xF3
#define SI7021_H 0xF5

//Піни
#define SERVOTX 8	//Горизонтальна серва
#define SERVOTY 7	//Вертикальна серва
#define ENA 6 		//шим мотору1
#define ENB 5		//шим мотору2
#define EA1 12		//мотор1
#define EA2 11		//мотор1
#define EB1 10		//мотор2
#define EB2 9		//мотор2

Servo ServoTX;
Servo ServoTY;
int temp = 0;
float MLX90614_Read(byte reg){ 					//Повертає температурні по цельсію з датчика MLX90614. Вхідні данні - адреса регістру (MLX90614_TAMB - температура оточення; MLX90614_TOBJ1 - Температура об'єкта).
	Wire.beginTransmission(MLX90614_ADDR);
	Wire.write(reg);
	Wire.endTransmission(false);
	Wire.requestFrom(MLX90614_ADDR,3,false);
	while (Wire.available() < 3);
	float result = Wire.read() + Wire.read()*256;
	result = (result*0.02 - 0.01) - 273.15;
	byte pec = Wire.read();
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
	while(Serial.available() < 8)					// Очікування вхідних данних.
	{
		delay(10);
	}
	byte xfrom = Serial.read();					//Початковий кут по Х.
	byte xto = Serial.read();						//Кінцевий кут по Х.
	byte yfrom = Serial.read();					//Початковий кут по .
	byte yto = Serial.read();						//Кінцевий кут по .
	byte k = Serial.read();						//Крок.
	byte del = Serial.read();						//Затримка перед зчитуванням.
	byte adddel = Serial.read();					//Додаткова затримка при зміні напрямку руху.
	byte subdel = Serial.read();					//Прискорення при змыны напрямку руху.
  
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
		Serial.write(Wire.read());
		Serial.write(temp);
		Wire.read();
		Wire.endTransmission();
    }
	if (Serial.available() > 0){
		if (Serial.read() == 200){
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
		Serial.write(Wire.read());
		Serial.write(temp);
		Wire.read();
		Wire.endTransmission();
    }
	if (Serial.available() > 0){
		if (Serial.read() == 200){
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
		}
	}
}

long temptime = 0;
int safeReadByte(int timeout){
	temptime = millis();
	while(Serial.available() < 1 && millis() - temptime < timeout);
	if (Serial.available() > 0){
		return Serial.read();
	}
	return -1;
}

void setup(){
	pinMode(ENA,OUTPUT);
	pinMode(ENB,OUTPUT);
	pinMode(EA1,OUTPUT);
	pinMode(EA2,OUTPUT);
	pinMode(EB1,OUTPUT);
	pinMode(EB2,OUTPUT);
	Serial.begin(9600);
	Wire.begin();
	ServoTX.attach(SERVOTX);
	ServoTY.attach(SERVOTY);
}

void loop(){
	Serial.write((byte)100);
	while(Serial.available() < 1){
		delay(500);
	}
	byte mode = Serial.read();
	
	if (mode == 1){
		Serial.write((byte)101);
		IRScan();
	}
	if (mode == 2){
		Serial.write((byte)102);
		MControl();
	}
}