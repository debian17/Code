unit Logic;

interface

procedure INSPECTION_FREE();
procedure ARRAY_CLONE();
procedure PROC_TAKT();

implementation
uses GraphABC,Laba2,Graphics;

//вводится ключ key-ключ проверки
//1   проверка на i+1
//2   проверка на i-1
//3  проверка на j+1
//4   проверка на j-1
function CHECK(i, j, key: integer): boolean;
var
  res: boolean;
begin
  res := false;
  case key of
    1:
      if (i < FIELD_SIZE) then
        res := true;
    2:
      if (i > 1) then
        res := true;
    3:
      if (j < FIELD_SIZE) then
        res := true;
    4:
      if (j > 1) then
        res := true;
  end;
  result := res;
end;

procedure C_N_ACTIVE();
var
  x, y: integer;
begin
  while(true) do
  begin
    x := random(FIELD_SIZE) + 1;
    y := random(FIELD_SIZE) + 1;
    if ((FIELD_GAME[x, y] = 5) or (FIELD_GAME[x, y] = 7)) then
    begin
      FIELD_GAME[x, y] := 0;
      inc(ACTIVE);
      break;
    end;
  end;
end;

procedure C_N_ACTIVE(i, j: integer);
begin
  FIELD_GAME[i, j] := -10;
  inc(ACTIVE);
end;

//поиск места для прыжка
procedure INSPECTION_FREE();
var
  count: integer;
