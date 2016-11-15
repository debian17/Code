unit Graphics;

interface

uses GraphABC;

procedure DRAW(flag: boolean);

implementation
uses Laba2;

//прорисовка игрового поля
procedure FIELD_RENDERING();
var
  x, y: integer;
  x1, y1: integer;
begin
  x := 10;
  y := 10;
  x1 := 30 + CELL_WIDTH * FIELD_SIZE;
  y1 := 10;
  SetBrushColor(clBlack);
  SetPenColor(clBlack);
  for var i := 1 to FIELD_SIZE + 1 do
  begin
    Line(x, y, x + CELL_WIDTH * (FIELD_SIZE), y);
    y += CELL_WIDTH;
    Line(x1, y1, x1 + CELL_WIDTH * (FIELD_SIZE), y1);
    y1 += CELL_WIDTH;
  end;
  y := 10;
  y1 := 10;
  for var i := 1 to FIELD_SIZE + 1 do
  begin
    Line(x, y, x, y + CELL_WIDTH * (FIELD_SIZE));
    x += CELL_WIDTH;
    Line(x1, y1, x1, y1 + CELL_WIDTH * (FIELD_SIZE));
    x1 += CELL_WIDTH;
  end;
  SetFontSize(14);
  SetFontColor(clBlue);
  SetBrushColor(Color.White);
  TextOut(10, 70 + CELL_WIDTH * FIELD_SIZE, 'Активные');
  TextOut(250, 70 + CELL_WIDTH * FIELD_SIZE, ACTIVE.ToString);
  SetFontSize(16);
  TextOut(10, 100 + CELL_WIDTH * FIELD_SIZE, 'Пассивные');
  TextOut(250, 100 + CELL_WIDTH * FIELD_SIZE, PASSIVE.ToString);
  SetFontSize(16);
  TextOut(10, 130 + CELL_WIDTH * FIELD_SIZE, 'Осталось тактов');
  TextOut(250, 130 + CELL_WIDTH * FIELD_SIZE, TAKT.ToString);  
  SetFontSize(16);
  TextOut(100 + CELL_WIDTH * FIELD_SIZE, 20 + CELL_WIDTH * FIELD_SIZE, 'Такт');
  if(IN_TAKT - TAKT - 1) >= 0 then
    TextOut(150 + CELL_WIDTH * FIELD_SIZE, 20 + CELL_WIDTH * FIELD_SIZE, (IN_TAKT - TAKT - 1).ToString);
  SetFontSize(16);
  TextOut(100, 20 + CELL_WIDTH * FIELD_SIZE, 'Такт');
  TextOut(150, 20 + CELL_WIDTH * FIELD_SIZE, (IN_TAKT - TAKT).ToString); 
end;

procedure DRAW(flag: boolean);
begin
  ClearWindow();
  FIELD_RENDERING();
  LockDrawing;
  if (flag) then
  begin
    for var i := 1 to FIELD_SIZE do
      for var j := 1 to FIELD_SIZE do
      begin
        if ((FIELD_GAME_PREV[i, j] = 7) or (FIELD_GAME_PREV[i, j] = -10)) then
        begin
          SetPenColor(clWhite);
          SetBrushColor(clWhite);
          circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          continue;
        end;
        if (FIELD_GAME_PREV[i, j] = 5) then
        begin
          SetPenColor(clRed);
          SetBrushColor(clRed);
          circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          continue;
        end;        
        if (FIELD_GAME_PREV[i, j] = -2) then
        begin
          FIELD_GAME[i, j] := 7;
          SetPenColor(clYellow);
          SetBrushColor(clYellow);
          circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
          SetFontSize(8);
          TextOut(30 + CELL_WIDTH * FIELD_SIZE + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_GAME_PREV[i, j].ToString()); 
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          
          continue;
        end;       
        SetPenColor(clCyan);
        SetBrushColor(clCyan);
        circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
        SetPenColor(clBlack);
        SetBrushColor(clCyan);
        SetFontSize(8);
        if ((FIELD_GAME_PREV[i, j] = -2) or (FIELD_GAME_PREV[i, j] = -1) or (FIELD_GAME_PREV[i, j] = 0) or (FIELD_GAME_PREV[i, j] = 1) or (FIELD_GAME_PREV[i, j] = 2)) then
          TextOut(30 + CELL_WIDTH * FIELD_SIZE + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_GAME_PREV[i, j].ToString());  
      end; 
  end;
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
    begin
      if ((FIELD_GAME[i, j] = 7) or (FIELD_GAME[i, j] = -3)) then
      begin
        FIELD_GAME[i, j] := 7;
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        //rectangle(CELL_WIDTH * i - 18, CELL_WIDTH * j - 18, CELL_WIDTH * i + 9, CELL_WIDTH * j + 9);
        circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
        SetPenColor(clBlack);
        SetBrushColor(clBlack);
        continue;
      end; 
      if (FIELD_GAME[i, j] = 5) then
      begin
        SetPenColor(clLime);
        SetBrushColor(clRed);
        //circle(CELL_WIDTH * i - 18, CELL_WIDTH * j - 18, CELL_WIDTH * i + 9, CELL_WIDTH * j + 9);
        circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
        SetPenColor(clBlack);
        SetBrushColor(clBlack);
        continue;
      end;
      SetPenColor(clCyan);
      SetBrushColor(clCyan);
      circle(CELL_WIDTH * i - 15, CELL_WIDTH * j - 15, 20);
      SetPenColor(clBlack);
      SetBrushColor(clCyan);
      SetFontSize(8);
      if ((FIELD_GAME[i, j] = -2) or (FIELD_GAME[i, j] = -1) or (FIELD_GAME[i, j] = 0) or (FIELD_GAME[i, j] = 1) or (FIELD_GAME[i, j] = 2)) then
        TextOut(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_GAME[i, j].ToString()); 
    end;
  SetPenColor(clBlack);
  SetBrushColor(clGray);
  Rectangle(WIDTH - 298, HEIGHT - 8, WIDTH - 448, HEIGHT - 58);
  SetPenColor(clBlack);
  SetBrushColor(clWhite);
  Rectangle(WIDTH - 300, HEIGHT - 10, WIDTH - 450, HEIGHT - 60);
  SetFontSize(20);
  TextOut(WIDTH - 400, HEIGHT - 50, 'ШАГ');
  Redraw; 
end;
end.
