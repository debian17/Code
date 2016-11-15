{-2,-1,0,1,2 - значения масссива для актива
5 - пассив
7- пусто}
program laba3;

uses GraphABC;
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

//копирование игрового поля для прорисвки предыдущего шага
procedure CopyOfArray();
begin
  for var i := 1 to n do
    for var j := 1 to n do
      prev_field[i, j] := game_field[i, j];
end;

//прорисовка игрового поля
procedure Field();
var
  x, y: integer;
  x1, y1: integer;
begin
  x := 10;
  y := 10;
  x1 := 30 + step * n;
  y1 := 10;
  SetBrushColor(clBlack);
  SetPenColor(clBlack);
  for var i := 1 to n + 1 do
  begin
    Line(x, y, x + step * (n), y);
    y += step;
    Line(x1, y1, x1 + step * (n), y1);
    y1 += step;
  end;
  y := 10;
  y1 := 10;
  for var i := 1 to n + 1 do
  begin
    Line(x, y, x, y + step * (n));
    x += step;
    Line(x1, y1, x1, y1 + step * (n));
    x1 += step;
  end;
  SetFontSize(12);
  SetBrushColor(Color.White);
  TextOut(10, 70 + step * n, 'Мафия');
  TextOut(150, 70 + step * n, active.ToString);
  SetFontSize(12);
  TextOut(10, 100 + step * n, 'Мирные жители');
  TextOut(150, 100 + step * n, passive.ToString);
  SetFontSize(12);
  TextOut(10, 130 + step * n, 'Осталось тактов');
  TextOut(150, 130 + step * n, takt.ToString);
  
  SetFontSize(12);
  TextOut(100 + step * n, 20 + step * n, 'Такт');
  if(takt_input - takt - 1) >= 0 then
    TextOut(150 + step * n, 20 + step * n, (takt_input - takt - 1).ToString);
  SetFontSize(12);
  TextOut(100, 20 + step * n, 'Такт');
  TextOut(150, 20 + step * n, (takt_input - takt).ToString);
  
end;

procedure Draw(flag: boolean);
begin
  ClearWindow();
  Field();
  
  LockDrawing;
  if (flag) then
  begin
    for var i := 1 to n do
      for var j := 1 to n do
      begin
        if ((prev_field[i, j] = 7) or (prev_field[i, j] = -10)) then
        begin
          SetPenColor(clWhite);
          SetBrushColor(clWhite);
          rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          continue;
        end;
        if (prev_field[i, j] = 5) then
        begin
          SetPenColor(clLime);
          SetBrushColor(clLime);
          rectangle(step * i + 2 + step * n, step * j - 18, step * i + 29 + step * n, step * j + 9);
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          continue;
        end;
         //
        
        if (prev_field[i, j] = -2) then
        begin
          game_field[i, j] := 7;
          SetPenColor(clGray);
          SetBrushColor(clGray);
          rectangle(step * i + 2 + step * n, step * j - 18, step * i + 29 + step * n, step * j + 9);
          SetFontSize(8); 
          TextOut(30 + step * n + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), prev_field[i, j].ToString()); 
          SetPenColor(clBlack);
          SetBrushColor(clBlack);
          
          continue;
        end;
        
        
        SetPenColor(clCyan);
        SetBrushColor(clCyan);
        rectangle(step * i + 2 + step * n, step * j - 18, step * i + 29 + step * n, step * j + 9);
        SetPenColor(clBlack);
        SetBrushColor(clCyan);
        SetFontSize(8);
        if ((prev_field[i, j] = -2) or (prev_field[i, j] = -1) or (prev_field[i, j] = 0) or (prev_field[i, j] = 1) or (prev_field[i, j] = 2)) then
          TextOut(30 + step * n + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), prev_field[i, j].ToString());  
      end; 
  end;
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if ((game_field[i, j] = 7) or (game_field[i, j] = -3)) then
      begin
        //
        game_field[i, j] := 7;
        SetPenColor(clWhite);
        SetBrushColor(clWhite);
        rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
        SetPenColor(clBlack);
        SetBrushColor(clBlack);
        continue;
      end;
      if (game_field[i, j] = 5) then
      begin
        SetPenColor(clLime);
        SetBrushColor(clLime);
        rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
        SetPenColor(clBlack);
        SetBrushColor(clBlack);
        continue;
      end;    
      SetPenColor(clCyan);
      SetBrushColor(clCyan);
      rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
      SetPenColor(clBlack);
      SetBrushColor(clCyan);
      SetFontSize(8);
      if ((game_field[i, j] = -2) or (game_field[i, j] = -1) or (game_field[i, j] = 0) or (game_field[i, j] = 1) or (game_field[i, j] = 2)) then
        TextOut(10 + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), game_field[i, j].ToString()); 
    end;
  SetPenColor(clBlack);
  SetBrushColor(clGray);
  Rectangle(width - 298, height - 8, width - 448, height - 58);
  SetPenColor(clBlack);
  SetBrushColor(clWhite);
  Rectangle(width - 300, height - 10, width - 450, height - 60);
  SetFontSize(20);
  TextOut(width - 400, height - 50, 'ШАГ');
  Redraw;
  
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

