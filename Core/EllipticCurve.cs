using System.Numerics;

namespace Core;

public class EllipticCurve
{
    public EllipticCurve(BigInteger x, BigInteger y, BigInteger a, BigInteger b, BigInteger p)
    {
        X = x;
        Y = y;
        A = a;
        B = b;
        P = p;
        IsInfinity = false;
    }

    private EllipticCurve(bool isInfinity)
    {
        IsInfinity = isInfinity;
    }

    public BigInteger X { get; }
    public BigInteger Y { get; }
    public BigInteger A { get; }
    public BigInteger B { get; }
    public BigInteger P { get; }
    public bool IsInfinity { get; }
    public static EllipticCurve Infinity { get; } = new(true);

    public bool IsOnCurve()
    {
        if (IsInfinity) return true;
        var left = ModuleMath.Mod(Y * Y, P);
        var right = ModuleMath.Mod(ModuleMath.Mod(X * X * X, P) + A * X + B, P);
        return left == right;
    }

    public static EllipticCurve operator +(EllipticCurve first, EllipticCurve second)
    {
        if (first.IsInfinity) return second;
        if (second.IsInfinity) return first;
        if (first.X == second.X && first.Y == ModuleMath.Mod(-second.Y, first.P)) return Infinity;
        BigInteger lambda;
        if (first == second)
            lambda = ModuleMath.Mod(
                (3 * first.X * first.X + first.A) * ModuleMath.ModInverse(2 * first.Y, first.P),
                first.P);
        else
            lambda = ModuleMath.Mod((second.Y - first.Y) * ModuleMath.ModInverse(second.X - first.X, first.P), first.P);

        var x3 = ModuleMath.Mod(lambda * lambda - first.X - second.X, first.P);
        var y3 = ModuleMath.Mod(lambda * (first.X - x3) - first.Y, first.P);
        return new EllipticCurve(x3, y3, first.A, first.B, first.P);
    }

    public static EllipticCurve operator *(BigInteger scalar, EllipticCurve curve)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(scalar);
        if (scalar == 0) return Infinity;
        var result = Infinity;
        var addend = curve;
        while (scalar > 0)
        {
            if (scalar.IsEven is false) result += addend;
            addend += addend;
            scalar /= 2;
        }

        return result;
    }

    public static bool operator ==(EllipticCurve first, EllipticCurve second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(EllipticCurve first, EllipticCurve second)
    {
        return !first.Equals(second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EllipticCurve other) return false;
        if (IsInfinity || other.IsInfinity) return IsInfinity && other.IsInfinity;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return IsInfinity ? 0 : HashCode.Combine(X, Y);
    }
}