
var
  a: array of double; //������� ������������
  b: array of double; //������� ��������� ������
  x: array of double; //������ �������� x
  y: array of double; //������ �������� y �� x
  k, n: integer; // k-���������� ����� ���. n - ������� ��������
  sum: array[,] of double; //����� �������� x,y ��� ����������� ������������� ��������
  flag: boolean; //���� ����������
  t: integer; //������� 

procedure RMATRIX();
begin
  for var i := 0 to n do
    for var j := 0 to n do
    begin
      sum[i, j] := 0;
      for var l := 0 to k - 1 do
        sum[i, j] += Power(x[l], i + j);
    end;
  for var i := 0 to n do
    for var j := 0 to k - 1 do
      b[i] += Power(x[j], i) * y[j];
end;

procedure DIAGONAL();
begin
  var
  temp: double;
  temp := 0;  
  for var i := 0 to n do
    if(sum[i, i] = 0) then
      for var j := 0 to n do
      begin
        if(j = i) then continue;
        if((sum[j, i] <> 0) and (sum[i, j] <> 0)) then
        begin
          for var l := 0 to n do
          begin
            temp := sum[j, l];
            sum[j, l] := sum[i, l];
            sum[i, l] := temp;          
          end;
          temp := b[j];
          b[j] := b[i];
          b[i] := temp;
          break;
        end;    
      end;
end;

procedure ROWSPROCESS();
var
  m: double;
  s: double;
begin
  for var l := 0 to n do
    for var i := l + 1 to n do
    begin
      if(sum[l, l] = 0) then
      begin
        writeln('�� ���������� �������!');
        exit();
      end;
      m := sum[i, l] / sum[l, l];
      for var j := l to n do
        sum[i, j] -= m * sum[l, j];
      b[i] -= M * b[l];
    end;  
  t := n;
  while(t >= 0) do
  begin
    s := 0;
    for var j := t to n do
      s := s + sum[t, j] * a[j];
    a[t] := (b[t] - s) / sum[t, t];
    dec(t);
  end;
end;

procedure PRINTRESULT();
begin
writeln();
writeln('���������:');  
  for var i := 0 to n do
    writeln('a',i,'=',a[i]);

writeln();
end;

begin
  flag := true;
  while(flag) do
  begin
    try
      writeln('������� ���������� ����� �������������:');
      readln(k);
      
      if(k <= 20) then flag := false
      else
      begin
        writeln('����� ������ ���� <=20.');
        writeln('��� ����������� ������� Enter...');
        readln();
      end;
    
    except
      on System.FormatException do
      begin
        writeln('������� �����!');
        writeln('��� ����������� ������� Enter...');
        readln();
      end;
    end;
  end;
  
  flag := true;
  while(flag) do
  begin
    try
      writeln('������� ������� ����������:');
      readln(n);
      
      if(n <= 3) then flag := false
      else
      begin
        writeln('����� ������ ���� <=3.');
        writeln('��� ����������� ������� Enter...');
        readln();
      end;
    
    except
      on System.FormatException do
      begin
        writeln('������� �����!');
        writeln('��� ����������� ������� Enter...');
        readln();
      end;
    end;
  end;
  
  x := new double[k];
  y := new double[k];
  sum := new double[n + 1, n + 1];
  a := new double[n + 1];
  b := new double[n + 1];
  
  for var i := 0 to k - 1 do
  begin;
    writeln('������� x', i);
    readln(x[i]);
    writeln('������� �������� y(x', i, ')');
    readln(y[i]);
  end;
  
  RMATRIX();
  
  DIAGONAL();
  
  ROWSPROCESS();
  
  PRINTRESULT();
  
  writeln('��� ������ ������� Enter...');
end.