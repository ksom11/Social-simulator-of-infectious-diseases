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

    public float before = 0;
    public float now = 0;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        before = now;
        now = 0;
        foreach (var a in Data.persons)//模拟后期出现总和数值过大，计算TOTALMONEY失效
        {
            /*
            if (!a.dead)
            {
                if(!a.isolated)
                {
                    now = now + a.make - a.cost;
                }
                else
                {
                    now = now  - a.cost;
                }
                //
            }*/
            now = now + a.money;
        }
        Data.TotalMoney = now - before;

        infnum.text = Data.infledNum.ToString();
        bronum.text = Data.brokenNum.ToString();
        dednum.text = Data.deadNum.ToString();
        moneynum.text = Data.TotalMoney.ToString();
    }
}
