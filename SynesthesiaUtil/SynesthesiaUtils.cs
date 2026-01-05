using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SynesthesiaUtil;

public static class SynesthesiaUtils
{
    public static bool IsDebugBuild => is_debug_build.Value;

    private static readonly Lazy<bool> is_debug_build = new Lazy<bool>(() =>
        isDebugAssembly(typeof(SynesthesiaUtils).Assembly) || isDebugAssembly(RuntimeInfo.Assembly)
    );

    // https://stackoverflow.com/a/2186634
    private static bool isDebugAssembly(Assembly? assembly) => assembly?.GetCustomAttributes(false).OfType<DebuggableAttribute>().Any(da => da.IsJITTrackingEnabled) ?? false;

}