using System;
using System.Runtime.InteropServices;

/// <summary>
/// Represents a method like:
/// 
/// <code>
/// receive_in_string {
///     params {
///         @in msg: string;
///     }
/// }
/// </code>
public class StringIn
{
    [DllImport("mylib")]
    public static extern void receive_in_string(string msg);

    public delegate void InStringCb(string msg);
    [DllImport("mylib")]
    public static extern void call_in_string(InStringCb cb);

    public static void in_string_cb(string msg)
    {
        // Console.WriteLine($"Called InCb with msg [{msg}]");
        int x = msg.Length;
    }

    public void DoIt()
    {
        // Console.WriteLine("=== Basic in string methods");
        // Console.WriteLine();
        receive_in_string("Some String that should not leak");
        // Console.WriteLine();
        call_in_string(in_string_cb);
        // Console.WriteLine();
    }
}