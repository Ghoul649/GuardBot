#include <Wire.h>

#define MLX90614_ADDR 0x5A
#define MLX90614_TAMB 0x06
#define MLX90614_TOBJ1 0x07

#define SI7021_ADDR 0x40
#define SI7021_TAMB 0xF3
#define SI7021_H 0xF5

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







void setup(){
	
}

void loop(){
	
}