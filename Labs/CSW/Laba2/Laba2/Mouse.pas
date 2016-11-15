unit Mouse;

interface

procedure MouseDown(x, y, mb: integer);

implementation
uses GraphABC,Laba2,Graphics,Logic;


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
    TextOut(width - 400, height - 50, ' ŒÕ≈÷ »√–€');
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



end.
