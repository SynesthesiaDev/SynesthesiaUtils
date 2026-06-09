// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using System.Reflection;
using System.Text;

namespace Synesthesia.Utils.Extensions;

public static class AssemblyExtensions
{
    extension(Assembly assembly)
    {
        public byte[] GetResource(string path)
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

        public string GetTextResource(string path)
        {
            return Encoding.UTF8.GetString(assembly.GetResource(path));
        }
    }
}
