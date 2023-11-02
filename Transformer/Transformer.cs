
public class HypersphericalCoordinatesTransformer {
    /// <summary>
    /// For valid hyperspherical coordinates it uniquely maps hyperspherical coordinates to cartesian
    /// </summary>
    public static double[] HypersphericalToCartesian(double[] hyperspherical) {
        int dimension = hyperspherical.Length;
        double[] cartesian = new double[dimension];
        double r = hyperspherical[0];

        if (r == 0.0) {
            // All cartesian coordinates are zero at the origin
            return cartesian;
        }

        double product = r;
        double theta = hyperspherical[1];

        for (int i = 1; i <= dimension - 2; i++) {
            product *= Math.Sin(theta);
            theta = hyperspherical[i+1];
            cartesian[i] = product * Math.Cos(theta);
        }

        theta = hyperspherical[1];
        cartesian[0] = r * Math.Cos(theta);

        theta = hyperspherical[dimension-1];
        cartesian[dimension - 1] = product * Math.Sin(theta);

        return cartesian;
    }
    /// <summary>
    /// For cartesian coordinates exists several hyperspherical mappings, so you can map one hyperspherical
    /// coordinate to cartesian, but reverse of resulting cartesian coordinate back to hyperspherical may not be the same
    /// </summary>
    public static double[] CartesianToHyperspherical(double[] cartesian) {
        int dimension = cartesian.Length;
        double[] hyperspherical = new double[dimension];
        double r = Math.Sqrt(cartesian.Sum(x=>x*x));
        hyperspherical[0] = r;

        if (r == 0.0) {
            // All angular coordinates are zero at the origin
            return hyperspherical;
        }

        double theta;
        int n = dimension - 1;
        double current = cartesian[n];
        double sum = current * current;

        for (int k = n - 1; k >= 1; k--) {
            current = cartesian[k];
            sum += current * current;
            hyperspherical[k] = Math.Atan2(1.0, cartesian[k-1] / Math.Sqrt(sum));
        }

        theta = cartesian[n];
        theta = 2 * Math.Atan2(1.0, (cartesian[n-1] + Math.Sqrt(cartesian[n-1] * cartesian[n-1] + theta * theta)) / theta);
        hyperspherical[n] = theta;

        return hyperspherical;
    }

    public static void Main(string[] args) {
        // Example usage
        double[] hyperspherical = {1.0, Math.PI / 4.0, Math.PI / 6.0};
        double[] cartesian = HypersphericalToCartesian(hyperspherical);
        Console.WriteLine("Cartesian Coordinates: [{0}]", string.Join(", ", cartesian));

        double[] convertedHyperspherical = CartesianToHyperspherical(cartesian);
        Console.WriteLine("Hyperspherical Coordinates: [{0}]", string.Join(", ", convertedHyperspherical));
    }
}