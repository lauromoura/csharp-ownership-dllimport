#include <stdio.h>
#include <string.h>
#include <stdlib.h>

// Receive a string and take ownership of it.
void receive_in_moved_string(char *str)
{
    printf("NATIVE: %s Called with string {%s} at {%p}\n", __PRETTY_FUNCTION__, str, str);
    free(str);
}

typedef void (*InOwnStringCb)(char *str);

// Call a C# method that would own the passed string
void call_in_moved_string(InOwnStringCb cb)
{
    char *msg = strdup("Some native owned string 12345678891234567891231241241241215235252432");
    cb(msg);
    // Msg should not leak...
}
