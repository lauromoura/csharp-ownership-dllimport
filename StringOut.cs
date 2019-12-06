using System;
using System.Runtime.InteropServices;

/// <summary>
/// Represents a method like:
/// 
/// <code>
/// get_out_string {
///     params {
///         @out msg: string;
///     }
/// }
/// </code>
public class StringOut
{
    [DllImport("mylib")]
    public static extern void get_out_string([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(StringReturnNonMovedMarshaler))]out string msg);

    public delegate void OutStringCb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(StringReturnNonMovedMarshaler))]out string msg);
    [DllImport("mylib")]
    public static extern void call_out_string(OutStringCb cb);

    private static string word = "a";
    public static void out_string_cb(out string msg)
    {
        word += "a";
        msg = word;
    }

    public void DoIt()
    {
        // Console.WriteLine("=== Basic in string methods");
        // Console.WriteLine();
        string x = null;
        get_out_string(out x);
        // Console.WriteLine();
        call_out_string(out_string_cb);
        // Console.WriteLine();
    }
}