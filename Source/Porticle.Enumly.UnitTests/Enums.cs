namespace ConsoleApp3;

public enum Foo
{
    GoldRed,
    GoldRose,
    GoldRoyal,
}

public enum Bar
{
    BarRed,
    BarRose,
    BarRoyalBlue,
}

public enum Noo
{
    GooNone,
    GooRose,
    GooRed,
    GooRoyal,
}

// Source enum with an extra value used to test [EnumlyIgnoreSource].
// BarExtra has no counterpart in Foo and would normally trigger EM0001.
public enum BarPlus
{
    BarRed,
    BarRose,
    BarRoyalBlue,
    BarExtra,
}

// Target enum with extra values used to test [EnumlyIgnoreTarget].
// GoldExtra1/GoldExtra2 cannot be reached from Bar by name and would
// normally trigger the EM0008 reachability warning.
public enum FooPlus
{
    GoldRed,
    GoldRose,
    GoldRoyal,
    GoldExtra1,
    GoldExtra2,
}
