program Laba2;

uses GraphABC, ABCButtons, Graphics, Logic, Mouse;
const
  CELL_WIDTH = 60;
var
  ACTIVE_PICTURE: Picture;
  PASSIVE_PICTURE: Picture;
  width, height: integer;
  BUTTON1 := new ButtonABC(width - 1000, height - 100, 200, 100, 'СЛЕДУЮЩИЙ ТАКТ', clWhite);
  n: integer;
  IN_ACTIVE: integer;
  IN_PASSIVE: integer;
  ACTIVE: integer;
  PASSIVE: integer;
  IN_TACT: integer;
  TACT: integer;
  FIELD_OF_GAME: array[1..20, 1..20] of integer;
  FIELD_OF_GAME_PREV: array[1..20, 1..20] of integer;
  
begin
  BUTTON1.Visible := false;
  BUTTON1.OnClick := ON_TACT_CLICK;
  width := 1000; 
  height := 600; 
  SetWindowCaption('Лабораторная работа №2');
  SetWindowPos(400, 50);
  SetWindowWidth(width);
  SetWindowHeight(height);
  SetBrushColor(clWhite);
  SetFontSize(12);
  IN_ACTIVE := 0;
  IN_PASSIVE := 0;
  write('Введите размер поля ');
  readln(n);
  writeln(n);
  for var i := 1 to n do
    for var j := 1 to n do FIELD_OF_GAME[i, j] := 7;
  ARRAY_CLONE();
  Write('Введите количество активных объектов ');
  ReadLN(ACTIVE);
  writeln(ACTIVE);
  Write(' Введите количество пассивных объектов  ');
  ReadLN(PASSIVE);
  writeln(PASSIVE);
  write('Введите количество тактов игры  ');
  readln(IN_TACT);
  writeln(IN_TACT);
  TACT := IN_TACT;
  ClearWindow();
  BUTTON1.Visible := true;
  width := Window.Width;
  height := Window.Height;
  BUTTON1.Left := width - 1900;
  BUTTON1.Top := height - 150;
  DRAW(false);
  OnMouseDown := MOUSE_DOWN;
end.