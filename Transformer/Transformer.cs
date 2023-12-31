using MathNet.Numerics.LinearAlgebra.Double;
public class Transformer {
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
        for (int k = 0; k <cartesian.Length; k++) 
            if(double.IsNaN(cartesian[k]))
                cartesian[k] = 0;

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
        theta = theta==0 ? 0 : 2 * Math.Atan2(1.0, (cartesian[n-1] + Math.Sqrt(cartesian[n-1] * cartesian[n-1] + theta * theta)) / theta);
        hyperspherical[n] = theta;
        for (int k = 0; k <hyperspherical.Length; k++) 
            if(double.IsNaN(hyperspherical[k]))
                hyperspherical[k] = 0;

        return hyperspherical;
    }
    
    public static double[] CartesianToSpherical(double[] cartesian){
        var sum = cartesian[0]*cartesian[0];
        var alpha = new double[cartesian.Length];
        for(int i = 0;i<cartesian.Length-1;i++){
            var nextSum = sum+cartesian[i+1]*cartesian[i+1];            
            alpha[i+1]=Math.Acos(Math.Sqrt(sum/nextSum));
            sum = nextSum;
        }
        alpha[0]=Math.Sqrt(sum);
        return alpha;
    }
    public static double[] SphericalToCartesian(double[] spherical){
        var cartesian = new double[spherical.Length];
        var cosProduct = spherical[0];
        for(int i = spherical.Length-2;i>=0;i--){
            var prevAlpha = i>=1 ? Math.Sin(spherical[i]) : 1;
            cosProduct*=Math.Cos(spherical[i+1]);
            cartesian[i]=cosProduct*prevAlpha;
        }
        cartesian[^1]=spherical[0]*Math.Sin(spherical[^1]);
        
        return cartesian;
    }

    public static void Main(string[] args) {
        
       
        
    }
}