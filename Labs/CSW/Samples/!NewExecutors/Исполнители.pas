///- ������ �����������
/// ��������� ��� ������������, ������� ����� ��������� �������
unit �����������;

///- ��������� �����(��������)
/// ������� ������ ��������
procedure �������(params args: array of object) := Println(args);
/// ������� ������ ������
procedure ������������������� := Println;

///- ����� �������
procedure integer.������� := Println(Self);
procedure real.������� := Println(Self);
procedure string.������� := Println(Self);


type
  ����� = integer;
  ������������ = real;
  ���������� = boolean;
  ��������� = string;
  
  �������� = interface
    procedure ������(params args: array of object);
    procedure ������������;
  end;

  ���������� = class(��������)
  public
    procedure ������(params args: array of object) := Println(args);
    procedure ������������ := Println;
  end;
  
  ������������ = interface
    ///- ����� ���������.��������(�������: �����)
    /// ��������� ������� � ���������. ���� ����� ������� ����, �� ������ �� ����������
    procedure ��������(�������: �����);
    ///- ����� ���������.�������(�������: �����)
    /// ������� ������� �� ���������. ���� ������ �������� ���, �� ������ �� ����������
    procedure �������(�������: �����);
    ///- ����� ���������.�������
    /// ������� �������� ���������
    procedure �������;
    ///- ����� ���������.��������(�������: �����): ����������
    /// ���������, ���� �� ������� �� ���������
    function ��������(�������: �����): ����������;
    ///- ����� ���������.����������������
    /// ������� ��� ������ ����������� ���������
    procedure ����������������;
    ///- ����� ���������.��������
    /// ������ ��������� ������
    procedure ��������;
    ///- ����� ���������.�����
    /// ������� ����� ���������
    function �����: ������������;
  end;
  
  ����������������� = interface
    ///- ����� ���������.��������(�������: ���������)
    /// ��������� ������� � ���������. ���� ����� ������� ����, �� ������ �� ����������
    procedure ��������(�������: string);
    ///- ����� ���������.�������(�������: ���������)
    /// ������� ������� �� ���������. ���� ������ �������� ���, �� ������ �� ����������
    procedure �������(�������: string);
    ///- ����� ���������.�������
    /// ������� �������� ���������
    procedure �������;
    ///- ����� ���������.��������(�������: ���������): ����������
    /// ���������, ���� �� ������� �� ���������
    function ��������(�������: string): ����������;
    ///- ����� ���������.����������������
    /// ������� ��� ������ ����������� ���������
    procedure ����������������;
    ///- ����� ���������.��������
    /// ������ ��������� ������
    procedure ��������;
    ///- ����� ���������.�����
    /// ������� ����� ���������
    function �����: �����������������;
    ///- ����� ���������.�����������(���������1)
    /// ������� ���� ��������� �� ���� ��������
    function �����������(��1: �����������������): �����������������;
  end;

  �������������� = interface
  ///- ����� �����������.�������������������(a,b,c: ������������)
  /// ������� ��� ������� ����������� ���������
    procedure �������������������������(a,b,c: ������������);
  ///- ����� �����������.������������������������(a0,d: �����)
  /// ������� �������������� ����������
    procedure �������������������������������(a0,d: integer);
    procedure ����������������;
  end;
  
  ������� = interface
    procedure ���������������(���: ���������);
    procedure ���������������(���: ���������);
    procedure �������;
    procedure ��������(������: ���������);
    function ���������������: ���������;
    function ��������������: �����;
    function ����������: ����������;
    function ��������: ���������;
    procedure �����������������(��������: ���������);
    procedure ����������������;
    function �����: �������;
    function ���������(��������: ���������): sequence of ���������;
  end;
  
  ��������������������� = interface
    
  end;


function DeleteEnd(Self: string; s: string): string; extensionmethod;
begin
  if Self.EndsWith(s) then
  begin
    var i := Self.LastIndexOf(s);
    if (i>=0) and (i<Self.Length) then
      Result := Self.Remove(i)
    else Result := Self;  
  end
  else Result := Self;
end;
 
Procedure PrintAllMethods(o: Object);
begin
  WritelnFormat('������ ����������� {0}:',o.GetType.Name.DeleteEnd('�����'));
  o.GetType.GetMethods(System.Reflection.BindingFlags.Public or
            System.Reflection.BindingFlags.Instance or 
            System.Reflection.BindingFlags.DeclaredOnly)
    .Select(s->s.ToString.Replace('Void ','')
    .Replace('Int32','�����')
    .Replace('Boolean','����������')
    .Replace('System.String','���������')
    .Replace('Double','������������'))
    .Select(s->'  '+s.DeleteEnd('()'))
    .Where(s->not s.ToString.Contains('$Init$'))
    .Println(NewLine);
end;

