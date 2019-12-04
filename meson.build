project('marshalling', ['c', 'cs'])


lib = library('mylib', ['mylib.c'])
app = executable('App', [
    'App.cs',
    'MemoryNative.cs',
    'StringInOwnMarshaler.cs',
    'StringReturnNonOwnMarshaler.cs'
    ])