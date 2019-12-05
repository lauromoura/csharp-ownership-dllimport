project('marshalling', ['c', 'cs'])


lib = library('mylib', [
    'helpers.h',
    'helpers.c',
    'string_in.c',
    'string_in_moved.c',
    'string_return.c',
    'string_return_moved.c',
    ])
app = executable('App', [
    'App.cs',
    'MemoryNative.cs',
    'StringIn.cs',
    'StringInMoved.cs',
    'StringInMovedMarshaler.cs',
    'StringReturn.cs',
    'StringReturnMoved.cs',
    'StringReturnNonMovedMarshaler.cs'
    ])