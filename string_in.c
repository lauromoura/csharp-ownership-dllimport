#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// Just use a string, without freeing
void receive_in_string(const char *str)
{
    printf("NATIVE: %s Called with string {%s}\n", __PRETTY_FUNCTION__, str);
    // Should not leak.
}

// Call a C# method that receives a string but must not own it.
typedef void (*InStringCb)(const char *str);
void call_in_string(InStringCb cb)
{
    char *msg = strdup("Some native owned string 12345678891234567891231241241241215235252432");
    cb(msg);
    free(msg); // Native code should still own it.
}
