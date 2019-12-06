#include <stdio.h>
#include <stdlib.h>

#include "helpers.h"

// Returning strings

const char *return_string()
{
    return "Some non owned string";
}

typedef const char *(*ReturnStringCb)(void);

void call_return_string(ReturnStringCb cb, CleanupCb wait_cb)
{
    const char *msg = cb();
    // wait_cb();
    printf("NATIVE: non-owned string returned was [%s]\n", msg);
}
