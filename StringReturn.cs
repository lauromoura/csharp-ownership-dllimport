using System;
using System.Runtime.InteropServices;

/// </summary>
/// <summary>
/// Represents a method like:
/// 
/// <code>
/// receive_in_string {
///     return: string;
/// }
/// </code>
/// </summary>
public class StringReturn
{
    [DllImport("mylib")]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(StringReturnNonMovedMarshaler))]
    public static extern string return_string();

    public delegate string ReturnStringCb();
    [DllImport("mylib")]
    public static extern void call_return_string(ReturnStringCb cb, CleanupCb Cleanup);
    public static string return_string_cb()
    {
        var owner = "managed";
        return $"Some string owned by {owner}";
    }

    public void DoIt()
    {
        Console.WriteLine("=== Basic return non-owned string");
        Console.WriteLine();

        var str = return_string();
        Console.WriteLine($"Got non owned string [{str}]");
        Console.WriteLine();

        call_return_string(return_string_cb, Helpers.Cleanup);

    }
}