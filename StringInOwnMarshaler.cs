using System;
using System.Runtime.InteropServices;

internal class StringInOwnMarshaler : ICustomMarshaler
{
    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        Console.WriteLine($"MarshalNativeToManaged called with pNativeData 0x{pNativeData.ToInt64():x}");
        return Marshal.PtrToStringUTF8(pNativeData);
    }

    public IntPtr MarshalManagedToNative(object managedObject)
    {
        IntPtr pNativeData = Marshal.StringToHGlobalAnsi(managedObject as string);
        Console.WriteLine($"MarshalManagedToNative returning string 0x{pNativeData.ToInt64():x}");
        return pNativeData;
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

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return new StringInOwnMarshaler();
    }
}
