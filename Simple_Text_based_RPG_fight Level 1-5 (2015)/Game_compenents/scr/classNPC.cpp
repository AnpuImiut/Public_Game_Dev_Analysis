#include <iostream>
#include <string.h>
#include <stdlib.h>
#include <fstream>


#include "../classNPC.h"
#include "../utils.h"

lebewesen::lebewesen(const char *dname)
{
    std::fstream x;
    x.open(dname);
    char a[100];
    x.getline(a,100);
    strcpy(name,a);
    x.getline(a,100);
    x.getline(a,100);
    s_HP = atoi(a);
    HP=s_HP;
    x.getline(a,100);
    Armor = atoi(a);
    x.getline(a,100);
    Dodge = atoi(a);
    x.getline(a,100);
    minDmg = atoi(a);
    x.getline(a,100);
    maxDmg = atoi(a);
}

lebewesen::~lebewesen() {};

int lebewesen::dodge_test(lebewesen & a)
{
    double zahl = zfllz(0, 100);
    if(a.Dodge >= zahl)
        return 0;
    else
        return 1;
}

void lebewesen::angriff(lebewesen & a,lebewesen & b)
{
    if(b.dodge_test(b))
    {
        double schd = (zfllz(a.minDmg,a.maxDmg));
        std::cout << "Damage : " << schd*(1-(b.Armor/100)) << "\n";
        schd = schd * (1-(b.Armor/100));
        b.HP = b.HP - schd;
    }
    else
        std::cout << "Ausgewichen\n";
}

int lebewesen::kampf(lebewesen & a,lebewesen & b)
{
    while(1)
    {
        warten();
        std::cout << std::string(20, '-') << "\n\n";
        std::cout << "Held greift Monster an\n\n";
        a.angriff(a,b);
        std::cout << "Monster HP : " << b.HP << "\n";
        if(!(a.HP >0 && b.HP >0))
            break;
        warten();
        std::cout << std::string(20, '-') << "\n\n";
        std::cout << "Monster greift Held an\n\n";
        b.angriff(b,a);
        std::cout << "Held HP : " << a.HP;
        if(!(a.HP >0 && b.HP >0))
            break;

    }
    if(a.HP <0)
        return 1;
    else
        return 0;
}

void lebewesen::refresh(lebewesen & a)
{
    HP=s_HP;
}

void lebewesen::lvup(lebewesen & a)
{
    int lvuparay[4];
    int counter = 0;
    int max_points = 5;
    std::cout << "\nLvUp-Time\n" << "You are allowed to spend 5 points\n";
    std::cout << "1 Point in HP means +5 HP\n1 Point in Armor means +2 Armor\n";
    std::cout << "1 Point in Dodge means +1 Dodge\n1 Point in Dmg means +3 Dmg";
    std::cout << "\nHow many Points for HP\n";
    while(true) {
        std::cin >> lvuparay[0];
        if(counter + lvuparay[0] > max_points) {
            std::cout << "Maximum exceeded\n";
        }
        else {
            break;
        }
    }
    counter = counter + lvuparay[0];

    std::cout << "\nHow many Points for Armor\n";
    while(true) {
        std::cin >> lvuparay[1];
        if(counter + lvuparay[1] > max_points) {
            std::cout << "Maximum exceeded\n";
        }
        else {
            break;
        }
    }
    counter = counter + lvuparay[1];

    std::cout << "\nHow many Points for Dodge\n";
    while(true) {
        std::cin >> lvuparay[2];
        if(counter + lvuparay[2] > max_points) {
            std::cout << "Maximum exceeded\n";
        }
        else {
            break;
        }
    }
    counter = counter + lvuparay[2];

    std::cout << "\nHow many Points for Dmg\n";
    while(true) {
        std::cin >> lvuparay[3];
        if(counter + lvuparay[3] > max_points) {
            std::cout << "Maximum exceeded\n";
        }
        if(counter + lvuparay[3] < max_points) {
            std::cout << "Spend all points!\n";
        }
        if(counter + lvuparay[3] == max_points) {
            break;
        }
    }

    a.s_HP = a.s_HP + lvuparay[0] * 5;
    a.Armor = a.Armor + lvuparay[1] * 2;
    a.Dodge = a.Dodge + lvuparay[2] * 2;
    a.minDmg = a.minDmg + lvuparay[3] * 3;
    a.maxDmg = a.maxDmg + lvuparay[3] * 3;
    std::cout << "\n" <<std::string(20, '-') << "\n\n";
    a.stats(a);
    std::cout << "\n\n" <<std::string(20, '-') << "\n";
}

void lebewesen::stats(lebewesen & a)
{
    std::cout << "Name : " << name << "\n";
    std::cout << "HP : " << s_HP << "\n";
    std::cout << "Armor : " << Armor << "\n";
    std::cout << "Dodge : " << Dodge << "\n";
    std::cout << "Dmg : " << minDmg << " - " << maxDmg;
}

void warten()
{
    std::cin.get();
}
