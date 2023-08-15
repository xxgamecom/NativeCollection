using System.Runtime.CompilerServices;

namespace NativeCollection;

public unsafe struct MultiMapPair<T, K> : IEquatable<MultiMapPair<T, K>>, IComparable<MultiMapPair<T, K>>, IDisposable
    where T : unmanaged, IEquatable<T>, IComparable<T> where K : unmanaged, IEquatable<K>
{
    public readonly T Key;

    private readonly Internal.List<K>* _value;

    internal ref Internal.List<K> Value => ref Unsafe.AsRef<Internal.List<K>>(_value);

    private MultiMapPair(T key)
    {
        Key = key;
        _value = Internal.List<K>.Create();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultiMapPair<T, K> Create(T key)
    {
        var pair = new MultiMapPair<T, K>(key);
        return pair;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(MultiMapPair<T, K> other)
    {
        return Key.Equals(other.Key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(MultiMapPair<T, K> other)
    {
        return Key.CompareTo(other.Key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _value->Dispose();
    }
}