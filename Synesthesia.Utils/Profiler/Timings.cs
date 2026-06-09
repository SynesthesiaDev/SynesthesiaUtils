// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using Synesthesia.Utils.Pooling;

namespace Synesthesia.Utils.Profiler;

public static class Timings
{
    private static readonly FastObjectPool<Profiler> object_pool = new FastObjectPool<Profiler>(() => new Profiler());

    public static Profiler Rent() => object_pool.Rent();

    public static Profiler RentAndPush()
    {
        var profiler = Rent();
        profiler.Start();
        return profiler;
    }

    public class Profiler : IPooledObject
    {
        private long startTimeMs;

        public void Start()
        {
            startTimeMs = Stopwatch.GetTimestamp();
        }

        public long Stop()
        {
            return (long)Stopwatch.GetElapsedTime(startTimeMs).TotalMilliseconds;
        }

        public long PopAndReturn()
        {
            var poppedTime = Stop();
            ReturnAction?.Invoke(this);
            return poppedTime;
        }

        public void Reset() => startTimeMs = 0;

        public bool IsPooled { get; set; } = false;
        public Action<IPooledObject>? ReturnAction { get; set; }
    }
}
