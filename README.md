# GuardBot

## Режими роботи

### 1. Теплове сканування
Для запуску режиму необхідно відправити байт з значення 1, коли робот знаходиться в режимі очікування. При розпізнні команди робот надсилає у відповідь байт з значенням 101, що означає початок роботи режиму. Далі робот очікує на прихід 8 аргументів.
Аргументи:
1. Початковий кут по Х
2. Кінцевий кут по Х
3. Початковий кут по Y
4. Кінцевий кут по Y
5. Крок
6. Затримка перед скануванням
7. Додаткова затримка при зміні напрямку руху
8. Прискорення при зміні напрямку руху

Після отримання аргументів сканер починає свою роботу.
Підчас сканування робот відправляє по два байти інформації за кожен піксель, які містять інформацію про температуру об'єкта. Для перевадення цієї інформації в температуру по цельсію скористайтесь наведеною формулою.
` float temp = (firstByte*256 + secondByte) * 0.02 - 273.16 `

Інсує можливість зупинення сканеру. Для цього необхідно відправити байт зі значення 200. Робот перейде в режим очікування пілся закінчення сканування початого ряду.
