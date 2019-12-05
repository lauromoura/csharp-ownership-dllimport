using System;
using System.Runtime.InteropServices;

public delegate void CleanupCb();

public static class Helpers
{
    public static void Cleanup() {
        for (int i = 0; i < 10000; i++) {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
    }
}

public class Program
{
    public static void do_tests()
    {

        var a = new StringIn();
        a.DoIt();
        Helpers.Cleanup();

        var b = new StringInMoved();
        b.DoIt();
        Helpers.Cleanup();

        var c = new StringReturn();
        c.DoIt();
        Helpers.Cleanup();

        var d = new StringReturnMoved();
        d.DoIt();
        Helpers.Cleanup();
    }
    public static void Main(string[] args)
    {
        do_tests();
        Helpers.Cleanup();
    }
}
