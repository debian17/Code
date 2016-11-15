unit Graphics;

interface

uses GraphABC;

procedure Draw(flag: boolean);

implementation
uses Laba2;


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



end.
