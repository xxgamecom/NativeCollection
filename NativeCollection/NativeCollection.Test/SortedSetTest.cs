using FluentAssertions;
using Xunit;

namespace NativeCollection.Test;

public unsafe class SortedSetTest
{
    [Fact]
    public void MinMax()
    {
        SortedSet<int> nativeSortedSet = new SortedSet<int>();
        System.Collections.Generic.SortedSet<int> managedSortedSet = new System.Collections.Generic.SortedSet<int>();
        
        for (int i = 0; i < 1000; i++)
        {
            int value = Random.Shared.Next();
            nativeSortedSet.Add(value);
            managedSortedSet.Add(value);
        }

        nativeSortedSet.Min.Should().Be(managedSortedSet.Min);
        nativeSortedSet.Max.Should().Be(managedSortedSet.Max);
    }

    [Fact]
    public void Enumerator()
    {
        SortedSet<int> nativeSortedSet =new SortedSet<int>();
        System.Collections.Generic.SortedSet<int> managedSortedSet = new System.Collections.Generic.SortedSet<int>();
        
        for (int i = 0; i < 1000; i++)
        {
            int value = Random.Shared.Next();
            nativeSortedSet.Add(value);
            managedSortedSet.Add(value);
        }

        var nativeSortedSetEnumerrator = nativeSortedSet.GetEnumerator();
        var managedSortedSetEnumerrator = managedSortedSet.GetEnumerator();
        while (managedSortedSetEnumerrator.MoveNext())
        {
            nativeSortedSetEnumerrator.MoveNext();
            nativeSortedSetEnumerrator.Current.Should().Be(managedSortedSetEnumerrator.Current);
        }
    }

    [Fact]
    public void Count()
    {
        SortedSet<int> nativeSortedSet = new SortedSet<int>();
        nativeSortedSet.Count.Should().Be(0);
        
        for (int i = 0; i < 1000; i++)
        {
            int value = Random.Shared.Next();
            nativeSortedSet.Add(value);
        }

        nativeSortedSet.Count.Should().Be(1000);
        nativeSortedSet.Clear();
        nativeSortedSet.Count.Should().Be(0);
    }

    [Fact]
    public void AddRemove()
    {
        SortedSet<int> nativeSortedSet = new SortedSet<int>();
        bool addResult;
        bool removeResult;
        addResult = nativeSortedSet.Add(1);
        addResult.Should().Be(true);
        nativeSortedSet.Count.Should().Be(1);
        addResult = nativeSortedSet.Add(1);
        nativeSortedSet.Count.Should().Be(1);
        addResult.Should().Be(false);
        removeResult = nativeSortedSet.Remove(1);
        nativeSortedSet.Count.Should().Be(0);
        removeResult.Should().Be(true);
        removeResult = nativeSortedSet.Remove(1);
        removeResult.Should().Be(false);
    }

    [Fact]
    public void AddRemoveInEnumerator()
    {
        SortedSet<int> nativeSortedSet = new SortedSet<int>();
        
        for (int i = 0; i < 100; i++)
        {
            int value = Random.Shared.Next();
            nativeSortedSet.Add(value);
        }

        bool hasException = false;

        try
        {
            foreach (var value in nativeSortedSet)
            {
                nativeSortedSet.Add(Random.Shared.Next());
            }
        }
        catch (Exception e)
        {
            hasException = true;
        }

        hasException.Should().Be(true);
        hasException = false;
        
        try
        {
            foreach (var value in nativeSortedSet)
            {
                nativeSortedSet.Remove(Random.Shared.Next());
            }
        }
        catch (Exception e)
        {
            hasException = true;
        }
        hasException.Should().Be(true);
    }
    
    [Fact]
    public void NativeCollectionClass()
    {
        SortedSet<int> sortedSet = new SortedSet<int>();
        sortedSet.IsDisposed.Should().Be(false);
        for (int i = 0; i < 100; i++)
        {
            sortedSet.Add(Random.Shared.Next());
        }
        sortedSet.Dispose();
        sortedSet.IsDisposed.Should().Be(true);
        sortedSet.ReInit();
        sortedSet.IsDisposed.Should().Be(false);
        sortedSet.Count.Should().Be(0);
        for (int i = 0; i < 100; i++)
        {
            sortedSet.Add(Random.Shared.Next());
        }
    }
    
}