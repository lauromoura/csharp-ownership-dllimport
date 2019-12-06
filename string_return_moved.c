#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#include "helpers.h"

//Return a moved string to c
char *return_moved_string()
{
    return strdup("Some string move out of the function");
}

typedef char *(*ReturnMovedStringCb)(void);

void call_return_moved_string(ReturnMovedStringCb cb, CleanupCb wait_cb)
{
    char *msg = cb();
    // wait_cb();
    printf("NATIVE: string return-moved from cb was [%s] at %p. Freeing...\n", msg, msg);
    free(msg);
}