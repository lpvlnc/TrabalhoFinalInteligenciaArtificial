#include <stdio.h>

int main()
{
    int s, e, n, d, m, o, r, y;
    int v1, v2, v3;

    for (s = 0; s < 10; ++s)
    {
        for (e = 0; e < 10; ++e)
        {
            for (n = 0; n < 10; ++n)
            {
                for (d = 0; d < 10; ++d)
                {
                    for (m = 1; m < 10; ++m)
                    {
                        for (o = 0; o < 10; ++o)
                        {
                            for (r = 0; r < 10; ++r)
                            {
                                for (y = 0; y < 10; ++y)
                                {	
                                    v1 = s * 1000 + e * 100 + n * 10 + d;
                                    v2 = m * 1000 + o * 100 + r * 10 + e;
                                    v3 = m * 10000 + o * 1000 + n * 100 + e * 10 + y;
                                    printf ("%d+%d=%d\n", v1, v2, v3);

                                    if (s != e && s != n && s != d && s != m && s != o && s != r && s != y &&
                                        e != n && e != d && e != m && e != o && e != r && e != y &&
                                        n != d && n != m && n != o && n != r && n != y &&
                                        d != m && d != o && d != r && d != y &&
                                        m != o && m != r && m != y &&
                                        o != r && o != y &&
                                        r != y &&
                                        v1 + v2 == v3)    
                                    {
                                    	printf("SEND (%d)+MORE (%d) = MONEY (%d)\n", v1, v2, v3);	
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    return 0;
}

