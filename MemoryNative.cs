using System;
using System.Runtime.InteropServices;

class MemoryNative
{
    [DllImport("mylib")]
    public static extern IntPtr my_malloc(uint size);

    [DllImport("mylib")]
    public static extern void my_free(IntPtr ptr);
}