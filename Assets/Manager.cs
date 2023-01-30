using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
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

    //List<Person_withoutTransf> persons = new List<Person_withoutTransf>();

    void Start()
    {
        Debug.Log(!false);
        for (int i = 0; i < 1000; i++)
        {
            Person_withoutTransf person = new Person_withoutTransf();
            person.Start2();
        }
    }

    private void FixedUpdate()
    {
        for (int j = 0; j < Data.persons2.Count; j++)
        {
            Data.persons2[j].FixedUpdate2();
        }
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
        foreach (var a in Data.persons2)//模拟后期出现总和数值过大，计算TOTALMONEY失效
        {
            now = now + a.money;
        }
        Data.TotalMoney = now - before;

        infnum.text = Data.infledNum.ToString();
        bronum.text = Data.brokenNum.ToString();
        dednum.text = Data.deadNum.ToString();
        if(Data.TotalMoney!=0)
        moneynum.text = Data.TotalMoney.ToString();
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
