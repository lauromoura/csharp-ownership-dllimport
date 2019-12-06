using System;
using System.Runtime.InteropServices;

/// </summary>
/// <summary>
/// Represents a method like:
/// 
/// <code>
/// receive_in_string {
///     return: mstring @moved;
/// }
/// </code>
/// </summary>
public class StringReturnMoved
{
    [DllImport("mylib")]
    public static extern string return_moved_string();

    public delegate string ReturnMovedStringCb();
    [DllImport("mylib")]
    public static extern void call_return_moved_string(ReturnMovedStringCb cb, CleanupCb cleanupCb);

    private static string word = "a";
    public static string return_moved_string_cb()
    {
        word += "a";
        return word;
    }

    public void DoIt()
    {
        // Console.WriteLine();
        // Console.WriteLine("=== Basic return non-owned string");
        // Console.WriteLine();

        var str2 = return_moved_string();
        // Console.WriteLine($"Got owned string [{str2}]");
        // Console.WriteLine();

        call_return_moved_string(return_moved_string_cb, Helpers.Cleanup);

        // Console.WriteLine();
        // Console.WriteLine("Exiting...");
    }
}