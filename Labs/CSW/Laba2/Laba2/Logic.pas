unit Logic;

interface

procedure CheckFree();
procedure CopyOfArray();
procedure p_takt();

implementation
uses GraphABC,Laba2,Graphics;

//вводится ключ key-ключ проверки
//1   проверка на i+1
//2   проверка на i-1
//3  проверка на j+1
//4   проверка на j-1
function Check(i, j, key: integer): boolean;
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

procedure CreateNewActive();
var
  x, y: integer;
begin
  while(true) do
  begin
    x := random(n) + 1;
    y := random(n) + 1;
    if ((game_field[x, y] = 5) or (game_field[x, y] = 7)) then
    begin
      game_field[x, y] := 0;
      inc(active);
      break;
    end;
  end;
end;

procedure CreateNewActive(i, j: integer);
begin
  game_field[i, j] := -10;
  inc(active);
end;


//поиск места для прыжка
procedure CheckFree();
var
  count: integer;
begin
  count := 0;
  for var i := 1 to n do
  begin
    for var j := 1 to n do
    begin
      count := 0;
      if (game_field[i, j] <> 7) then continue;
      if (Check(i, j, 1)) then
        if ((game_field[i + 1, j] = -1) or (game_field[i + 1, j] = 0) or (game_field[i + 1, j] = 1)) then
          inc(count);
      if (Check(i, j, 2)) then
        if ((game_field[i - 1, j] = -1) or (game_field[i - 1, j] = 0) or (game_field[i - 1, j] = 1)) then
          inc(count);
      if (Check(i, j, 3)) then 
        if ((game_field[i, j + 1] = -1) or (game_field[i, j + 1] = 0) or (game_field[i, j + 1] = -1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CreateNewActive(i, j);
            continue;
          end;
        end;
      if (Check(i, j, 4)) then
        if ((game_field[i, j - 1] = -1) or (game_field[i, j - 1] = 0) or (game_field[i, j - 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CreateNewActive(i, j);
            continue;
          end;
        end;
      if ((Check(i, j, 3)) and (Check(i, j, 1))) then
        if ((game_field[i + 1, j + 1] = -1) or (game_field[i + 1, j + 1] = 0) or (game_field[i + 1, j + 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CreateNewActive(i, j);
            continue;
          end;
        end;
      if ((Check(i, j, 4)) and (Check(i, j, 1))) then
        if ((game_field[i + 1, j - 1] = -1) or (game_field[i + 1, j - 1] = 0) or (game_field[i + 1, j - 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CreateNewActive(i, j);
            continue;
          end;
        end;
      if ((Check(i, j, 3)) and (Check(i, j, 2))) then
        if ((game_field[i - 1, j + 1] = -1) or (game_field[i - 1, j + 1] = 0) or (game_field[i - 1, j + 1] = 1)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CreateNewActive(i, j);
            continue;
          end;
        end;
      if ((Check(i, j, 4)) and (Check(i, j, 2))) then 
        if ((game_field[i - 1, j - 1] = -1) or (game_field[i - 1, j - 1] = 0) or (game_field[i - 1, j - 1] = 0)) then
        begin
          inc(count);
          if (count = 3) then
          begin
            CreateNewActive(i, j);
            continue;
          end;
        end;
    end;
  end;
end;


//копирование игрового поля для прорисвки предыдущего шага
procedure CopyOfArray();
begin
  for var i := 1 to n do
    for var j := 1 to n do
      prev_field[i, j] := game_field[i, j];
end;

function ChengeStepActive(i, j: integer): boolean;
var
  res: boolean;
begin
  if (game_field[i, j] = 5) then
  begin
    game_field[i, j] := 7;
    res := true;
    dec(passive);
  end;
  if (Check(i, j, 1)) then
    if (game_field[i + 1, j] = 5) then
    begin
      game_field[i + 1, j] := 7;
      res := true;
      dec(passive);
    end;
  if (Check(i, j, 2)) then
    if (game_field[i - 1, j] = 5) then
    begin
      game_field[i - 1, j] := 7;
      res := true;
      dec(passive);
    end;
  if (Check(i, j, 3)) then
    if (game_field[i, j + 1] = 5) then
    begin
      game_field[i, j + 1] := 7;
      res := true;
      dec(passive);
    end;
  if (Check(i, j, 4)) then
    if (game_field[i, j - 1] = 5) then
    begin
      game_field[i, j - 1] := 7;
      res := true;
      dec(passive);
    end;
  if ((Check(i, j, 3)) and (Check(i, j, 1))) then
    if (game_field[i + 1, j + 1] = 5) then
    begin
      game_field[i + 1, j + 1] := 7;
      res := true;
      dec(passive);
    end;
  if ((Check(i, j, 4)) and (Check(i, j, 1))) then
    if (game_field[i + 1, j - 1] = 5) then
    begin
      game_field[i + 1, j - 1] := 7;
      res := true;
      dec(passive);
    end;
  if ((Check(i, j, 2)) and (Check(i, j, 3))) then
    if (game_field[i - 1, j + 1] = 5) then
    begin
      game_field[i - 1, j + 1] := 7;
      res := true;
      dec(passive);
    end;
  if ((Check(i, j, 4)) and (Check(i, j, 2))) then
    if (game_field[i - 1, j - 1] = 5) then
    begin
      game_field[i - 1, j - 1] := 7;
      res := true;
      dec(passive);
    end;
  result := res;
end;


procedure p_takt();
var
  x, y: integer;
begin
  CheckFree;
  for var i := 1 to n do
    for var j := 1 to n do
      if (prev_field[i, j] = -10) then 
      begin
        game_field[i, j] := prev_field[i, j] + 10;
        prev_field[i, j] := 7;
        
      end;
  Draw(false);
  CopyOfArray();
  
  LockDrawing;
  Draw(false);
  Redraw;
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if ((game_field[i, j] = -1) or (game_field[i, j] = 0) or (game_field[i, j] = 1) or (game_field[i, j] = 2)) then//(game_field[i, j] = -2) or 
      begin
        while (true) do
        begin
          x := random(n) + 1;
          y := random(n) + 1;
          if ((game_field[x, y] = 5) or (game_field[x, y] = 7)) then
            break;
        end;
        if (ChengeStepActive(x, y)) then
          game_field[x, y] := game_field[i, j] + 11
        else
          game_field[x, y] := game_field[i, j] - 11;
        game_field[i, j] := 7;
        LockDrawing;
        SetPenColor(Color.Red);
           //
        //Line(10+step*n + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step),10+step*n + round(step / 2 + (x - 1) * step), 10 + round(step / 2 + (y - 1) * step));
        Line(10 + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), 10 + round(step / 2 + (x - 1) * step), 10 + round(step / 2 + (y - 1) * step));Line(10 + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), 10 + round(step / 2 + (x - 1) * step), 10 + round(step / 2 + (y - 1) * step));
        Redraw();
        Sleep(2000);
      end;
    end;
  
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if (game_field[i, j] > 7) then
        game_field[i, j] -= 10;
      if (game_field[i, j] < -5) then
        game_field[i, j] += 10;
    end;

  Draw(true);
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if (game_field[i, j] = -2) then
      begin
        LockDrawing;
        SetPenColor(clGray);
        SetBrushColor(clGray);
        rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
        SetFontSize(8);
        TextOut(10 + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), game_field[i, j].ToString());      
        Redraw();  
        dec(active);
      end;
      
      if (game_field[i, j] = 2) then
      begin
        LockDrawing;
        SetPenColor(clLightCyan);
        SetBrushColor(clLightCyan);
        rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
        SetFontSize(8);
        TextOut(10 + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), game_field[i, j].ToString());
        CreateNewActive();
        game_field[i, j] := 0;
        Redraw;
      end;
    end; 
end;

end.
