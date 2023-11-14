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

        return hyperspherical;
    }

    public static void Main(string[] args) {
        
        var mat = DenseMatrix.Create(3,3,(_,_)=>Random.Shared.NextDouble()*Random.Shared.Next(10)-4);
        var svd = mat.Svd();
        var U = svd.U;
        System.Console.WriteLine(U);

        var angle1 = Random.Shared.NextDouble();
        var angle2 =Math.PI/3;
        var angle3 = Random.Shared.NextDouble();
        for(int i = 0;i<3;i++){
            var e = DenseVector.Create(3,0);
            e[i]=1;
            var eHy = CartesianToHyperspherical(e.ToArray());
            // eHy[0]+=angle1;
            eHy[1]+=angle2;
            // eHy[2]+=angle3;

            var newE = HypersphericalToCartesian(eHy);
            System.Console.WriteLine($"({newE[0]:0.000},{newE[1]:0.000},{newE[2]:0.000})");
        }

        // for(int i = 0;i<5;i++){
        //     var randV = DenseVector.Create(U.RowCount,_=>Random.Shared.NextDouble()*4-2);
        //     randV/=randV.L2Norm();

        //     var rotated = U*randV;
        //     var r1 = DenseVector.OfArray(CartesianToHyperspherical(randV.ToArray()));
        //     var r2 = DenseVector.OfArray(CartesianToHyperspherical(rotated.ToArray()));
        //     System.Console.WriteLine(r1);
        //     System.Console.WriteLine(r2);
        //     System.Console.WriteLine("-------------------");
        //     var diff = (r1-r2).PointwiseAbs();
        //     diff.MapInplace(i=>(i+Math.PI)%Math.PI);
        //     // System.Console.WriteLine(diff);
        // }

        // for(int i = 0;i<U.ColumnCount;i++){
        //     var basis = DenseVector.Create(U.RowCount,0);
        //     basis[i] = 1;
        //     var rotation = U.Column(i);

        //     var basisSphere = DenseVector.OfArray(CartesianToHyperspherical(basis.ToArray()));
        //     var rotationSphere = DenseVector.OfArray(CartesianToHyperspherical(rotation.ToArray()));

        //     System.Console.WriteLine(basisSphere-rotationSphere);
        // }
        // Example usage
        
    }
}