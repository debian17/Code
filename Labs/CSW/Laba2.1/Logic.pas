unit Logic;

interface

procedure CHECK_FREE();
procedure ARRAY_CLONE();
procedure PROC_TACT();

implementation
uses GraphABC,Main,Graphics;

procedure ARRAY_CLONE();
begin
  for var i := 1 to n do
    for var j := 1 to n do
      FIELD_OF_GAME_PREV[i, j] := FIELD_OF_GAME[i, j];
end;

procedure CREATE_NEW_ACTIVE();
var
  x, y: integer;
begin
  while(true) do
  begin
    x := random(n) + 1;
    y := random(n) + 1;
    if ((FIELD_OF_GAME[x, y] = 5) or (FIELD_OF_GAME[x, y] = 7)) then
    begin
      FIELD_OF_GAME[x, y] := 0;
      inc(ACTIVE);
      break;
    end;
  end;
end;

procedure CREATE_NEW_ACTIVE(i, j: integer);
begin
  FIELD_OF_GAME[i, j] := -10;
  inc(ACTIVE);
end;

function CHECK(i, j, key: integer): boolean;
var
  res: boolean;
begin
  res := false;
  case key of
    1:
      if (i < n) then
        res := true;
    2:
      if (i > 1) then
        res := true;
    3:
      if (j < n) then
        res := true;
    4:
      if (j > 1) then
        res := true;
  end;
  result := res;
end;

procedure CHECK_FREE();
var
  count: integer;
