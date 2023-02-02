using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public int infledNum
    {
        get
        {
            int Sum = 0;
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].infected) Sum++;
            }
            return Sum;
        }
    }

    public int brokenNum
    {
        get
        {
            int Sum = 0;
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].isbroken) Sum++;
            }
            return Sum;
        }
    }
    public int deadNum
    {
        get
        {
            int Sum = 0;
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].dead) Sum++;
            }
            return Sum;
        }
    }
    public float money
    {
        get
        {
            float Sum = 0;
            for(int i=0;i<persons.Count;i++)
            {
                Sum += persons[i].money;
            }
            return Sum;
        }
    }
    public List<Person_withoutTransf> persons = new List<Person_withoutTransf>();
    public List<Good> goods = new List<Good>();
}