#include <iostream>
#include <time.h>
#include <stdlib.h>

#include "Game_compenents/level.h"
#include "Game_compenents/classNPC.h"


int abenteuer1(lebewesen & held);

int main()
{
    srand(time(0));
    lebewesen Held("Entity_Info/Hero/held.txt");
    abenteuer1(Held);

}

int abenteuer1(lebewesen & held)
{
    if(level1(held))
    {
        std::cout << "\nVerloren";
        return 0;
    }
    if(level2(held))
    {
        std::cout << "\nVerloren";
        return 0;
    }
    if(level3(held))
    {
        std::cout << "\nVerloren";
        return 0;
    }
    if(level4(held))
    {
        std::cout << "\nVerloren";
        return 0;
    }
    if(level5(held))
    {
        std::cout << "\nVerloren";
        return 0;
    }
    return 1;
}
