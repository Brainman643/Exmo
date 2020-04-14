﻿using System;

namespace Exmo
{
    public static class UnixTime
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime Create(long seconds)
        {
            return Epoch.AddSeconds(seconds);
        }
    }
}