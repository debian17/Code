unit Graphics;

interface

uses GraphABC;
procedure DRAW(flag: boolean);

implementation
uses Main;

procedure FIELD_RENDERING();
var
  x, y: integer;
  x1, y1: integer;
begin
  x := 10;
  y := 10;
  x1 := 30 + CELL_WIDTH * n;
  y1 := 10;
  SetBrushColor(clBlack);
  SetPenColor(clGreen);
  SetPenWidth(5);
  for var i := 1 to n + 1 do
  begin
    Line(x, y, x + CELL_WIDTH * (n), y);
    y += CELL_WIDTH;
    Line(x1, y1, x1 + CELL_WIDTH * (n), y1);
    y1 += CELL_WIDTH;
  end;
  y := 10;
  y1 := 10;
  for var i := 1 to n + 1 do
  begin
    Line(x, y, x, y + CELL_WIDTH * (n));
    x += CELL_WIDTH;
    Line(x1, y1, x1, y1 + CELL_WIDTH * (n));
    x1 += CELL_WIDTH;
  end;
  SetFontSize(12);
  SetBrushColor(Color.White); 
  {TextOut(10, 70 + CELL_WIDTH * n, 'Волки');
  TextOut(200, 70 + CELL_WIDTH * n, ACTIVE.ToString);
  SetFontSize(12);
  TextOut(10, 100 + CELL_WIDTH * n, 'Овцы');
  TextOut(200, 100 + CELL_WIDTH * n, PASSIVE.ToString);}
  SetFontSize(16);
  TextOut(10, 100 + CELL_WIDTH * n, 'Тактов игры осталось:');
  TextOut(250, 100 + CELL_WIDTH * n, TACT.ToString);
  SetFontSize(16);
  TextOut(100 + CELL_WIDTH * n, 20 + CELL_WIDTH * n, 'Такт№ ');
  if(IN_TACT - TACT - 1) >= 0 then
    TextOut(170 + CELL_WIDTH * n, 20 + CELL_WIDTH * n, (IN_TACT - TACT - 1).ToString);
  SetFontSize(16);
  TextOut(100, 20 + CELL_WIDTH * n, 'Такт№');
  TextOut(170, 20 + CELL_WIDTH * n, (IN_TACT - TACT).ToString);
end;

procedure DRAW(flag: boolean);
begin
  ClearWindow();
  ACTIVE_PICTURE := Picture.Create(60, 60);
  ACTIVE_PICTURE.Load('A.jpg');
  PASSIVE_PICTURE := Picture.Create(60, 60);
  PASSIVE_PICTURE.Load('P.jpg');
  FIELD_RENDERING();
  LockDrawing;
  if (flag) then
  begin
    for var i := 1 to n do
      for var j := 1 to n do
      begin
        if ((FIELD_OF_GAME_PREV[i, j] = 7) or (FIELD_OF_GAME_PREV[i, j] = -10)) then
        begin
          continue;
        end;
        if (FIELD_OF_GAME_PREV[i, j] = 5) then
        begin
          PASSIVE_PICTURE.DRAW(CELL_WIDTH * i + 2 + CELL_WIDTH * n - 30, CELL_WIDTH * j - 47, 50, 50);
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          continue;
        end;
        if (FIELD_OF_GAME_PREV[i, j] = -2) then
        begin
          FIELD_OF_GAME[i, j] := 7;
          SetPenColor(clWhite);
          SetBrushColor(clWhite);
          ACTIVE_PICTURE.DRAW(CELL_WIDTH * i + 2 + CELL_WIDTH * n - 30, CELL_WIDTH * j - 47, 50, 50);
          SetFontSize(14);
          TextOut(30 + CELL_WIDTH * n + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_OF_GAME_PREV[i, j].ToString()); 
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          continue;
        end;
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        ACTIVE_PICTURE.DRAW(CELL_WIDTH * i + 2 + CELL_WIDTH * n - 30, CELL_WIDTH * j - 47, 50, 50);
        SetPenColor(clBlack);
        SetBrushColor(clWhite);
        SetFontSize(14);
        if ((FIELD_OF_GAME_PREV[i, j] = -2) or (FIELD_OF_GAME_PREV[i, j] = -1) or (FIELD_OF_GAME_PREV[i, j] = 0) or (FIELD_OF_GAME_PREV[i, j] = 1) or (FIELD_OF_GAME_PREV[i, j] = 2)) then
          TextOut(30 + CELL_WIDTH * n + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_OF_GAME_PREV[i, j].ToString());  
      end; 
  end;
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if ((FIELD_OF_GAME[i, j] = 7) or (FIELD_OF_GAME[i, j] = -3)) then
      begin
        FIELD_OF_GAME[i, j] := 7;
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        SetPenColor(clBlack);
        SetBrushColor(clBlack);
        continue;
      end;
      if (FIELD_OF_GAME[i, j] = 5) then
      begin
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        PASSIVE_PICTURE.DRAW(CELL_WIDTH * i - 47, CELL_WIDTH * j - 47, 50, 50);
        SetPenColor(clBlack);
        SetBrushColor(clBlack);
        continue;
      end;    
      SetPenColor(clWhite);
      SetBrushColor(clWhite);
      ACTIVE_PICTURE.DRAW(CELL_WIDTH * i - 47, CELL_WIDTH * j - 47, 50, 50);
      SetPenColor(clBlack);
      SetBrushColor(clWhite);
      SetFontSize(14);
      if ((FIELD_OF_GAME[i, j] = -2) or (FIELD_OF_GAME[i, j] = -1) or (FIELD_OF_GAME[i, j] = 0) or (FIELD_OF_GAME[i, j] = 1) or (FIELD_OF_GAME[i, j] = 2)) then
        TextOut(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_OF_GAME[i, j].ToString()); 
    end;
  SetPenColor(clBlack);
  Redraw;
  BUTTON1.RedrawNow;
end;
end.