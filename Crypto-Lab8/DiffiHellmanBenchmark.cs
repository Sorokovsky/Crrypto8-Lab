using System.Globalization;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using Core;

namespace Crypto_Lab8;

public class DiffiHellmanBenchmark
{
    private static readonly BigInteger P = BigInteger.Parse(
        "195750774265396599882749000924414228731955964609671916646677257653727766812799158651755114425553249428857217101390662338206656504678607661514217506930747116108369561982355127672272713933882939471707053718171355134504715927179233327574482257344838903986306228098517039201843378267706216011752085886284131389553473341027433763981688523306132232478191034294371307424807950105865225053532358778317228467708449037236468407383865133024763044532780532770857928620706586061356416647106540508297691298418169440498049039817715520839372080720843983480771450574421192959009854800638421739853682800569202093684441302150234763264831233713828954770493509964464336290769609359275779562424772689749820554876495406952613775396348620729209345235707506888185725692173363398125679650345183391356660456746641034407102614500636556919345564110350815469076587172977717300117387380116117008563929420432744888554985784266020086292730366496945767003098457");

    private static readonly BigInteger EcP =
        BigInteger.Parse("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFED", NumberStyles.HexNumber);

    private static readonly BigInteger EcA = 486662;
    private static readonly BigInteger EcB = 1;
    private static readonly BigInteger EcGx = BigInteger.Zero;
    private static readonly BigInteger EcGy = BigInteger.One;

    private readonly EllipticCurveDiffieHellman _ellipticCurveDiffieHellman =
        new(new EllipticCurve(EcGx, EcGy, EcA, EcB, EcP));

    private readonly StandardDiffieHellman _standardDiffieHellman = new(P, 2, new PrimeBigIntegerGenerator(P));

    [Benchmark(Baseline = true)]
    public void StandardGeneratePair()
    {
        try
        {
            _standardDiffieHellman.GeneratePair();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    [Benchmark]
    public void EllipticGeneratePair()
    {
        try
        {
            _ellipticCurveDiffieHellman.GeneratePair();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}