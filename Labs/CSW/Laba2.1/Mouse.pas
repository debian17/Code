unit Mouse;

interface
procedure MOUSE_DOWN(x, y, mb: integer);
procedure ON_TACT_CLICK();

implementation
uses Main, Logic, GraphABC, Graphics;

procedure ON_TACT_CLICK();
begin
  if (TACT <> 0) then
  begin
    TACT -= 1;
    PROC_TACT();
    if (TACT = 0) then BUTTON1.Text := 'GAME OVER';
  end
  else BUTTON1.Text := 'GAME OVER';
end;

procedure MOUSE_DOWN(x, y, mb: integer);
var
  i, j: integer;
begin
  if (x < width - 300) and (y < height - 10) and (x > width - 450) and (y > height - 60) and (TACT = 0) then
  begin
    SetPenColor(clWhite);
    SetBrushColor(clWhite);
    Rectangle(width - 298, height - 8, width - 448, height - 58);
    SetPenColor(clWhite);
    SetBrushColor(clWhite);
    Rectangle(width - 300, height - 10, width - 450, height - 60);
    SetFontSize(20);
    TextOut(width - 400, height - 50, 'GAME OVER');
    Redraw;
    BUTTON1.Redraw;
  end;
  
  if (x < 10) or (y < 10) or (x > n * CELL_WIDTH + 10) or (y > n * CELL_WIDTH + 10) then exit;
  LockDrawing;
  i := (x - 10) div CELL_WIDTH + 1;
  j := (y - 10) div CELL_WIDTH + 1;
  if (IN_ACTIVE < ACTIVE) then
  begin
    FIELD_OF_GAME[i, j] := 0;
    inc(IN_ACTIVE);
    DRAW(false);
    Redraw();
    BUTTON1.Redraw();
    exit;
  end;
  if (IN_PASSIVE < PASSIVE) then
  begin
    FIELD_OF_GAME[i, j] := 5;
    inc(IN_PASSIVE);
    DRAW(false);
    Redraw();
    BUTTON1.Redraw();
    exit;
  end;
  Redraw;
  BUTTON1.Redraw;
end;
end.
