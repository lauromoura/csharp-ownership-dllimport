using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

internal class StringReturnNonMovedMarshaler : ICustomMarshaler
{
    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        Console.WriteLine($"MarshalNativeToManaged called with pNativeData 0x{pNativeData.ToInt64():x}");
        return Marshal.PtrToStringUTF8(pNativeData);
    }

    private List<IntPtr> cachedPtrs = new List<IntPtr>();

    public IntPtr MarshalManagedToNative(object managedObject)
    {
        IntPtr pNativeData = Marshal.StringToHGlobalAnsi(managedObject as string);
        Console.WriteLine($"MarshalManagedToNative returning string 0x{pNativeData.ToInt64():x}");
        cachedPtrs.Add(pNativeData);
        return pNativeData;
        // return IntPtr.Zero;
    }

    public void Cleanup()
    {
        foreach (var ptr in cachedPtrs)
        {
            Marshal.FreeHGlobal(ptr);
        }
        cachedPtrs = new List<IntPtr>();
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        Console.WriteLine($"CleanUpNativeData called with pNativeData 0x{pNativeData.ToInt64():x}");
        // MemoryNative.my_free(pNativeData);
    }

    public void CleanUpManagedData(object managedObject)
    {
        Console.WriteLine($"CleanUpManagedData called with object [{managedObject}]");
    }

    public int GetNativeDataSize() => -1;

    private static ICustomMarshaler instance = null;

    public static ICustomMarshaler GetInstance(string cookie)
    {
        if (instance == null)
        {
            instance = new StringReturnNonMovedMarshaler();
        }
        return instance;
    }
}
