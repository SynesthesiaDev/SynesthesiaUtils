using System.IO;
using System.Reflection;
using System.Text;

namespace SynesthesiaUtil.Extensions;

public static class AssemblyExtensions
{
    public static byte[] GetResource(this Assembly assembly, string path)
    {
        using var stream = assembly.GetManifestResourceStream(path);
        if (stream == null) throw new FileNotFoundException($"Embedded resource '{path}' does not exist!");
        using var memoryStream = new MemoryStream();

        stream.CopyTo(memoryStream);
        var array = memoryStream.ToArray();

        stream.Close();
        memoryStream.Close();

        return array;
    }

    public static string GetTextResource(this Assembly assembly, string path)
    {
        return Encoding.UTF8.GetString(assembly.GetResource(path));
    }
}