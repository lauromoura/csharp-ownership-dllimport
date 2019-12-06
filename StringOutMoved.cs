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
public class StringOutMoved
{
    [DllImport("mylib")]
    public static extern void get_out_moved_string(out string msg);

    public delegate void OutMovedStringCb(out string msg);
    [DllImport("mylib")]
    public static extern void call_out_moved_string(OutMovedStringCb cb);

    private static string word = "a";
    public static void out_moved_string_cb(out string msg)
    {
        word += "a";
        msg = word;
    }

    public void DoIt()
    {
        // Console.WriteLine("=== Basic in string methods");
        // Console.WriteLine();
        string x = null;
        get_out_moved_string(out x);
        // Console.WriteLine();
        call_out_moved_string(out_moved_string_cb);
        // Console.WriteLine();
    }
}