namespace Core;

public interface IDiffieHellman<TPrivate, TPublic>
{
    public KeyPair<TPrivate, TPublic> GeneratePair();

    public TPublic GenerateSharedSecret(TPrivate myPrivateKey, TPublic otherPublicKey);
}