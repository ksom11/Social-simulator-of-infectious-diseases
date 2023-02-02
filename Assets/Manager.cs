using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public Text infnum;
    public Text bronum;
    public Text dednum;
    public Text moneynum;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Slider slider1;
    [SerializeField]
    Slider slider2;
    [SerializeField]
    Slider slider3;

    public float before = 0;
    public float now = 0;

    City cityA = new City();
    City cityB = new City();

    private void Awake()
    {
        // Only one instance of debug console is allowed
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Person_withoutTransf person = new Person_withoutTransf();
            person.Start2(cityA.persons);
        }
        for (int i = 0; i < 100; i++)
        {
            Person_withoutTransf person = new Person_withoutTransf();
            person.Start2(cityB.persons);
        }
        Good Food = new Good(); Food.Number = 0;
        cityA.goods.Add(Food);
        cityB.goods.Add(Food);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < cityA.persons.Count; i++)
        {
            cityA.persons[i].FixedUpdate2(cityA.persons);
        }
        for (int i = 0; i < cityA.persons.Count; i++)
        {
            cityA.persons[i].FixedUpdate2(cityB.persons);
        }
        if (cityA.goods[0].Number == 0) cityA.goods[0].Price *= 1.1f;
        if (cityA.goods[0].Number > 100) cityA.goods[0].Price *= 0.9f;
        cityA.goods[0].Number += 100;

        if (cityB.goods[0].Number == 0) cityB.goods[0].Price *= 1.1f;
        if (cityB.goods[0].Number > 100) cityB.goods[0].Price *= 0.9f;
        cityB.goods[0].Number += 100;
    }

    // Update is called once per frame
    void Update()
    {
        SystemCoefficients.MovingRange = ValToK(slider3.value);
        SystemCoefficients.K_infectdes = ValToK(slider2.value);
        SystemCoefficients.K_cost = ValToK(slider1.value);
        SystemCoefficients.K_make = ValToK(slider.value);

        before = now;
        now = 0;
        //City_1.concat
        foreach (var a in cityA.persons)//模拟后期出现总和数值过大，计算TOTALMONEY失效
        {
            now = now + a.money;
        }
        foreach (var a in cityB.persons)
        {
            now = now + a.money;
        }
        CityData.TotalMoney = now - before;

        infnum.text = CityData.infledNum.ToString();
        bronum.text = CityData.brokenNum.ToString();
        dednum.text = CityData.deadNum.ToString();
        if(CityData.TotalMoney!=0)
        moneynum.text = CityData.TotalMoney.ToString();
    }

    float ValToK(float val)
    {
        if(val<=0.5)
        {
            return val * 1.8f + 0.1f;
        }
        else
        {
            return val * 18 - 8;
        }
    }
}
