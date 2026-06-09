using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Synesthesia.Utils;

public class RuntimeInfo
{
    public static readonly Assembly ASSEMBLY = Assembly.GetAssembly(typeof(RuntimeInfo))!;

    public static string StartupDirectory { get; } = AppContext.BaseDirectory;

    public static Platform OperatingSystem { get; }

    public static bool IsUnix => OperatingSystem != Platform.Windows && OperatingSystem != Platform.Unknown;
    public static bool IsDesktop => OperatingSystem is Platform.Linux or Platform.macOS or Platform.Windows;
    public static bool IsMobile => OperatingSystem is Platform.iOS or Platform.Android;
    public static bool IsApple => OperatingSystem is Platform.iOS or Platform.macOS;

    static RuntimeInfo()
    {
        if (System.OperatingSystem.IsWindows())
            OperatingSystem = Platform.Windows;
        if (System.OperatingSystem.IsIOS())
            OperatingSystem = OperatingSystem == 0 ? Platform.iOS : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.iOS)}, but is already {Enum.GetName(OperatingSystem)}");
        if (System.OperatingSystem.IsAndroid())
            OperatingSystem = OperatingSystem == 0 ? Platform.Android : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.Android)}, but is already {Enum.GetName(OperatingSystem)}");
        if (System.OperatingSystem.IsMacOS())
            OperatingSystem = OperatingSystem == 0 ? Platform.macOS : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.macOS)}, but is already {Enum.GetName(OperatingSystem)}");
        if (System.OperatingSystem.IsLinux())
            OperatingSystem = OperatingSystem == 0 ? Platform.Linux : throw new InvalidOperationException($"Tried to set OS Platform to {nameof(Platform.Linux)}, but is already {Enum.GetName(OperatingSystem)}");
        if (OperatingSystem == 0)
            OperatingSystem = Platform.Unknown;
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
