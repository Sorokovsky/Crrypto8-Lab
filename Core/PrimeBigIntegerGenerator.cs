using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public class PrimeBigIntegerGenerator : IRandomGenerator<BigInteger>
{
    private readonly BigInteger _p;

    public PrimeBigIntegerGenerator(BigInteger p)
    {
        _p = p;
    }

    public BigInteger Generate(BigInteger minimum, BigInteger maximum)
    {
        using var generator = RandomNumberGenerator.Create();
        var value = RandomNumberGenerator.GetBytes(_p.GetByteCount());
        var bigInteger = new BigInteger(value, isBigEndian: true, isUnsigned: true);
        while (bigInteger < minimum || bigInteger > maximum)
        {
            value = RandomNumberGenerator.GetBytes(_p.GetByteCount());
            bigInteger = new BigInteger(value, isBigEndian: true, isUnsigned: true);
        }

        return bigInteger;
    }
}