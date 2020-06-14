using System;
using System.Threading;

namespace Exmo
{
    public static class Nonce
    {
        private static long _value = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        public static long Value => Interlocked.Increment(ref _value);
    }
}
