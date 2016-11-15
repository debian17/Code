{-2,-1,0,1,2 - �������� �������� ��� ������
5 - ������
7- �����}
program laba3;

uses GraphABC, Graphics,Logic,Mouse;
const
  step = 60; //������ ������ �������� ����
  width = 800; //������ �������� ����
  height = 600;//������ �������� ����

var
  n: integer;//������ �������� ����
  active_input: integer;//���������� ������� ��� ��������� �� ����
  passive_input: integer;//���������� �������� ��� ��������� �� ����
  active: integer;//���������� �������� ��������
  passive: integer;//���������� ��������� ��������
  takt_input: integer;
  takt: integer; //���������� ������
  
  game_field: array [1..20, 1..20] of integer;//������� ����
  prev_field: array [1..20, 1..20] of integer;//����� ����, ��� ���������� ����������� ����


//����� �����
begin
  SetWindowCaption('������������ ������ �2');
  SetWindowPos(500, 100);
  SetWindowWidth(width);
  SetWindowHeight(height);
  active_input := 0;
  passive_input := 0;
  write('������ ����:  ');
  readln(n);
  writeln(n);
  for var i := 1 to n do
    for var j := 1 to n do  game_field[i, j] := 7;
  CopyOfArray();
  Write('������� �������� ��������? ');
  ReadLN(active);
  writeln(active);
  Write('������� ��������� ��������? ');
  ReadLN(passive);
  writeln(passive);
  write('���������� �����: ');
  readln(takt_input);
  writeln(takt_input);
  takt := takt_input;
  ClearWindow();
  Draw(false); 
  OnMouseDown := MouseDown;
end.