//вводятся дополнительные значение 8 9 10 11 12, которые используются чтобы не было повторного использования точки
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
  
    ///////////////
  Draw(true);
  for var i := 1 to n do
    for var j := 1 to n do
    begin
      if (game_field[i, j] = -2) then
      begin
        //
        
        LockDrawing;
        SetPenColor(clGray);
        SetBrushColor(clGray);
        rectangle(step * i - 18, step * j - 18, step * i + 9, step * j + 9);
        SetFontSize(8);
        TextOut(10 + round(step / 2 + (i - 1) * step), 10 + round(step / 2 + (j - 1) * step), game_field[i, j].ToString());      
        //game_field[i, j] := 7;
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

procedure MouseDown(x, y, mb: integer);
var
  i, j: integer;
begin
  if (x < width - 300) and (y < height - 10) and (x > width - 450) and (y > height - 60) and (takt <> 0) then
  begin
    takt -= 1;
    p_takt();     
  end;
  if (x < width - 300) and (y < height - 10) and (x > width - 450) and (y > height - 60) and (takt = 0) then
  begin
    
    SetPenColor(clWhite);
    SetBrushColor(clWhite);
    Rectangle(width - 298, height - 8, width - 448, height - 58);
    SetPenColor(clWhite);
    SetBrushColor(clWhite);
    Rectangle(width - 300, height - 10, width - 450, height - 60);
    SetFontSize(20);
    TextOut(width - 400, height - 50, 'КОНЕЦ ИГРЫ');
    Redraw;
  end;
  if (x < 10) or (y < 10) or (x > n * step + 10) or (y > n * step + 10) then exit;
  LockDrawing;
  i := (x - 10) div step + 1;
  j := (y - 10) div step + 1;
  if (active_input < active) then
  begin
    game_field[i, j] := 0;
    inc(active_input);
    Draw(false);
    Redraw();
    exit;
  end;
  if (passive_input < passive) then
  begin
    game_field[i, j] := 5;
    inc(passive_input);
    Draw(false);
    Redraw();
    exit;
  end; 
  Redraw;
end;


//основная программа
begin
  SetWindowCaption('Action Мысливчик Т.И.');
  SetWindowPos(400, 50);
  SetWindowWidth(width);
  SetWindowHeight(height);
  active_input := 0;
  passive_input := 0;
  write('Введите размер поля ');
  readln(n);
  writeln(n);
  for var i := 1 to n do
    for var j := 1 to n do  game_field[i, j] := 7;
  CopyOfArray();
  Write('Количество активных объектов ');
  ReadLN(active);
  writeln(active);
  Write('Количество пассивных объектов ');
  ReadLN(passive);
  writeln(passive);
  write('Количество тактов ');
  readln(takt_input);
  writeln(takt_input);
  takt := takt_input;
  ClearWindow();
  Draw(false); 
  OnMouseDown := MouseDown;
end.