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
            for (int i = 0; i < citizens.Count; i++)
            {
                if (citizens[i].infected) Sum++;
            }
            return Sum;
        }
    }

    public int brokenNum
    {
        get
        {
            int Sum = 0;
            for (int i = 0; i < citizens.Count; i++)
            {
                if (citizens[i].isbroken) Sum++;
            }
            return Sum;
        }
    }
    public int deadNum
    {
        get
        {
            int Sum = 0;
            for (int i = 0; i < citizens.Count; i++)
            {
                if (citizens[i].dead) Sum++;
            }
            return Sum;
        }
    }
    public float money
    {
        get
        {
            float Sum = 0;
            for(int i=0;i< citizens.Count;i++)
            {
                Sum += citizens[i].money;
            }
            return Sum;
        }
    }
    public List<Citizen> citizens = new List<Citizen>();
    public List<Good> goods = new List<Good>();
}