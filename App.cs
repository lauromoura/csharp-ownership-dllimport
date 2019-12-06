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

        // var a = new StringIn();
        // a.DoIt();
        // // Helpers.Cleanup();

        // var b = new StringInMoved();
        // // for (int i = 0; i < 1000; i++)
        //     b.DoIt();
        // // Helpers.Cleanup();

        // var c = new StringReturn();
        // // for (int i = 0; i < 1000; i++)
        //     c.DoIt();
        // // Helpers.Cleanup();

        // var d = new StringReturnMoved();
        // // for (int i = 0; i < 1000; i++)
        //     d.DoIt();
        // Helpers.Cleanup();

        var e = new StringOut();
        for (int i = 0; i < 1000; i++)
        e.DoIt();

        // var f = new StringOutMoved();
        // e.DoIt();
    }
    public static void Main(string[] args)
    {
        do_tests();
        Helpers.Cleanup();
    }
}
