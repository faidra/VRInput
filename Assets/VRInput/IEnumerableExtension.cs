using System;
using UniRx;
using System.Linq;
using System.Collections.Generic;

public static class IEnumerableExtension
{
    public static IEnumerable<ValueTuple<T, int>> WithIndex<T>(this IEnumerable<T> source) => source.Select(ValueTuple.Create<T, int>);
}
