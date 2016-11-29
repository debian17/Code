{-2,-1,0,1,2 - �������� �������� ��� ������
5 - ������
7- �����}
program laba3;

uses GraphABC, Graphics, Logic, Mouse;
const
  CELL_WIDTH = 50; //������ ������ �������� ����
  WIDTH = 900; //������ �������� ����
  HEIGHT = 900;//������ �������� ����

var
  FIELD_SIZE: integer;//������ �������� ����
  IN_ACTIVE: integer;//���������� ������� ��� ��������� �� ����
  IN_PASSIVE: integer;//���������� �������� ��� ��������� �� ����
  ACTIVE: integer;//���������� �������� ��������
  PASSIVE: integer;//���������� ��������� ��������
  IN_TAKT: integer;
  TAKT: integer; //���������� ������
  BATMAN : Picture;
  SUPERMAN : Picture;
  FIELD_GAME: array[1..20, 1..20] of integer;//������� ����
  FIELD_GAME_PREV: array[1..20, 1..20] of integer;//����� ����, ��� ���������� ����������� ����

//����� �����
begin
  SetWindowCaption('������������ ������ �2');
  SetWindowPos(500, 10);
  SetWindowWIDTH(WIDTH);
  SetWindowHEIGHT(HEIGHT);
  IN_ACTIVE := 0;
  IN_PASSIVE := 0;
  SetFontSize(14);
  write('������� ������ ����:  ');
  readln(FIELD_SIZE);
  writeln(FIELD_SIZE);
  for var i := 1 to FIELD_SIZE do
    for var j := 1 to FIELD_SIZE do
      FIELD_GAME[i, j] := 7;
  ARRAY_CLONE();
  Write('������� ������� �������� ��������:');
  ReadLN(ACTIVE);
  writeln(ACTIVE);
  Write('������� ������� ��������� ��������:');
  ReadLN(PASSIVE);
  writeln(PASSIVE);
  write('������� ���������� �����: ');
  readln(IN_TAKT);
  writeln(IN_TAKT);
  TAKT := IN_TAKT;
  ClearWindow();
  DRAW(false);
  OnMouseDown := MOUSE_CLICK;
end.