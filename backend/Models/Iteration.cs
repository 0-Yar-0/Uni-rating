public class Iteration
{
    public int Id { get; set; }
    public ClassName ClassName;
    public Names Names;
}

public class ClassName
{
    public Dictionary<int, YearData> Year { get; set; }
}


public class YearData
{
    public B11 B11 { get; set; }
    public B12 B12 { get; set; }
    public B13 B13 { get; set; }
    public B21 B21 { get; set; }
}

public class B11
{
    public double ENa { get; set; }
    public double ENb { get; set; }
    public double ENc { get; set; }
    public double Eb { get; set; }
    public double Ec { get; set; }
    public double Result { get; set; }
}

public class B12
{
    public double Beta121 { get; set; }
    public double Beta122 { get; set; }
    public double Result { get; set; }
}

public class B13
{
    public double Beta131 { get; set; }
    public double Beta132 { get; set; }
    public double Result { get; set; }
}

public class B21
{
    public double Beta211 { get; set; }
    public double Beta212 { get; set; }
    public double Result { get; set; }
}

public class Names
{
    public Classes Classes { get; set; }
}

public class Classes
{
    public ClassInfo A { get; set; }
    public ClassInfo B { get; set; }
    public ClassInfo C { get; set; }
}

public class ClassInfo
{
    public string Name { get; set; }
    public string B11 { get; set; }
    public string B12 { get; set; }
    public string B13 { get; set; }
    public string B21 { get; set; }
}