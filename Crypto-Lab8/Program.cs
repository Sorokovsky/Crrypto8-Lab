using System.Numerics;
using Core;

namespace Crypto_Lab8;

public static class Program
{
    private static readonly BigInteger P = 23;

    private static readonly BigInteger A = BigInteger.Zero;

    private static readonly BigInteger B = 7;

    private static readonly BigInteger Gx = 15;

    private static readonly BigInteger Gy = 13;

    private static EllipticCurve G => new(Gx, Gy, A, B, P);

    public static void Main()
    {
        var diffieHellman = new EllipticCurveDiffieHellman(G);

        var alice = diffieHellman.GeneratePair();

        var bob = diffieHellman.GeneratePair();

        PrintPair("Alice", alice);
        PrintPair("Bob", bob);

        var aliceSharedKey = diffieHellman.GenerateSharedSecret(alice.PrivateKey, bob.PublicKey);

        var bobSharedKey = diffieHellman.GenerateSharedSecret(bob.PrivateKey, alice.PublicKey);

        Console.WriteLine($"Alice Shared Secret: {aliceSharedKey}");
        Console.WriteLine($"Bob Shared Secret: {bobSharedKey}");

        Console.WriteLine($"Shared keys are equals: {aliceSharedKey.Equals(bobSharedKey)}");
    }

    private static void PrintPair<TPrivate, TPublic>(string name, KeyPair<TPrivate, TPublic> pair)
    {
        Console.WriteLine($"{name}: ");
        Console.WriteLine($"PrivateKey: {pair.PrivateKey}");
        Console.WriteLine($"PublicKey: {pair.PublicKey}");
    }
}