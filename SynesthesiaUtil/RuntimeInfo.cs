using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SynesthesiaUtil;

public class RuntimeInfo
{
    public static readonly Assembly Assembly = Assembly.GetAssembly(typeof(RuntimeInfo))!;

    public static string StartupDirectory { get; } = AppContext.BaseDirectory;

    public static Platform OS { get; }

    public static bool IsUnix => OS != Platform.Windows && OS != Platform.Unknown;
    public static bool IsDesktop => OS is Platform.Linux or Platform.macOS or Platform.Windows;
    public static bool IsMobile => OS is Platform.iOS or Platform.Android;
    public static bool IsApple => OS is Platform.iOS or Platform.macOS;

    static RuntimeInfo()
    {
        if (OperatingSystem.IsWindows())
            OS = Platform.Windows;
        if (OperatingSystem.IsIOS())
            OS = OS == 0 ? Platform.iOS : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.iOS)}, but is already {Enum.GetName(OS)}");
        if (OperatingSystem.IsAndroid())
            OS = OS == 0 ? Platform.Android : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.Android)}, but is already {Enum.GetName(OS)}");
        if (OperatingSystem.IsMacOS())
            OS = OS == 0 ? Platform.macOS : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.macOS)}, but is already {Enum.GetName(OS)}");
        if (OperatingSystem.IsLinux())
            OS = OS == 0 ? Platform.Linux : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.Linux)}, but is already {Enum.GetName(OS)}");
        if (OS == 0)
            OS = Platform.Unknown;
    }


    public static string GetAssemblyPath()
    {
        var assembly = Assembly.GetAssembly(typeof(RuntimeInfo));
        Debug.Assert(assembly != null);

        return assembly.Location;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Platform
    {
        Windows = 1,
        Linux = 2,
        macOS = 3,
        iOS = 4,
        Android = 5,
        Unknown = 6,
    }
}