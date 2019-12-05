#include <stdlib.h>
#include <stdio.h>

void *my_alloc(size_t count)
{
    return malloc(count);
}

void my_free(void *ptr)
{
    printf("NATIVE freeing: %p\n", ptr);
    free(ptr);
}