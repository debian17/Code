unit Mouse;

interface

procedure MOUSE_CLICK(x, y, mb: integer);

implementation
uses GraphABC,Laba2,Graphics,Logic;

procedure MOUSE_CLICK(x, y, mb: integer);
var
  i, j: integer;
begin
  if (x < WIDTH - 300) and (y < HEIGHT - 10) and (x > WIDTH - 450) and (y > HEIGHT - 60) and (TAKT <> 0) then
  begin
    TAKT -= 1;
    PROC_TAKT();
  end;
  if (x < WIDTH - 300) and (y < HEIGHT - 10) and (x > WIDTH - 450) and (y > HEIGHT - 60) and (TAKT = 0) then
  begin
    SetPenColor(clWhite);
    SetBrushColor(clWhite);
    Rectangle(WIDTH - 298, HEIGHT - 8, WIDTH - 448, HEIGHT - 58);
    SetPenColor(clWhite);
    SetBrushColor(clWhite);
    Rectangle(WIDTH - 300, HEIGHT - 10, WIDTH - 450, HEIGHT - 60);
    SetFontSize(20);
    TextOut(WIDTH - 400, HEIGHT - 50, 'Игра окончена!');
    Redraw;
  end;
  if (x < 10) or (y < 10) or (x > FIELD_SIZE * CELL_WIDTH + 10) or (y > FIELD_SIZE * CELL_WIDTH + 10) then exit;
  LockDrawing;
  i := (x - 10) div CELL_WIDTH + 1;
  j := (y - 10) div CELL_WIDTH + 1;
  if (IN_ACTIVE < ACTIVE) then
  begin
    FIELD_GAME[i, j] := 0;
    inc(IN_ACTIVE);
    DRAW(false);
    Redraw();
    exit;
  end;
  if (IN_PASSIVE < PASSIVE) then
  begin
    FIELD_GAME[i, j] := 5;
    inc(IN_PASSIVE);
    DRAW(false);
    Redraw();
    exit;
  end; 
  Redraw;
end;
end.