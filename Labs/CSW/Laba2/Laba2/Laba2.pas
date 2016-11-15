{-2,-1,0,1,2 - значения масссива для актива
5 - пассив
7- пусто}
program laba3;

uses GraphABC, Graphics,Logic,Mouse;
const
  step = 60; //ширина клетки игрового поля
  width = 800; //ширина игрового поля
  height = 600;//высота игрового поля

var
  n: integer;//размер игрового поля
  active_input: integer;//количество активов уже внесенных на поле
  passive_input: integer;//количество пассивов уже внесенных на поле
  active: integer;//количество активных объектов
  passive: integer;//количество пассивных объектов
  takt_input: integer;
  takt: integer; //количество тактов
  
  game_field: array [1..20, 1..20] of integer;//игровое поле
  prev_field: array [1..20, 1..20] of integer;//копия поля, для прорисовки предыдущего шага


//точка входа
begin
  SetWindowCaption('Лабораторная работа №2');
  SetWindowPos(500, 100);
  SetWindowWidth(width);
  SetWindowHeight(height);
  active_input := 0;
  passive_input := 0;
  write('Размер поля:  ');
  readln(n);
  writeln(n);
  for var i := 1 to n do
    for var j := 1 to n do  game_field[i, j] := 7;
  CopyOfArray();
  Write('Сколько активных объектов? ');
  ReadLN(active);
  writeln(active);
  Write('Сколько пассивных объектов? ');
  ReadLN(passive);
  writeln(passive);
  write('Количество шагов: ');
  readln(takt_input);
  writeln(takt_input);
  takt := takt_input;
  ClearWindow();
  Draw(false); 
  OnMouseDown := MouseDown;
end.