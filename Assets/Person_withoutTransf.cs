using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen
{
    public Vector3 position;
    public bool dead = false;//是否死亡
    public bool infected = false;//是否感染
    public bool isolated = false;//是否隔离
    public bool isbroken = false;//是否破产
    public int isolatedtime = 0;//被隔离时间
    public int isbrokentime = 0;//破产时间
    public int yearsold = 0;//年龄
    public float money = 0;//存款
    public float make = 0;//收入
    public float cost = 0;//花销
    float infectdes = 2;//感染距离
    public float hungery = 100;//饥饿值

    public string job = "";

    public void Start2(City city)
    {
        city.citizens.Add(this);
        randset();
        randcread();
    }

    public void FixedUpdate2(City city)
    {
        if (!dead)
        {
            if (isolated)
            {
                isolatedtime++;
            }

            if (isolatedtime > 1000)
            {
                isolated = false;
                infected = false;
                isolatedtime = 0;
            }

            if (isbroken)
            {
                isbrokentime++;
            }

            if (isbrokentime > 1000)
            {
                dead = true;
            }


            if (!isolated)
            {
                pmove(this);
            }


            if (!isolated)
            {
                if (getinf(this,city.citizens))
                {
                    infected = true;
                    isolate();
                }
            }

            if (!isolated)
            {
                makemoney();
            }
            costmoney(city.goods[0]);
            randdead(this,city.citizens);
        }
    }

    void randdead(Citizen P,List<Citizen> citizens)
    {
        if (infected)
        {
            if (UnityEngine.Random.Range(0, 10000) == 50)
            {
                dead = true;
                citizens.Remove(P);
            }
        }
    }

    void randcread()
    {
        var v = this.position;
        yearsold = UnityEngine.Random.Range(1, 101);
        money = UnityEngine.Random.Range(10000, 1000000);
        //Debug.Log(1 / magnitude(v));
        cost = UnityEngine.Random.Range(50, 1000);
        make = UnityEngine.Random.Range(100, 10000);
    }

    void pmove(Citizen person)
    {
        switch (UnityEngine.Random.Range(0, 5))
        {
            case 0:
                {
                    var v = person.position;
                    v.x = Clamp(v.x + SystemCoefficients.MovingRange, -50, 50);
                    person.position = v;
                    break;
                }
            case 1:
                {
                    var v = person.position;
                    v.x = Clamp(v.x - SystemCoefficients.MovingRange, -50, 50);
                    person.position = v;
                    break;
                }
            case 2:
                {
                    var v = person.position;
                    v.z = Clamp(v.z + SystemCoefficients.MovingRange, -50, 50);
                    person.position = v;
                    break;
                }
            case 3:
                {
                    var v = person.position;
                    v.z = Clamp(v.z - SystemCoefficients.MovingRange, -50, 50);
                    person.position = v;
                    break;
                }
        }
    }

    float Clamp(float value, float min, float max)
    {
        if (value > max) return max;
        if (value < min) return min;
        return value;
    }

    void makemoney()
    {
        money = money + make * SystemCoefficients.K_make;
    }

    void costmoney(Good food)
    {
        if (!isbroken)
        {
            hungery--;
            if(hungery<100)
            {
                if(food.Number>0&&money>=food.Price)
                {
                    food.Number--;
                    money -= food.Price;
                    hungery = 100;
                }
            }

            if (money > cost * SystemCoefficients.K_cost)
            {
                money = money - cost * SystemCoefficients.K_cost;
            }

            else
            {
                isbroken = true;
                //CityData.brokenNum++;
            }
        }
    }



    void isolate()
    {
        isolated = true;
    }

    bool getinf(Citizen person,List<Citizen> citizens)
    {
        var p = person;//.GetComponent<Person>();
        foreach (var item in citizens)
        {
            if (item != person)
            {
                var i = item;
                if (p.dead == false && i.dead == false)
                {
                    if ((person.position - item.position).sqrMagnitude < infectdes * SystemCoefficients.K_infectdes)
                    {
                        if (p.infected == true || i.infected == true)
                        {
                            return true;
                        }
                    }
                }


            }
        }
        return false;
    }

    public float magnitude(Vector3 v)
    {
        return v.sqrMagnitude;
    }

    void randset()
    {
        var v = this.position;
        v.x = UnityEngine.Random.Range(-50, 50);
        v.z = UnityEngine.Random.Range(-50, 50);
        this.position = v;
        if(Random.Range(0,10)==0)
        {
            infected = true;
        }
    }
}
