#include <fstream>
#include <iostream>

#include "../level.h"

int level1(lebewesen & held)
{
    lausgabe("Entity_Info/Level/level1.txt");
    std::cout << std::endl;
    lebewesen gegner("Entity_Info/Enemies/Skelett.txt");
    held.stats(held);
    std::cout << std::endl;
    std::cout << "\n";
    gegner.stats(gegner);
    std::cout << std::endl;
    if(held.kampf(held,gegner))
    {
        std::cout << "\nHeld ist gestorben";
        return 1;
    }
    std::cout << std::endl;
    held.lvup(held);
    held.refresh(held);
    std::cout << std::endl;
    item dummy("dummy.txt");
    dummy.auswahl("Entity_Info/Items/item1.1.txt",
                  "Entity_Info/Items/item1.2.txt",
                  "Entity_Info/Items/item1.3.txt",
                  held);
    return 0;
}

int level2(lebewesen & held)
{
    std::cout << std::endl;
    lausgabe("Entity_Info/Level/level2.txt");
    std::cout << std::endl;
    lebewesen gegner("Entity_Info/Enemies/Ghoul.txt");
    held.stats(held);
    std::cout << std::endl;
    std::cout << "\n";
    gegner.stats(gegner);
    std::cout << std::endl;
    if(held.kampf(held,gegner))
    {
        std::cout << "\nHeld ist gestorben";
        return 1;
    }
    std::cout << std::endl;
    held.refresh(held);
    held.lvup(held);
    std::cout << std::endl;
    item dummy("dummy.txt");
    dummy.auswahl("Entity_Info/Items/item2.1.txt",
                  "Entity_Info/Items/item2.2.txt",
                  "Entity_Info/Items/item2.3.txt",
                  held);
    return 0;
}

int level3(lebewesen & held)
{
    std::cout << std::endl;
    lausgabe("Entity_Info/Level/level3.txt");
    std::cout << std::endl;
    lebewesen gegner("Entity_Info/Enemies/Skelettmagier.txt");
    held.stats(held);
    std::cout << std::endl;
    std::cout << "\n";
    gegner.stats(gegner);
    std::cout << std::endl;
    if(held.kampf(held, gegner))
    {
        std::cout << "\nHeld ist gestorben";
        return 1;
    }
    std::cout << std::endl;
    held.refresh(held);
    held.lvup(held);
    std::cout << std::endl;
    item dummy("dummy.txt");
    dummy.auswahl("Entity_Info/Items/item3.1.txt",
                  "Entity_Info/Items/item3.2.txt",
                  "Entity_Info/Items/item3.3.txt",
                  held);
    return 0;
}

int level4(lebewesen & held)
{
    std::cout << std::endl;
    lausgabe("Entity_Info/Level/level4.txt");
    std::cout << std::endl;
    lebewesen gegner("Entity_Info/Enemies/Geist.txt");
    held.stats(held);
    std::cout << std::endl;
    std::cout << "\n";
    gegner.stats(gegner);
    std::cout << std::endl;
    if(held.kampf(held,gegner))
    {
        std::cout << "\nHeld ist gestorben";
        return 1;
    }
    std::cout << std::endl;
    held.refresh(held);
    held.lvup(held);
    std::cout << std::endl;
    item dummy("dummy.txt");
    dummy.auswahl("Entity_Info/Items/item4.1.txt",
                  "Entity_Info/Items/item4.2.txt",
                  "Entity_Info/Items/item4.3.txt",
                  held);
    return 0;
}

int level5(lebewesen & held)
{
    std::cout << std::endl;
    lausgabe("Entity_Info/Level/level5.txt");
    std::cout << std::endl;
    lebewesen gegner("Entity_Info/Enemies/Grabesfürst.txt");
    held.stats(held);
    std::cout << std::endl;
    std::cout << "\n";
    gegner.stats(gegner);
    std::cout << std::endl;
    if(held.kampf(held,gegner))
    {
        std::cout << "\nHeld ist gestorben";
        return 1;
    }
    return 0;
}

void lausgabe(const char *a)
{
    std::fstream x;
    char b[100];
    x.open(a);
    while(x.eof()!=1)
    {
        x.getline(b,100);
        std::cout << b << "\n";
    }
}
