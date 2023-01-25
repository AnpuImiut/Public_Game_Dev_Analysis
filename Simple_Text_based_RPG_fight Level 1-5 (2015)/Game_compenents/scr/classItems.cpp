#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <fstream>

#include "../classItems.h"

item::item(const char *dname)
{
    std::fstream x;
    x.open(dname);
    char a[100];
    x.getline(a,100);
    strcpy(name,a);
    x.getline(a,100);
    x.getline(a,100);
    HP = atoi(a);
    x.getline(a,100);
    Armor = atoi(a);
    x.getline(a,100);
    Dodge = atoi(a);
    x.getline(a,100);
    Dmg = atoi(a);
}

void item::ausgabe()
{
    std::cout << "Name : " << name;
    std::cout << "\nHP : " << HP;
    std::cout << "\nArmor : " << Armor;
    std::cout << "\nDodge : " << Dodge;
    std::cout << "\nDmg : " << Dmg << "\n\n";
}

item::~item(){};

void item::auswahl(const char *i1,const char *i2,const char *i3,lebewesen & a)
{
    item item1(i1);
    item item2(i2);
    item item3(i3);
    item1.ausgabe();
    item2.ausgabe();
    item3.ausgabe();
    std::cout << "\nWaehle ein Item(1/2/3)\n";
    int auswahl;
    std::cin >> auswahl;
    switch(auswahl)
    {
    case 1:
        {
            item1.Wahl(item1,a);break;
        }
    case 2:
        {
            item2.Wahl(item2,a);break;
        }
    case 3:
        {
            item3.Wahl(item3,a);break;
        }
    }
}

void item::Wahl(item & a,lebewesen & b)
{
    b.HP = b.HP + a.HP;
    b.s_HP = b.s_HP + a.HP;
    b.Armor = b.Armor + a.Armor;
    b.Dodge = b.Dodge + a.Dodge;
    b.minDmg = b.minDmg + a.Dmg;
    b.maxDmg = b.maxDmg + a.Dmg;
}
