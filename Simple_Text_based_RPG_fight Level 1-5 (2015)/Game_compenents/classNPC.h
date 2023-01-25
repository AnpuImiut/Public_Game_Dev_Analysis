#ifndef CLASSNPC_H_INCLUDED
#define CLASSNPC_H_INCLUDED

void warten();

class lebewesen
{
public:
    double HP,s_HP,Armor,Dodge,minDmg,maxDmg;;
    lebewesen(const char *dname);
    ~lebewesen();
    int dodge_test(lebewesen & a);
    void angriff(lebewesen & a,lebewesen & b);
    int kampf(lebewesen & a,lebewesen & b);
    void refresh(lebewesen & a);
    void lvup(lebewesen & a);
    void stats(lebewesen & a);

private:
    char name[50];
};

#endif // CLASSNPC_H_INCLUDED
