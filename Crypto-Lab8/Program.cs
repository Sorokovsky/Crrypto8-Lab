using System.Diagnostics;
using System.Numerics;
using Core;

namespace Crypto_Lab8;

public static class Program
{
    public static void Main()
    {
        var standardDiffieHellman = Constants.StandardDiffieHellman;
        var ellipticCurvesDiffieHellman = Constants.EllipticCurveDiffieHellman;
        GenerationFlow(standardDiffieHellman, "Стандартний Діффі Хеллмана");
        GenerationFlow(ellipticCurvesDiffieHellman, "Діффі Хеллмана на еліптичних кривих");
    }

    private static void PrintPair<TPrivate, TPublic>(string name, KeyPair<TPrivate, TPublic> pair)
    {
        Console.WriteLine($"{name}: ");
        Console.WriteLine($"PrivateKey: {pair.PrivateKey}");
        Console.WriteLine($"PublicKey: {pair.PublicKey}");
    }

    private static KeyPair<TPrivate, TPublic> ShowGenerationPublicKey<TPrivate, TPublic>(string name,
        IDiffieHellman<TPrivate, TPublic> diffieHellman)
    {
        var stopWatch = Stopwatch.StartNew();
        var pair = diffieHellman.GeneratePair();
        stopWatch.Stop();
        Console.WriteLine($"{name} генерує публічний ключ за {stopWatch.Elapsed.TotalMilliseconds} мс.");
        Console.WriteLine($"Публічний ключ: {pair.PublicKey}");
        Console.WriteLine($"Розмір ключа: {GetSize(pair.PublicKey)} байт");
        return pair;
    }

    private static TPublic ShowGenerationShare<TPrivate, TPublic>(string name, TPrivate privateKey, TPublic publicKey,
        IDiffieHellman<TPrivate, TPublic> diffieHellman)
    {
        var stopWatch = Stopwatch.StartNew();
        var sharedKey = diffieHellman.GenerateSharedSecret(privateKey, publicKey);
        stopWatch.Stop();
        Console.WriteLine($"{name} генерує сесійний ключ за {stopWatch.Elapsed.TotalMilliseconds} мс.");
        Console.WriteLine($"Сесійний ключ: {sharedKey}");
        Console.WriteLine($"Розмір ключа: {GetSize(sharedKey)} байт");
        return sharedKey;
    }

    private static int GetSize<T>(T value)
    {
        if (value is EllipticCurve ellipticCurve) return ellipticCurve.ToString().Length / 2;
        if (value is BigInteger bigInteger) return bigInteger.ToString("X").Length / 2;
        return 0;
    }

    private static void GenerationFlow<TPrivate, TPublic>(IDiffieHellman<TPrivate, TPublic> diffieHellman, string name)
    {
        Console.WriteLine(name);
        var alicePairs = ShowGenerationPublicKey("Аліса", diffieHellman);
        var bobPairs = ShowGenerationPublicKey("Боб", diffieHellman);
        var aliceShared = ShowGenerationShare("Аліса", alicePairs.PrivateKey,
            bobPairs.PublicKey, diffieHellman);
        var bobShared = ShowGenerationShare("Боб", bobPairs.PrivateKey, alicePairs.PublicKey,
            diffieHellman);
        Console.WriteLine($"Співпали ключі: {aliceShared.Equals(bobShared)}");
    }
}