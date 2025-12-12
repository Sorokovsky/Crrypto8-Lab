using System.Numerics;

namespace Core;

public class StandardDiffieHellman : IDiffieHellman<BigInteger, BigInteger>
{
    private readonly BigInteger _g;
    private readonly BigInteger _p;
    private readonly IRandomGenerator<BigInteger> _randomGenerator;

    public StandardDiffieHellman(BigInteger p, BigInteger g, IRandomGenerator<BigInteger> randomGenerator)
    {
        _p = p;
        _g = g;
        _randomGenerator = randomGenerator;
    }

    public KeyPair<BigInteger, BigInteger> GeneratePair()
    {
        var privateKey = _randomGenerator.Generate(2, _p - 2);
        while (privateKey == _p - privateKey) privateKey = _randomGenerator.Generate(2, _p - 2);

        var publicKey = BigInteger.ModPow(_g, privateKey, _p);
        return new KeyPair<BigInteger, BigInteger>(privateKey, publicKey);
    }

    public BigInteger GenerateSharedSecret(BigInteger myPrivateKey, BigInteger otherPublicKey)
    {
        return BigInteger.ModPow(otherPublicKey, myPrivateKey, _p);
    }
}