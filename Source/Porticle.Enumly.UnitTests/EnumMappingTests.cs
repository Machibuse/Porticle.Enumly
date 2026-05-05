using Xunit;

namespace Porticle.Enumly.UnitTests;

public class EnumMappingTests
{
    [Theory]
    [InlineData(Bar.BarRed, Foo.GoldRed)]
    [InlineData(Bar.BarRose, Foo.GoldRose)]
    [InlineData(Bar.BarRoyalBlue, Foo.GoldRoyal)] // explicit override
    public void ToFoo_maps_by_name_and_explicit_overrides(Bar input, Foo expected)
    {
        Assert.Equal(expected, Mapper.ToFoo(input));
    }

    [Theory]
    [InlineData(Foo.GoldRed, Bar.BarRed)]
    [InlineData(Foo.GoldRose, Bar.BarRose)]
    [InlineData(Foo.GoldRoyal, Bar.BarRoyalBlue)] // explicit override
    public void ToBar_maps_by_name_and_explicit_overrides(Foo input, Bar expected)
    {
        Assert.Equal(expected, Mapper.ToBar(input));
    }

    [Theory]
    [InlineData(Noo.GooRose, Bar.BarRose)]
    [InlineData(Noo.GooRed, Bar.BarRed)]
    [InlineData(Noo.GooRoyal, Bar.BarRoyalBlue)] // explicit override
    public void ToNullableBar_maps_non_null_values(Noo input, Bar expected)
    {
        Assert.Equal(expected, Mapper.ToNullableBar(input));
    }

    [Fact]
    public void ToNullableBar_maps_NullSourceValue_to_null()
    {
        Assert.Null(Mapper.ToNullableBar(Noo.GooNone));
    }

    [Theory]
    [InlineData(Bar.BarRed, Noo.GooRed)]
    [InlineData(Bar.BarRose, Noo.GooRose)]
    [InlineData(Bar.BarRoyalBlue, Noo.GooRoyal)] // explicit override
    public void ToNoo_maps_non_null_input(Bar input, Noo expected)
    {
        Assert.Equal(expected, Mapper.ToNoo(input));
    }

    [Fact]
    public void ToNoo_maps_null_to_NullTargetValue()
    {
        Assert.Equal(Noo.GooNone, Mapper.ToNoo(null));
    }

    [Theory]
    [InlineData(Bar.BarRed, Foo.GoldRed)]
    [InlineData(Bar.BarRose, Foo.GoldRose)]
    [InlineData(Bar.BarRoyalBlue, Foo.GoldRoyal)] // explicit override
    public void ToNullFoo_maps_non_null_input(Bar input, Foo expected)
    {
        Assert.Equal(expected, Mapper.ToNullFoo(input));
    }

    [Fact]
    public void ToNullFoo_maps_null_to_null()
    {
        Assert.Null(Mapper.ToNullFoo(null));
    }

    [Theory]
    [InlineData(Bar.BarRed, Noo.GooRed)]
    [InlineData(Bar.BarRose, Noo.GooRose)]
    [InlineData(Bar.BarRoyalBlue, Noo.GooRoyal)] // explicit override
    public void ToNooX_maps_non_nullable_source(Bar input, Noo expected)
    {
        // NullTargetValue on a non-nullable source is allowed but unused.
        Assert.Equal(expected, Mapper.ToNooX(input));
    }

    [Theory]
    [InlineData(BarPlus.BarRed, Foo.GoldRed)]
    [InlineData(BarPlus.BarRose, Foo.GoldRose)]
    [InlineData(BarPlus.BarRoyalBlue, Foo.GoldRoyal)] // explicit override
    public void ToFooFromPlus_maps_non_ignored_values(BarPlus input, Foo expected)
    {
        Assert.Equal(expected, Mapper.ToFooFromPlus(input));
    }

    [Fact]
    public void ToFooFromPlus_throws_for_ignored_source_value()
    {
        // BarPlus.BarExtra is marked with [EnumlyIgnoreSource] — calling with it must
        // throw with the dedicated "excluded by [EnumlyIgnoreSource]" message.
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Mapper.ToFooFromPlus(BarPlus.BarExtra));
        Assert.Contains("EnumlyIgnoreSource", ex.Message);
        Assert.Contains("BarExtra", ex.Message);
    }

    [Fact]
    public void ToFooFromPlus_throws_for_unknown_source_value()
    {
        // A value cast from outside the declared range still hits the default arm
        // and reports the generic "is not supported" message — distinguishable from
        // an explicitly ignored value.
        var unknown = (BarPlus)999;
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Mapper.ToFooFromPlus(unknown));
        Assert.Contains("is not supported", ex.Message);
        Assert.DoesNotContain("EnumlyIgnoreSource", ex.Message);
    }

    [Theory]
    [InlineData(Bar.BarRed, FooPlus.GoldRed)]
    [InlineData(Bar.BarRose, FooPlus.GoldRose)]
    [InlineData(Bar.BarRoyalBlue, FooPlus.GoldRoyal)] // explicit override
    public void ToFooPlus_maps_when_extra_targets_are_ignored(Bar input, FooPlus expected)
    {
        // FooPlus has GoldExtra1/GoldExtra2 that aren't reachable from any Bar value.
        // [EnumlyIgnoreTarget] silences the EM0008 reachability warning. Runtime mapping
        // for the reachable values must continue to work.
        Assert.Equal(expected, Mapper.ToFooPlus(input));
    }

    [Theory]
    [InlineData(Bar.BarRed,        Bar.BarRed)]
    [InlineData(Bar.BarRose,       Bar.BarRose)]
    [InlineData(Bar.BarRoyalBlue,  Bar.BarRoyalBlue)]
    public void ToBarStrict_maps_non_null_input(Bar input, Bar expected)
    {
        Assert.Equal(expected, Mapper.ToBarStrict(input));
    }

    [Fact]
    public void ToBarStrict_throws_ArgumentNullException_for_null_input()
    {
        // IgnoreNullSource = true → null input throws with a dedicated message
        // identifying it as excluded by IgnoreNullSource.
        var ex = Assert.Throws<ArgumentNullException>(() => Mapper.ToBarStrict(null));
        Assert.Contains("IgnoreNullSource", ex.Message);
    }
}