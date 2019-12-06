# String/Data ownership in DllImport scenarios

This is a set of examples about moving data between C# and C through `DllIport`,
with or without changes of ownership, like eolian's `@move` tag.

## Leaking contexts

- Types involving allocation of native memory (and ownership)
    - Strings
    - Collections
    - By-ref value types?
    - Struct fields?

- Returning/outing data from C# to C
    - Virtual method implementations


## Marshaler semantics

* In parameter from C# calling C
    * `MarshalManagedToNative`
    * `CleanupNativeData`
        - To cleanup the allocated data above

* In parameter from C calling C#
    * `MarshalNativeToManaged`
    * `CleanupManagedData`

* Return value C# calling C
    * `MarshalNativeToManaged`
    * `CleanupNativeData`
        - To clean up the data we received from C

* Return value C calling C#
    * `MarshalManagedToNative`
        - Beware of leaking memory in non-owned contexts
    * `CleanupManagedData`

## Example: strings

- `@in string`: C# calling C
    - Convert to native pointer
    - C# can deallocate the native after returning from the C method
    - Could be done with default string marshaling\

- `@in string`: C calling C#
    - Plain string marshalling seems to work with manual testing.

- `@in string @move`: C# calling C
    - Can't use default marshaling.
    - Must allocate a new Native one and give it to C
    - Use custom marshaler
        - MarshalManagedToNative
            - Called before entering C
        - CleanUpNativeData
            - Called after returning from C

- `@in string @move`: C calling C#
    - C# doesn't seems to be taking ownership.
        - Valgrind shows leak with default marshaling
    - Use Custom Marshalling
        - MarshalNativeToManaged
            - Called before entering C# cb
            - Must free the Native datahere
        - CleanupManagedData
            - Called after returning to C

- `return string`: C# calling C
    - Kaboooms with default marshalling
    - Needs to be marshalled manually
        - MarshalNativeToManaged
        - CleanupNativeData

- `return string`: C calling C#
    - Should work with default marshalling.
    - But could kaboom?

- `return string @move`: C# calling C
    - Default marshalling?

- `return string @move`: C calling C#
    - Default marshalling?
