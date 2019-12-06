#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// Just send a string, without freeing
void get_out_string(const char **str)
{
    *str = "Do not free me";
    // Should not leak.
}

// Call a C# method that receives a string but must not own it.
typedef void (*OutStringCb)(const char **str);
void call_out_string(OutStringCb cb)
{
    const char *word;
    cb(&word);
    printf("NATIVE: got out string [%s]\n", word);
}
