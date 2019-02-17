using System;

namespace WebApplication1.Extensions
{
    public static class ChainExtensions
    {
        public static R Map<T, R>(this T source, Func<T, R> mapper) => mapper(source);
    }
}
