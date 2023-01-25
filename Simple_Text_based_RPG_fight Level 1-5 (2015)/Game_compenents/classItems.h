#ifndef CLASSITEMS_H_INCLUDED
#define CLASSITEMS_H_INCLUDED

#include "classNPC.h"


class item
{
public:
    item(const char *dname);
    ~item();
    void ausgabe();
    void auswahl(const char *i1,const char *i2,const char *i3,lebewesen & a);
    void Wahl(item & a,lebewesen & b);


private:
    char name[100];
    int HP,Armor,Dodge,Dmg;

};

#endif // CLASSITEMS_H_INCLUDED