// ����������
type
  �������������� = class(������������)
    s := new SortedSet<integer>;
  public
    constructor;
    begin end;
    procedure ��������(�������: �����);
    begin
      s.Add(�������);
    end;
    procedure �������(�������: �����);
    begin
      s.Remove(�������);
    end;
    procedure �������;
    begin
      s.Println;
    end;
    function ��������(�������: �����): ����������;
    begin
      Result := s.Contains(�������)
    end;
    procedure ����������������;
    begin
      if Random(2)=1 then
        PrintAllMethods(Self)
      else 
      begin
        WritelnFormat('������ ����������� {0}:',Self.GetType.Name.DeleteEnd('�����'));
        Writeln('  ��������(�������: �����)');
        Writeln('  �������(�������: �����)');
        Writeln('  �������');
        Writeln('  ��������(�������: �����): ����������');
        Writeln('  ����������������');
      end;
    end;
    procedure ��������;
    begin
      s.Clear
    end;
    function �����: ������������;
    begin
      Result := new ��������������();
    end;
  end;
  
type
  ������������������� = class(�����������������)
    s := new SortedSet<string>;
  public
    constructor;
    begin end;
    procedure ��������(�������: string);
    begin
      s.Add(�������);
    end;
    procedure �������(�������: string);
    begin
      s.Remove(�������);
    end;
    procedure �������;
    begin
      s.Println;
    end;
    function ��������(�������: string): ����������;
    begin
      Result := s.Contains(�������)
    end;
    procedure ����������������;
    begin
      if Random(2)=1 then
        PrintAllMethods(Self)
      else 
      begin
        WritelnFormat('������ ����������� {0}:',Self.GetType.Name.DeleteEnd('�����'));
        Writeln('  ��������(�������: �����)');
        Writeln('  �������(�������: �����)');
        Writeln('  �������');
        Writeln('  ��������(�������: �����): ����������');
        Writeln('  ����������������');
      end;
    end;
    procedure ��������;
    begin
      s.Clear
    end;
    function �����: �����������������;
    begin
      Result := new �������������������
    end;
    function �����������(��1: �����������������): �����������������;
    begin
      var ss: SortedSet<string>;
      ss := (��1 as �������������������).s;
      
      var m := new �������������������;
      m.s := s.ZipTuple(ss).Select(x -> x.ToString()).ToSortedSet;
      Result := m
    end;
  end;
  
type
  ���������������� = class(��������������)
  public
    procedure �������������������������(a,b,c: real);
    begin
      writelnFormat('���������� ���������: {0}*x*x+{1}*x+{2}=0',a,b,c);
      var D := b*b-4*a*c;
      if D<0 then
        writeln('������� ���')
      else
      begin
        var x1 := (-b-sqrt(D))/2/a;
        var x2 := (-b+sqrt(D))/2/a;
        writelnFormat('�������: x1={0} x2={1}',x1,x2)
      end;
    end;
    procedure �������������������������������(a0,d: integer);
    begin
      writelnFormat('�������������� ����������: a0={0} d={1}',a0,d);
      SeqGen(10,a0,x->x+d).Println; // ! ������ ���� ��������� ����������� �����
    end;
    procedure ����������������;
    begin
      PrintAllMethods(Self);
    end;
  end;

  FileState = (Closed,OpenedForRead,OpenedForWrite);
  ��������� = class(�������)
    f: Text;
    State := FileState.Closed;
  public
    constructor ;
    begin
    end;
    procedure ���������������(���: ���������);
    begin
      if State<>FileState.Closed then
        f.Close;
      f := OpenRead(���);
      State := FileState.OpenedForRead
    end;
    procedure ���������������(���: ���������);
    begin
      if State<>FileState.Closed then
        f.Close;
      f := OpenWrite(���);
      State := FileState.OpenedForWrite
    end;
    procedure �������;
    begin
      if State=FileState.Closed then
        Println('������: ���� ��� ������')
      else f.Close;
      State := FileState.Closed;
    end;
    procedure ��������(������: ���������);
    begin
      if State=FileState.Closed then
        Println('������: ����� ������� ���� ������� �������')
      else f.Writeln(������)
    end;
    function ���������������: ���������;
    begin
      if State=FileState.Closed then
      begin
        Println('������: ����� ������� ���� ������� �������');
        Result := '';
      end
      else 
      begin
        Result := f.ReadlnString;
        Println(Result);
      end;  
    end;
    function ��������������: �����;
    begin
      if State=FileState.Closed then
      begin
        Println('������: ����� ������� ���� ���� �������');
        Result := 0;
      end
      else 
      begin
        Result := f.ReadInteger;
        Print(Result);
      end;  
    end;
    function ����������: ����������;
    begin
      Result := f.Eof;
    end;
    function ��������: ���������;
    begin
      if State=FileState.Closed then
      begin
        Println('������: ���� ������, ������� �� �� ����� �����');
        Result := '';
      end
      else 
      begin
        Println('��� �����: ',f.Name);
        Result := f.Name;
      end
    end;
    procedure �����������������(��������: ���������);
    begin
      if (State<>FileState.Closed) and (f.Name.ToLower=��������.ToLower) then
        Println('������: ���������� ����� ������� ������ � ��������� �����')
      else 
      begin
        WritelnFormat('���������� ����� {0}:',��������);
        try
          ReadLines(��������).Println(NewLine);
        except
          WritelnFormat('���� {0}: ����������� �� �����',��������);
        end;
      end;  
        
    end;
    procedure ����������������;
    begin
      PrintAllMethods(Self);
    end;
    function �����: �������;
    begin
      Result := new ���������
    end;
    function ���������(��������: ���������): sequence of ���������;
    begin
      if (State<>FileState.Closed) and (f.Name.ToLower=��������.ToLower) then
      begin
        Println('������: ������ ����� �������� ������ � ��������� �����');
        Result := nil;
        exit;
      end;
      Result := ReadLines(��������).ToArray;
    end;
  end;

