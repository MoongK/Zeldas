using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shining : MonoBehaviour {

    public RectTransform HeartField;
    List<GameObject> GaugeIm = new List<GameObject>();
    List<GameObject> ShineIm = new List<GameObject>();
    bool ShineReady = false;
    float gaugeDir;

    int s_damage, s_heal;
    int lastHeart;
    float dist;

    private void Update()
    {
        s_damage = GetComponent<HeartMgr>().damage;
        s_heal = GetComponent<HeartMgr>().heal;

        if (GetComponent<HeartMgr>().isStarted && !ShineReady)
        {
            for (int i = 0; i < HeartField.childCount; i++)
            {
                GaugeIm.Add(HeartField.GetChild(i).Find("Gauge").gameObject);
                ShineIm.Add(HeartField.GetChild(i).Find("Shine").gameObject);
            }
            lastHeart = HeartField.childCount - 1;
            ShineReady = true;
        }


        if (Input.GetMouseButtonDown(0))
            DamageShine(s_damage);
        if (Input.GetMouseButtonDown(1))
            HealShine(s_heal);
    }

    public void DamageShine(int _damage)
    {
        for (int i = lastHeart; i > 0; i--)
        {
            if (_damage == 0)
                break;

            while (HeartField.GetChild(i).Find("Shine").GetComponent<Image>().fillAmount < 1f)
            {
                if (_damage == 0)
                    break;
                else
                {
                    HeartField.GetChild(i).Find("Shine").GetComponent<Image>().fillAmount += 0.25f;
                    _damage--;
                }
            }
            lastHeart = i;
        }

        for (int i = 0; i < GaugeIm.Count; i++)
        {
            GameObject redHeart = GaugeIm[i];
            GameObject whiteHeart = ShineIm[i];

            StartCoroutine(shiner(redHeart, whiteHeart));
        }

        print("LastHeart : " + lastHeart);
    }

    public void HealShine(int _heal)
    {
        for (int i = 0; i < GaugeIm.Count; i++)
        {
            GameObject redHeart = GaugeIm[i];
            GameObject whiteHeart = ShineIm[i];

            StartCoroutine(shiner(redHeart, whiteHeart));
        }
    }

    IEnumerator shiner(GameObject _red, GameObject _white)
    {
        dist = 1 - 0.1f;

        yield return new WaitForSeconds(0.5f);
        while (_white.GetComponent<Image>().fillAmount != 0f)
        {
            _white.GetComponent<Image>().fillAmount = Mathf.Lerp(_white.GetComponent<Image>().fillAmount, 0f, 1 / dist * 0.1f);
            dist = (1 - 0.1f) * dist;
            print("whiling~");
            yield return null;
        }

    }
}