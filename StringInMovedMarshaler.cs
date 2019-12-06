using System;
using System.Runtime.InteropServices;

internal class StringInMovedMarshaler : ICustomMarshaler
{
    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        // Console.WriteLine($"MarshalNativeToManaged called with pNativeData 0x{pNativeData.ToInt64():x}");
        return Marshal.PtrToStringUTF8(pNativeData);
    }

    public IntPtr MarshalManagedToNative(object managedObject)
    {
        IntPtr pNativeData = Marshal.StringToHGlobalAnsi(managedObject as string);
        // Console.WriteLine($"MarshalManagedToNative returning string 0x{pNativeData.ToInt64():x}");
        return pNativeData;
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        // Console.WriteLine($"CleanUpNativeData called with pNativeData 0x{pNativeData.ToInt64():x}");
        // Called from In
    }

    public void CleanUpManagedData(object managedObject)
    {
        // Console.WriteLine($"CleanUpManagedData called with object [{managedObject}]");
    }

    public int GetNativeDataSize() => -1;

    public static ICustomMarshaler instance = null;

    public static ICustomMarshaler GetInstance(string cookie)
    {
        if (instance == null)
        {
            instance = new StringInMovedMarshaler();
        }
        return instance;
    }
}