begin
  count := 0;
  for var i := 1 to n do
  begin
    for var j := 1 to n do
    begin
      count := 0;
      if (FIELD_OF_GAME[i, j] <> 7) then continue;
      if (CHECK(i, j, 1)) then
        if ((FIELD_OF_GAME[i + 1, j] = -1) or (FIELD_OF_GAME[i + 1, j] = 0) or (FIELD_OF_GAME[i + 1, j] = 1)) then
          inc(count);
      if (CHECK(i, j, 2)) then
        if ((FIELD_OF_GAME[i - 1, j] = -1) or (FIELD_OF_GAME[i - 1, j] = 0) or (FIELD_OF_GAME[i - 1, j] = 1)) then
          inc(count);
      if (CHECK(i, j, 3)) then 
        if ((FIELD_OF_GAME[i, j + 1] = -1) or (FIELD_OF_GAME[i, j + 1] = 0) or (FIELD_OF_GAME[i, j + 1] = -1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CREATE_NEW_ACTIVE(i, j);
            continue;
          end;
        end;
      if (CHECK(i, j, 4)) then
        if ((FIELD_OF_GAME[i, j - 1] = -1) or (FIELD_OF_GAME[i, j - 1] = 0) or (FIELD_OF_GAME[i, j - 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CREATE_NEW_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 3)) and (CHECK(i, j, 1))) then
        if ((FIELD_OF_GAME[i + 1, j + 1] = -1) or (FIELD_OF_GAME[i + 1, j + 1] = 0) or (FIELD_OF_GAME[i + 1, j + 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CREATE_NEW_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 4)) and (CHECK(i, j, 1))) then
        if ((FIELD_OF_GAME[i + 1, j - 1] = -1) or (FIELD_OF_GAME[i + 1, j - 1] = 0) or (FIELD_OF_GAME[i + 1, j - 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CREATE_NEW_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 3)) and (CHECK(i, j, 2))) then
        if ((FIELD_OF_GAME[i - 1, j + 1] = -1) or (FIELD_OF_GAME[i - 1, j + 1] = 0) or (FIELD_OF_GAME[i - 1, j + 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CREATE_NEW_ACTIVE(i, j);
            continue;
          end;
        end;
      if ((CHECK(i, j, 4)) and (CHECK(i, j, 2))) then 
        if ((FIELD_OF_GAME[i - 1, j - 1] = -1) or (FIELD_OF_GAME[i - 1, j - 1] = 0) or (FIELD_OF_GAME[i - 1, j - 1] = 0)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CREATE_NEW_ACTIVE(i, j);
            continue;
          end;
        end;
    end;
  end;
end;

function CHANGE_STEP_ACTIVE(i, j: integer): boolean;
var
  res: boolean;
begin
  if (FIELD_OF_GAME[i, j] = 5) then
  begin
    FIELD_OF_GAME[i, j] := 7;
    res := true;
    dec(PASSIVE);
  end;
  if (CHECK(i, j, 1)) then
    if (FIELD_OF_GAME[i + 1, j] = 5) then
    begin
      FIELD_OF_GAME[i + 1, j] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if (CHECK(i, j, 2)) then
    if (FIELD_OF_GAME[i - 1, j] = 5) then
    begin
      FIELD_OF_GAME[i - 1, j] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if (CHECK(i, j, 3)) then
    if (FIELD_OF_GAME[i, j + 1] = 5) then
    begin
      FIELD_OF_GAME[i, j + 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if (CHECK(i, j, 4)) then
    if (FIELD_OF_GAME[i, j - 1] = 5) then
    begin
      FIELD_OF_GAME[i, j - 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 3)) and (CHECK(i, j, 1))) then
    if (FIELD_OF_GAME[i + 1, j + 1] = 5) then
    begin
      FIELD_OF_GAME[i + 1, j + 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 4)) and (CHECK(i, j, 1))) then
    if (FIELD_OF_GAME[i + 1, j - 1] = 5) then
    begin
      FIELD_OF_GAME[i + 1, j - 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 2)) and (CHECK(i, j, 3))) then
    if (FIELD_OF_GAME[i - 1, j + 1] = 5) then
    begin
      FIELD_OF_GAME[i - 1, j + 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  if ((CHECK(i, j, 4)) and (CHECK(i, j, 2))) then
    if (FIELD_OF_GAME[i - 1, j - 1] = 5) then
    begin
      FIELD_OF_GAME[i - 1, j - 1] := 7;
      res := true;
      dec(PASSIVE);
    end;
  result := res;
end;

procedure PROC_TACT();
var
  x, y: integer;
begin
  CHECK_FREE;
  for var i := 1 to n do
    for var j := 1 to n do
      if (FIELD_OF_GAME_PREV[i, j] = -10) then
      begin
        FIELD_OF_GAME[i, j] := FIELD_OF_GAME_PREV[i, j] + 10;
        FIELD_OF_GAME_PREV[i, j] := 7;
        
      end;
  DRAW(false);
  ARRAY_CLONE();
  LockDrawing;
  BUTTON1.Visible := true;
  DRAW(false);
  Redraw;
  BUTTON1.Redraw;
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if ((FIELD_OF_GAME[i, j] = -1) or (FIELD_OF_GAME[i, j] = 0) or (FIELD_OF_GAME[i, j] = 1) or (FIELD_OF_GAME[i, j] = 2)) then//(FIELD_OF_GAME[i, j] = -2) or 
      begin
        while (true) do
        begin
          x := random(n) + 1;
          y := random(n) + 1;
          if ((FIELD_OF_GAME[x, y] = 5) or (FIELD_OF_GAME[x, y] = 7)) then
            break;
        end;
        if (CHANGE_STEP_ACTIVE(x, y)) then
          FIELD_OF_GAME[x, y] := FIELD_OF_GAME[i, j] + 11
        else
          FIELD_OF_GAME[x, y] := FIELD_OF_GAME[i, j] - 11;
        FIELD_OF_GAME[i, j] := 7;
        LockDrawing;
        SetPenColor(Color.Green);
        //
        Redraw();
        //
        BUTTON1.Redraw;
      end;
    end; 
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if (FIELD_OF_GAME[i, j] > 7) then
        FIELD_OF_GAME[i, j] -= 10;
      if (FIELD_OF_GAME[i, j] < -5) then
        FIELD_OF_GAME[i, j] += 10;
    end;
  DRAW(true);
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if (FIELD_OF_GAME[i, j] = -2) then
      begin        
        LockDrawing;
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        SetFontSize(12);
        TextOut(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_OF_GAME[i, j].ToString());      
        Redraw();
        BUTTON1.Redraw;
        dec(ACTIVE);
      end;
      
      if (FIELD_OF_GAME[i, j] = 2) then
      begin
        LockDrawing;
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        SetFontSize(12);
        TextOut(10 + round(CELL_WIDTH / 2 + (i - 1) * CELL_WIDTH), 10 + round(CELL_WIDTH / 2 + (j - 1) * CELL_WIDTH), FIELD_OF_GAME[i, j].ToString());
        CREATE_NEW_ACTIVE();
        FIELD_OF_GAME[i, j] := 0;
        Redraw;
        BUTTON1.Redraw;
      end;
    end;  
end;
end.