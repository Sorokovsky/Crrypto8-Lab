using System.Numerics;

namespace Core;

public static class ModuleMath
{
    public static BigInteger Mod(BigInteger number, BigInteger modulus)
    {
        return (number % modulus + modulus) % modulus;
    }

    public static BigInteger ModInverse(BigInteger number, BigInteger modulus)
    {
        var p = modulus;
        var a = Mod(number, modulus);
        var p0 = p;
        BigInteger y = 0, inverse = 1;
        if (p == 1) return 0;
        while (a > 1)
        {
            var q = a / p;
            var temp = p;
            p = a % p;
            a = temp;
            temp = y;
            y = inverse - q * y;
            inverse = temp;
        }

        if (inverse < 0) inverse += p0;
        return inverse;
    }
}