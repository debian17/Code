{-2,-1,0,1,2 - значени€ масссива дл€ актива
5 - пассив
7- пусто}
program laba3;

uses GraphABC, Graphics, Logic, Mouse;
const
  CELL_WIDTH = 50; //ширина клетки игрового пол€
  WIDTH = 900; //ширина игрового пол€
  HEIGHT = 900;//высота игрового пол€

var
  FIELD_SIZE: integer;//размер игрового пол€
  IN_ACTIVE: integer;//количество активов уже внесенных на поле
  IN_PASSIVE: integer;//количество пассивов уже внесенных на поле
  ACTIVE: integer;//количество активных объектов
  PASSIVE: integer;//количество пассивных объектов
  IN_TAKT: integer;
  TAKT: integer; //количество тактов
  BATMAN : Picture;
  SUPERMAN : Picture;
  FIELD_GAME: array[1..20, 1..20] of integer;//игровое поле
  FIELD_GAME_PREV: array[1..20, 1..20] of integer;//копи€ пол€, дл€ прорисовки предыдущего шага

//точка входа
begin
  SetWindowCaption('Ћабораторна€ работа є2');
  SetWindowPos(500, 10);
  SetWindowWIDTH(WIDTH);
  SetWindowHEIGHT(HEIGHT);
  IN_ACTIVE := 0;
  IN_PASSIVE := 0;
  SetFontSize(14);
  write('¬ведите размер пол€:  ');
  readln(FIELD_SIZE);
  writeln(FIELD_SIZE);
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
      FIELD_GAME[i, j] := 7;
  ARRAY_CLONE();
  Write('¬ведите сколько активных объектов:');
  ReadLN(ACTIVE);
  writeln(ACTIVE);
  Write('¬ведите сколько пассивных объектов:');
  ReadLN(PASSIVE);
  writeln(PASSIVE);
  write('¬ведите количество шагов: ');
  readln(IN_TAKT);
  writeln(IN_TAKT);
  TAKT := IN_TAKT;
  ClearWindow();
  DRAW(false);
  OnMouseDown := MOUSE_CLICK;
end.