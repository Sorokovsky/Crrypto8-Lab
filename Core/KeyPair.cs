namespace Core;

public record KeyPair<TPrivate, TPublic>(TPrivate PrivateKey, TPublic PublicKey);