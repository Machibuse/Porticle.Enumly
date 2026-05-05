using Porticle.Enumly;

namespace ConsoleApp3;

[EnumlyClass]
public static partial class Mapper
{
    /// <summary>
    /// Simple Mapping 
    /// </summary>
    [EnumlyMap]
    [EnumlyMapValue(Bar.BarRoyalBlue, Foo.GoldRoyal)]
    public static partial Foo ToFoo(Bar value);

    /// <summary>
    /// Simple Mapping reversed 
    /// </summary>
    [EnumlyMap]
    [EnumlyMapValue(Foo.GoldRoyal, Bar.BarRoyalBlue)]
    public static partial Bar ToBar(Foo value);

    
    /// <summary>
    /// Case A: Noo → Bar?  (source enum, target nullable enum) 
    /// </summary>
    [EnumlyMap(NullSourceValue = Noo.GooNone)]
    [EnumlyMapValue(Noo.GooRoyal, Bar.BarRoyalBlue)]
    public static partial Bar? ToNullableBar(Noo value);

    
    /// <summary>
    /// Case B: Bar? → Noo  (nullable source, non-nullable target) 
    /// </summary>
    [EnumlyMap(NullTargetValue = Noo.GooNone)]
    [EnumlyMapValue(Bar.BarRoyalBlue, Noo.GooRoyal)]
    public static partial Noo ToNoo(Bar? value);

    
    /// <summary>
    /// Case C: Bar? → Foo?  (nullable → nullable, null → null automatisch) 
    /// </summary>
    [EnumlyMap]
    [EnumlyMapValue(Bar.BarRoyalBlue, Foo.GoldRoyal)]
    public static partial Foo? ToNullFoo(Bar? value);

    /// <summary>
    /// Case D: Bar → Noo  (NullTargetValue ist hier ungenutzt — kein Fehler)
    /// </summary>
    [EnumlyMap(NullTargetValue = Noo.GooNone)]
    [EnumlyMapValue(Bar.BarRoyalBlue, Noo.GooRoyal)]
    public static partial Noo ToNooX(Bar value);

    /// <summary>
    /// Case E: BarPlus → Foo with [EnumlyIgnoreSource] for BarPlus.BarExtra.
    /// BarExtra has no matching target. Without [EnumlyIgnoreSource] this would fail with EM0001.
    /// At runtime, calling with BarPlus.BarExtra throws ArgumentOutOfRangeException with a
    /// dedicated "excluded by [EnumlyIgnoreSource]" message.
    /// </summary>
    [EnumlyMap]
    [EnumlyMapValue(BarPlus.BarRoyalBlue, Foo.GoldRoyal)]
    [EnumlyIgnoreSource(BarPlus.BarExtra)]
    public static partial Foo ToFooFromPlus(BarPlus value);

    /// <summary>
    /// Case F: Bar → FooPlus with [EnumlyIgnoreTarget] for the extra target values.
    /// FooPlus has GoldExtra1/GoldExtra2 that cannot be reached from any Bar value.
    /// Without [EnumlyIgnoreTarget] this would fire EM0008 (warning).
    /// </summary>
    [EnumlyMap]
    [EnumlyMapValue(Bar.BarRoyalBlue, FooPlus.GoldRoyal)]
    [EnumlyIgnoreTarget(FooPlus.GoldExtra1)]
    [EnumlyIgnoreTarget(FooPlus.GoldExtra2)]
    public static partial FooPlus ToFooPlus(Bar value);
}