const dbname = 'countries.db';

var coun: array of string := nil;

function ������������: sequence of string;
begin
  if coun = nil then
    coun := ReadLines(dbname).ToArray();
  Result := coun;  
end;

function �������<T>(Self: sequence of T): sequence of T; extensionmethod;
begin
  Self.Println
end;

function ����������������<T>(Self: sequence of T): sequence of T; extensionmethod;
begin
  Self.Println(NewLine)
end;

function �������<T>(Self: sequence of T; cond: T -> boolean): sequence of T; extensionmethod;
begin
  Result := Self.Where(cond);  
end;

function �����<T>(Self: sequence of T; n: integer): sequence of T; extensionmethod;
begin
  Result := Self.Take(n);  
end;

function ����������<T>(Self: sequence of T; cond: T -> boolean): integer; extensionmethod;
begin
  Result := Self.Count(cond)
end;

function �����(Self: sequence of integer): integer; extensionmethod := Self.Sum();  

function �������������<T,Key>(Self: sequence of T; conv: T -> Key): sequence of Key; extensionmethod;
begin
  Result := Self.Select(conv);  
end;

function ���������������<T,Key>(Self: sequence of T; cond: T -> Key): sequence of T; extensionmethod;
begin
  Result := Self.OrderBy(cond);  
end;

function �����������������������<T,Key>(Self: sequence of T; cond: T -> Key): sequence of T; extensionmethod;
begin
  Result := Self.OrderByDescending(cond);  
end;

procedure �������<T>(Self: sequence of T; act: T -> ()); extensionmethod;
begin
  Self.Foreach(act);  
end;

function �������(c: char): string -> boolean;
begin
  Result := ������ -> ������[1] = c;
end;

function �������(s: string): string -> boolean;
begin
  Result := ������ -> ������[1] = s[1];
end;

function ������������(Self: string; s: string): boolean; extensionmethod;
begin
  Result := Self.StartsWith(s);  
end;


type ������������������� = class
public
  ���������: ������������;
  �����������: ��������������;
  ����: �������;
  �����: ��������;
  procedure �������;
  begin
    Println('���������');
    Println('�����������');
    Println('����');
    Println('�����');
  end;
end;

type 
  Country = auto class
    nm,cap: string;
    inh: integer;
    cont: string;
  public  
    property ��������: string read nm;
    property �������: string read cap;
    property ���������: integer read inh;
    property ���������: string read cont;
  end;
  
var ������: sequence of Country;  

procedure InitCountries();
begin
  ������ := ReadLines('������.csv')
    .Select(s->s.ToWords(';'))
    .Select(w->new Country(w[0],w[1],w[2].ToInteger,w[3])).ToArray;
end;

// ������������������

function ������������������������(a,d: integer; n: integer := 20): sequence of integer;
begin
  Result := SeqGen(n,a,a->a+d)
end;

function ������������������������(a,d: real; n: integer := 20): sequence of real;
begin
  Result := SeqGen(n,a,a->a+d)
end;

function �����������������������(a,d: integer; n: integer := 10): sequence of integer;
begin
  Result := SeqGen(n,a,a->a*d)
end;

function �����������������������(a,d: real; n: integer := 10): sequence of real;
begin
  Result := SeqGen(n,a,a->a*d)
end;

function ���������: �������;
begin
  Result := new ���������;
end;

var 
  ///- ��������� - ��� ����� ��������
  ��������������������: ������������ := new ��������������;
  ///- ��������� - ��� ����� ��������
  ���������: ������������ := new ��������������;
  ///- ��������� - ��� ����� ��������
  ���������1: ������������ := new ��������������;
  ///- ��������� - ��� ����� ��������
  ��������������: ����������������� := new �������������������;
  ///- ����������� - �����������, ������������ �������������� ����������
  �����������: �������������� := new ����������������;
  ///- ���� - �����������, ����������� �� � ������������ � ���� �� �����
  ����: ������� := new ���������;
  ///- ����� - �����������, ��������� ������
  �����: �������� := new ����������;
  ///- ��������������
  �������������� := new �������������������;
begin  
  ��������������.��������� := ���������;
  ��������������.����������� := �����������;
  ��������������.���� := ����;
  ��������������.����� := �����;
  InitCountries;
end.  
  