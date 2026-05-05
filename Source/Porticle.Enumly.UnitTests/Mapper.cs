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
}