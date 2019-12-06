using System;
using System.Runtime.InteropServices;

/// <summary>
/// Represents a method like:
/// 
/// <code>
/// receive_in_string {
///     params {
///         @in msg: mstring @moved;
///     }
/// }
/// </code>
/// </summary>
public class StringInMoved
{
    [DllImport("mylib")]
    public static extern void receive_in_moved_string(
        [MarshalAs(UnmanagedType.CustomMarshaler,MarshalTypeRef=typeof(StringInMovedMarshaler))]string msg);
        // string msg);


    public delegate void InMovedStringCb(
        [MarshalAs(UnmanagedType.CustomMarshaler,MarshalTypeRef=typeof(StringInMovedNativeMarshaler))]string msg);
        // string msg);
    [DllImport("mylib")]
    public static extern void call_in_moved_string(InMovedStringCb cb);
    public static void Cb(string msg)
    {
        // Console.WriteLine($"Called InMovedCb with msg [{msg}]");
        int x = msg.Length;
    }

    public void DoIt()
    {

        // Console.WriteLine();
        // Console.WriteLine("=== Basic in @move string methods");
        // Console.WriteLine();

        receive_in_moved_string("Some moved string from managed to unmanaged");
        // Console.WriteLine("Done with C# calling C\n");

        call_in_moved_string(Cb);

        // Console.WriteLine();
    }

}