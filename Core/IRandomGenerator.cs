namespace Core;

public interface IRandomGenerator<T>
{
    public T Generate(T minimum, T maximum);
}