using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_withoutTransf
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

    public string job = "";

    public void Start2()
    {
        Data.persons2.Add(this);
        randset();
        randcread();
    }

    public void FixedUpdate2()
    {
        if (!dead)
        {
            //if(this.transform.position.x<500&& this.transform.position.x > -500 && this.transform.position.z < 500 && this.transform.position.z > -500)
            //{
            //Debug.Log("runing");
            if (isolated)
            {
                //GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                isolatedtime++;
            }

            if (isolatedtime > 1000)
            {
                //GetComponent<Renderer>().material.color = new Color(0, 1, 0);
                isolated = false;
                infected = false;
                isolatedtime = 0;
            }

            if (isbroken)
            {
                //GetComponent<Renderer>().material.color = new Color(0, 0, 1);
                isbrokentime++;
            }

            if (isbrokentime > 1000)
            {
                //Destroy(this.gameObject);
                //this.gameObject.SetActive(false);
                dead = true;
                Data.deadNum++;
            }


            if (!isolated)
            {
                pmove(this);
            }


            if (!isolated)
            {
                if (getinf(this))
                {
                    infected = true;
                    Data.infledNum++;
                    isolate();
                }
            }


            if (!isolated)
            {
                makemoney();
            }
            costmoney();
            randdead(this);
        }
        else
        {
            //GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        }
    }

    void randdead(Person_withoutTransf P)
    {
        if (infected)
        {
            if (UnityEngine.Random.Range(0, 10000) == 50)
            {
                dead = true;
                Data.persons2.Remove(P);
                Data.deadNum++;
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

    void pmove(Person_withoutTransf person)
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

    void costmoney()
    {
        if (!isbroken)
        {
            if (money > cost * SystemCoefficients.K_cost)
            {
                money = money - cost * SystemCoefficients.K_cost;
            }

            else
            {
                isbroken = true;
                Data.brokenNum++;
            }
        }
    }

    void isolate()
    {
        isolated = true;
    }

    bool getinf(Person_withoutTransf person)
    //void getperson()
    {
        //GameObject[] obj = FindObjectOfType(typeof( GameObject)) as GameObject[];
        /*
        List<GameObject> ob = new GameObject();
        foreach(GameObject i in GameObject.Find("Person"))
        {

        }
        */
        //GameObject.Find("Person");
        //GameObject[] all = { };
        //var all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        //all.Initialize();
        var p = person;//.GetComponent<Person>();
        foreach (var item in Data.persons2)
        {
            //item.name.Contains("Cube (")&&
            if (item != person)
            {
                var i = item;//.GetComponent<Person>();
                if (p.dead == false && i.dead == false)
                {
                    /*
                    var v = person.transform.position;
                    var vv = item.transform.position;
                    var des = vv - v;
                    */
                    //person.transform.position- item.transform.position
                    if ((person.position - item.position).sqrMagnitude < infectdes * SystemCoefficients.K_infectdes)
                    {
                        //Debug.Log("des:" + magnitude(des));
                        //Debug.Log(person.name + " : " + item.name);
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

        //自身各分量平方运算.
        /*
        float X = v.x * v.x;
        float Y = v.y * v.y;
        float Z = v.z * v.z;
        */
        //return v.x;
        //return v.x + v.y + v.z;
        return v.sqrMagnitude;
        //return Mathf.Sqrt(X + Y + Z);//开根号,最终返回向量的长度/模/大小.


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