begin
  count := 0;
  for var i := 1 to FIELD_SIZE do
  begin
    for var j := 1 to FIELD_SIZE do
    begin
      count := 0;
      if (FIELD_GAME[i, j] <> 7) then continue;
      if (CHECK(i, j, 1)) then
        if ((FIELD_GAME[i + 1, j] = -1) or (FIELD_GAME[i + 1, j] = 0) or (FIELD_GAME[i + 1, j] = 1)) then
          inc(count);
      if (CHECK(i, j, 2)) then
        if ((FIELD_GAME[i - 1, j] = -1) or (FIELD_GAME[i - 1, j] = 0) or (FIELD_GAME[i - 1, j] = 1)) then
          inc(count);
      if (CHECK(i, j, 3)) then 
        if ((FIELD_GAME[i, j + 1] = -1) or (FIELD_GAME[i, j + 1] = 0) or (FIELD_GAME[i, j + 1] = -1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            C_N_ACTIVE(i, j);
            continue;
          end;
        end;
      if (CHECK(i, j, 4)) then
        if ((FIELD_GAME[i, j - 1] = -1) or (FIELD_GAME[i, j - 1] = 0) or (FIELD_GAME[i, j - 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            C_N_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 3)) and (CHECK(i, j, 1))) then
        if ((FIELD_GAME[i + 1, j + 1] = -1) or (FIELD_GAME[i + 1, j + 1] = 0) or (FIELD_GAME[i + 1, j + 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            C_N_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 4)) and (CHECK(i, j, 1))) then
        if ((FIELD_GAME[i + 1, j - 1] = -1) or (FIELD_GAME[i + 1, j - 1] = 0) or (FIELD_GAME[i + 1, j - 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            C_N_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 3)) and (CHECK(i, j, 2))) then
        if ((FIELD_GAME[i - 1, j + 1] = -1) or (FIELD_GAME[i - 1, j + 1] = 0) or (FIELD_GAME[i - 1, j + 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            C_N_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 4)) and (CHECK(i, j, 2))) then 
        if ((FIELD_GAME[i - 1, j - 1] = -1) or (FIELD_GAME[i - 1, j - 1] = 0) or (FIELD_GAME[i - 1, j - 1] = 0)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            C_N_ACTIVE(i, j);
            continue;
          end;
        end;
    end;
  end;
end;

procedure ARRAY_CLONE();
begin
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
      FIELD_GAME_PREV[i, j] := FIELD_GAME[i, j];
end;

function CH_CELL_WIDTH_ACTIVE(i, j: integer): boolean;
var
  res: boolean;
begin
  if (FIELD_GAME[i, j] = 5) then
  begin
    FIELD_GAME[i, j] := 7;
    res := true;
    dec(PASSIVE);
  end;
  if (CHECK(i, j, 1)) then
    if (FIELD_GAME[i + 1, j] = 5) then
    begin
      FIELD_GAME[i + 1, j] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if (CHECK(i, j, 2)) then
    if (FIELD_GAME[i - 1, j] = 5) then
    begin
      FIELD_GAME[i - 1, j] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if (CHECK(i, j, 3)) then
    if (FIELD_GAME[i, j + 1] = 5) then
    begin
      FIELD_GAME[i, j + 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if (CHECK(i, j, 4)) then
    if (FIELD_GAME[i, j - 1] = 5) then
    begin
      FIELD_GAME[i, j - 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 3)) and (CHECK(i, j, 1))) then
    if (FIELD_GAME[i + 1, j + 1] = 5) then
    begin
      FIELD_GAME[i + 1, j + 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 4)) and (CHECK(i, j, 1))) then
    if (FIELD_GAME[i + 1, j - 1] = 5) then
    begin
      FIELD_GAME[i + 1, j - 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 2)) and (CHECK(i, j, 3))) then
    if (FIELD_GAME[i - 1, j + 1] = 5) then
    begin
      FIELD_GAME[i - 1, j + 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 4)) and (CHECK(i, j, 2))) then
    if (FIELD_GAME[i - 1, j - 1] = 5) then
    begin
      FIELD_GAME[i - 1, j - 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  result := res;
end;

procedure PROC_TAKT();
var
  x, y: integer;
begin
  INSPECTION_FREE;
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
      if (FIELD_GAME_PREV[i, j] = -10) then
      begin
        FIELD_GAME[i, j] := FIELD_GAME_PREV[i, j] + 10;
        FIELD_GAME_PREV[i, j] := 7;
        
      end;
  DRAW(false);
  ARRAY_CLONE();
  LockDrawing;
  DRAW(false);
  Redraw;
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
    begin
      if ((FIELD_GAME[i, j] = -1) or (FIELD_GAME[i, j] = 0) or (FIELD_GAME[i, j] = 1) or (FIELD_GAME[i, j] = 2)) then//(FIELD_GAME[i, j] = -2) or 
      begin
        while (true) do
        begin
          x := random(FIELD_SIZE) + 1;
          y := random(FIELD_SIZE) + 1;
          if ((FIELD_GAME[x, y] = 5) or (FIELD_GAME[x, y] = 7)) then
            break;
        end;
        if (CH_CELL_WIDTH_ACTIVE(x, y)) then
          FIELD_GAME[x, y] := FIELD_GAME[i, j] + 11
        else
          FIELD_GAME[x, y] := FIELD_GAME[i, j] - 11;
        FIELD_GAME[i, j] := 7;
        LockDrawing;
        SetPenColor(Color.Red);
        //Line(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (x - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (y - 1) * CELL_WIDTH));
        //Line(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (x - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (y - 1) * CELL_WIDTH));
        Redraw();
        //Sleep(2000);
      end;
    end; 
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
    begin
      if (FIELD_GAME[i, j] > 7) then
        FIELD_GAME[i, j] -= 10;
      if (FIELD_GAME[i, j] < -5) then
        FIELD_GAME[i, j] += 10;
    end;  
  DRAW(true);
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
    begin
      if (FIELD_GAME[i, j] = -2) then
      begin
        LockDrawing;
        SetPenColor(clRed);
        SetBrushColor(clRed);
        //BATMAN.Draw(CELL_WIDTH* i + 2 + CELL_WIDTH* FIELD_SIZE-290, CELL_WIDTH* j - 30, 40, 40);
        //rectangle(CELL_WIDTH * i - 18, CELL_WIDTH * j - 18, CELL_WIDTH * i + 9, CELL_WIDTH * j + 9);
        SetFontSize(8);
        TextOut(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_GAME[i, j].ToString());      
        Redraw();
        dec(ACTIVE);
      end;      
      if (FIELD_GAME[i, j] = 2) then
      begin
        LockDrawing;
        SetPenColor(clRed);
        SetBrushColor(clRed);
        //BATMAN.Draw(CELL_WIDTH* i + 2 + CELL_WIDTH* FIELD_SIZE-290, CELL_WIDTH* j - 30, 40, 40);
        //rectangle(CELL_WIDTH * i - 18, CELL_WIDTH * j - 18, CELL_WIDTH * i + 9, CELL_WIDTH * j + 9);
        SetFontSize(10);
        TextOut(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_GAME[i, j].ToString());
        C_N_ACTIVE();
        FIELD_GAME[i, j] := 0;
        Redraw;
      end;
    end; 
end;
end.