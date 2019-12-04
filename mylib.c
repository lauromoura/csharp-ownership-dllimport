#include <stdlib.h>
#include <stdio.h>
#include <string.h>

void get_in_string(const char *str)
{
    printf("NATIVE: %s Called with string {%s}\n", __PRETTY_FUNCTION__, str);
    // Should not leak.
}

typedef void (*InStringCb)(const char *str);
void call_in_string(InStringCb cb)
{
    char *msg = strdup("Some native owned string 12345678891234567891231241241241215235252432");
    cb(msg);
    free(msg); // Native code should still own it.
}

void get_in_own_string(char *str)
{
    printf("NATIVE: %s Called with string {%s} at {%p}\n", __PRETTY_FUNCTION__, str, str);
    free(str);
}

typedef void (*InOwnStringCb)(char *str);

void call_in_own_string(InOwnStringCb cb)
{
    char *msg = strdup("Some native owned string 12345678891234567891231241241241215235252432");
    cb(msg);
}

void *my_alloc(size_t count)
{
    return malloc(count);
}

void my_free(void *ptr)
{
    printf("NATIVE freeing: %p\n", ptr);
    free(ptr);
}

// Returning strings

const char *return_string()
{
    return "Some non owned string";
}

char *return_owned_string()
{
    return strdup("Some owned string");
}