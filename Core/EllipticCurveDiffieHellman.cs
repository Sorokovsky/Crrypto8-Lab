using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public class EllipticCurveDiffieHellman : IDiffieHellman<BigInteger, EllipticCurve>
{
    private readonly EllipticCurve _g;

    public EllipticCurveDiffieHellman(EllipticCurve g)
    {
        _g = g;
        if (_g.IsOnCurve() is false) throw new ArgumentException("EllipticCurve must be on curve");
    }

    public KeyPair<BigInteger, EllipticCurve> GeneratePair()
    {
        var bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);

        var privateKey = new BigInteger(bytes, true, true);

        var max = BigInteger.One << 256;
        privateKey &= max - 1;

        if (privateKey <= 0) privateKey = 1;

        var publicKey = privateKey * _g;
        return new KeyPair<BigInteger, EllipticCurve>(privateKey, publicKey);
    }

    public EllipticCurve GenerateSharedSecret(BigInteger myPrivateKey, EllipticCurve otherPublicKey)
    {
        if (myPrivateKey <= 0) throw new ArgumentException("PrivateKey must be positive");
        if (otherPublicKey.IsInfinity || otherPublicKey.IsOnCurve() is false)
            throw new ArgumentException("EllipticCurve must be on curve");
        return myPrivateKey * otherPublicKey;
    }
}