using System;
using System.Runtime.InteropServices;

public class Program
{
    [DllImport("mylib")]
    public static extern void get_in_string(string msg);

    public delegate void InStringCb(string msg);
    [DllImport("mylib")]
    public static extern void call_in_string(InStringCb cb);

    public static void in_string_cb(string msg)
    {
        Console.WriteLine($"Called InCb with msg [{msg}]");
    }


    [DllImport("mylib")]
    public static extern void get_in_own_string(
        [MarshalAs(UnmanagedType.CustomMarshaler,MarshalTypeRef=typeof(StringInOwnMarshaler))]string msg);
        // string msg);


    public delegate void InOwnStringCb(
        [MarshalAs(UnmanagedType.CustomMarshaler,MarshalTypeRef=typeof(StringInOwnMarshaler))]string msg);
        // string msg);
    [DllImport("mylib")]
    public static extern void call_in_own_string(InOwnStringCb cb);
    public static void in_own_string_cb(string msg)
    {
        Console.WriteLine($"Called InOwnCb with msg [{msg}]");
    }

    [DllImport("mylib")]
    [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef=typeof(StringReturnNonOwnMarshaler))]
    public static extern string return_string();
    
    [DllImport("mylib")]
    public static extern string return_owned_string();

    public static void do_tests()
    {

        Console.WriteLine("=== Basic in string methods");
        Console.WriteLine();
        get_in_string("Some String that should not leak");
        Console.WriteLine();
        call_in_string(in_string_cb);

        Console.WriteLine();
        Console.WriteLine("=== Basic in @move string methods");
        Console.WriteLine();

        get_in_own_string("Some owned string");
        Console.WriteLine();

        call_in_own_string(in_own_string_cb);

        Console.WriteLine();

        Console.WriteLine("=== Basic return non-owned string");
        Console.WriteLine();

        var str = return_string();
        Console.WriteLine($"Got non owned string [{str}]");
        Console.WriteLine();

        Console.WriteLine("=== Basic return non-owned string");
        Console.WriteLine();

        var str2 = return_owned_string();
        Console.WriteLine($"Got owned string [{str2}]");
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("Exiting...");

    }
    public static void Main(string[] args)
    {
        do_tests();
        for (int i = 0; i < 1000; i++) {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
    }
}