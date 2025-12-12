using System.Numerics;
using Core;

namespace Tests;

public class StandardDiffieHellmanTests
{
    private readonly BigInteger _g = 2;

    private readonly BigInteger _p = BigInteger.Parse(
        "1217343378044033732497225708494830131692414359657113541135980315982138267969103935889495592387103987103987103987103987103987103987103987103987103678710368710368710368710368710368710368710368710368710368710368710367812345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789");

    private IDiffieHellman<BigInteger, BigInteger> _diffieHellman = null!;

    [SetUp]
    public void Setup()
    {
        _diffieHellman = new StandardDiffieHellman(_p, _g, new PrimeBigIntegerGenerator(_p));
    }

    [Test]
    public void ShouldBeSessionKeyEqual()
    {
        var alice = _diffieHellman.GeneratePair();
        var bob = _diffieHellman.GeneratePair();
        var aliceSharedKey = _diffieHellman.GenerateSharedSecret(alice.PrivateKey, bob.PublicKey);
        var bobSharedKey = _diffieHellman.GenerateSharedSecret(bob.PrivateKey, alice.PublicKey);
        Assert.That(aliceSharedKey, Is.EqualTo(bobSharedKey));
    }
}