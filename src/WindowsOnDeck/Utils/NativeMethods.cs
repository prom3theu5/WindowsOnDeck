﻿using System.Runtime.InteropServices;

[Flags]
internal enum MoveFileFlags
{
    None = 0,
    ReplaceExisting = 1,
    CopyAllowed = 2,
    DelayUntilReboot = 4,
    WriteThrough = 8,
    CreateHardlink = 16,
    FailIfNotTrackable = 32,
}

internal static class NativeMethods
{
    [DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Unicode)]
    public static extern bool MoveFileEx(
        string lpExistingFileName,
        string lpNewFileName, 
        MoveFileFlags dwFlags);
}