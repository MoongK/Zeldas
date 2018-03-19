using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shining : MonoBehaviour {

    public RectTransform HeartField;
    ArrayList GaugeIm = new ArrayList();
    ArrayList ShineIm = new ArrayList();
    bool ShineReady = false;

    int s_damage, s_heal;



    private void Update()
    {
        s_damage = GetComponent<HeartMgr>().damage;
        s_heal = GetComponent<HeartMgr>().heal;

        if (GetComponent<HeartMgr>().isStarted)
        {
            for (int i = 0; i < HeartField.childCount; i++)
            {
                GaugeIm.Add(HeartField.GetChild(i).Find("Gauge"));
                ShineIm.Add(HeartField.GetChild(i).Find("Shine"));
            }

            ShineReady = true;
        }

        if (ShineReady)
        {
            if (Input.GetMouseButtonDown(0))
                DamageShine(s_damage);
            if (Input.GetMouseButtonDown(1))
                HealShine(s_heal);
        }

    }

    public void DamageShine(int _damage)
    {

    }

    public void HealShine(int _heal)
    {

    }
}
