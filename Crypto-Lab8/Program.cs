using System.Globalization;
using System.Numerics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using Core;

namespace Crypto_Lab8;

public static class Program
{
    private static readonly BigInteger EcP =
        BigInteger.Parse("7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFED", NumberStyles.HexNumber);

    private static readonly BigInteger EcA = 486662;
    private static readonly BigInteger EcB = 1;
    private static readonly BigInteger EcGx = BigInteger.Zero;
    private static readonly BigInteger EcGy = BigInteger.One;

    public static void Main()
    {
        var config = new ManualConfig()
            .AddJob(Job.MediumRun.WithToolchain(InProcessNoEmitToolchain.Instance))
            .AddLogger(ConsoleLogger.Default);
        BenchmarkRunner.Run<DiffiHellmanBenchmark>(config);
    }

    private static void PrintPair<TPrivate, TPublic>(string name, KeyPair<TPrivate, TPublic> pair)
    {
        Console.WriteLine($"{name}: ");
        Console.WriteLine($"PrivateKey: {pair.PrivateKey}");
        Console.WriteLine($"PublicKey: {pair.PublicKey}");
    }

    private static void ShowPointsOnCurve()
    {
        for (var x = 0; x < EcP; x++)
        for (var y = 0; y < EcP; y++)
        {
            var point = new EllipticCurve(x, y, EcA, EcB, EcP);
            if (!point.IsOnCurve()) continue;
            Console.WriteLine(point.ToString());
        }
    }
}