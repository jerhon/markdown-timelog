using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Honlsoft.TimeLog;

public class TestUtils {

    public static string GetDirectory([CallerFilePath] string path = "") {
        return Path.GetDirectoryName(path);
    }
}