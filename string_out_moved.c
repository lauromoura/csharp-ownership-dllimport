#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// Just send a string, without freeing
void get_out_moved_string(const char **str)
{
    *str = strdup("Do not free me");
}

// Call a C# method that receives a string but must not own it.
typedef void (*OutMovedStringCb)(char **str);
void call_out_moved_string(OutMovedStringCb cb)
{
    char *word;
    cb(&word);
    printf("NATIVE: got out string [%s]\n", word);
    free(word);
}
