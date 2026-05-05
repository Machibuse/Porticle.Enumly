using Xunit;

namespace ConsoleApp3;

public class EnumMappingTests
{
    [Theory]
    [InlineData(Bar.BarRed,        Foo.GoldRed)]
    [InlineData(Bar.BarRose,       Foo.GoldRose)]
    [InlineData(Bar.BarRoyalBlue,  Foo.GoldRoyal)] // explicit override
    public void ToFoo_maps_by_name_and_explicit_overrides(Bar input, Foo expected)
    {
        Assert.Equal(expected, Mapper.ToFoo(input));
    }

    [Theory]
    [InlineData(Foo.GoldRed,    Bar.BarRed)]
    [InlineData(Foo.GoldRose,   Bar.BarRose)]
    [InlineData(Foo.GoldRoyal,  Bar.BarRoyalBlue)] // explicit override
    public void ToBar_maps_by_name_and_explicit_overrides(Foo input, Bar expected)
    {
        Assert.Equal(expected, Mapper.ToBar(input));
    }

    [Theory]
    [InlineData(Noo.GooRose,  Bar.BarRose)]
    [InlineData(Noo.GooRed,   Bar.BarRed)]
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
    [InlineData(Bar.BarRed,        Noo.GooRed)]
    [InlineData(Bar.BarRose,       Noo.GooRose)]
    [InlineData(Bar.BarRoyalBlue,  Noo.GooRoyal)] // explicit override
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
    [InlineData(Bar.BarRed,        Foo.GoldRed)]
    [InlineData(Bar.BarRose,       Foo.GoldRose)]
    [InlineData(Bar.BarRoyalBlue,  Foo.GoldRoyal)] // explicit override
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
    [InlineData(Bar.BarRed,        Noo.GooRed)]
    [InlineData(Bar.BarRose,       Noo.GooRose)]
    [InlineData(Bar.BarRoyalBlue,  Noo.GooRoyal)] // explicit override
    public void ToNooX_maps_non_nullable_source(Bar input, Noo expected)
    {
        // NullTargetValue on a non-nullable source is allowed but unused.
        Assert.Equal(expected, Mapper.ToNooX(input));
    }
}
