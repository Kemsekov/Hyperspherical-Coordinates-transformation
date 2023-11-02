namespace Tests;

public class EnsureTransformation
{
    [Fact]
    void CartesianToHyperspherical()
    {
        var cartesian =
            Enumerable.Range(0, 100)
            .Select(x => Random.Shared.NextDouble() * Random.Shared.Next(5))
            .ToArray();

        var hyperspherical = HypersphericalCoordinatesTransformer.CartesianToHyperspherical(cartesian);
        var cartesianReverse = HypersphericalCoordinatesTransformer.HypersphericalToCartesian(hyperspherical);

        var error = cartesianReverse.Zip(cartesian).Sum(x => Math.Abs(x.First - x.Second));
        Assert.True(error<1e-10);
    }
    [Fact]
    void HypersphericalToCartesian()
    {
        var hyperspherical = Enumerable.Range(0, 100)
            .Select(x => Random.Shared.NextDouble() * Math.PI * 2)
            .ToArray();

        var cartesian = HypersphericalCoordinatesTransformer.HypersphericalToCartesian(hyperspherical);
        var hypersphericalReverse = HypersphericalCoordinatesTransformer.CartesianToHyperspherical(cartesian);
        var cartesianReverse = HypersphericalCoordinatesTransformer.HypersphericalToCartesian(hypersphericalReverse);

        var error = cartesianReverse.Zip(cartesian).Sum(x => Math.Abs(x.First - x.Second));
        Assert.True(error<1e-10);
    }

    [Fact]
    void MultipleTransformations()
    {
        var cartesian = Enumerable.Range(0, 100)
            .Select(x => Random.Shared.NextDouble() * Math.PI * 2)
            .ToArray();

        var hyperspherical1 = HypersphericalCoordinatesTransformer.CartesianToHyperspherical(cartesian);
        var cartesian1 = HypersphericalCoordinatesTransformer.HypersphericalToCartesian(hyperspherical1);
        var hyperspherical2 = HypersphericalCoordinatesTransformer.CartesianToHyperspherical(cartesian1);
        var cartesian2 = HypersphericalCoordinatesTransformer.HypersphericalToCartesian(hyperspherical2);

        var error1 = cartesian.Zip(cartesian1).Sum(x => Math.Abs(x.First - x.Second));
        var error2 = hyperspherical1.Zip(hyperspherical2).Sum(x => Math.Abs(x.First - x.Second));
        var error3 = cartesian.Zip(cartesian2).Sum(x => Math.Abs(x.First - x.Second));

        Assert.True(error1<1e-10);
        Assert.True(error2<1e-10);
        Assert.True(error3<1e-10);

    }
}