using System.Diagnostics.Contracts;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Tests;

public class EnsureTransformation{
    [Fact]
    void CartesianToHyperspherical()
    {
        var cartesian =
            Enumerable.Range(0, 100)
            .Select(x => Random.Shared.NextDouble() * Random.Shared.Next(5))
            .ToArray();

        var hyperspherical = Transformer.CartesianToHyperspherical(cartesian);
        var cartesianReverse = Transformer.HypersphericalToCartesian(hyperspherical);

        var error = cartesianReverse.Zip(cartesian).Sum(x => Math.Abs(x.First - x.Second));
        Assert.True(error<1e-10);
    }
    [Fact]
    void HypersphericalToCartesian()
    {
        var hyperspherical = Enumerable.Range(0, 100)
            .Select(x => Random.Shared.NextDouble() * Math.PI * 2)
            .ToArray();

        var cartesian = Transformer.HypersphericalToCartesian(hyperspherical);
        var hypersphericalReverse = Transformer.CartesianToHyperspherical(cartesian);
        var cartesianReverse = Transformer.HypersphericalToCartesian(hypersphericalReverse);

        var error = cartesianReverse.Zip(cartesian).Sum(x => Math.Abs(x.First - x.Second));
        Assert.True(error<1e-10);
    }

    [Fact]
    void MultipleTransformations()
    {
        var cartesian = Enumerable.Range(0, 100)
            .Select(x => Random.Shared.NextDouble() * Math.PI * 2)
            .ToArray();

        var hyperspherical1 = Transformer.CartesianToHyperspherical(cartesian);
        var cartesian1 = Transformer.HypersphericalToCartesian(hyperspherical1);
        var hyperspherical2 = Transformer.CartesianToHyperspherical(cartesian1);
        var cartesian2 = Transformer.HypersphericalToCartesian(hyperspherical2);

        var error1 = cartesian.Zip(cartesian1).Sum(x => Math.Abs(x.First - x.Second));
        var error2 = hyperspherical1.Zip(hyperspherical2).Sum(x => Math.Abs(x.First - x.Second));
        var error3 = cartesian.Zip(cartesian2).Sum(x => Math.Abs(x.First - x.Second));

        Assert.True(error1<1e-10);
        Assert.True(error2<1e-10);
        Assert.True(error3<1e-10);

    }
}