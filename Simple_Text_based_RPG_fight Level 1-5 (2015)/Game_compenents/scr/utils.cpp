#include <stdlib.h>
#include "../utils.h"

double zfllz(double x, double y)
{
    double r = y - x +1;
    return x = x + (int)(r * rand()/(RAND_MAX+1.0));
}
