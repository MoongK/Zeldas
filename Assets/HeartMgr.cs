using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMgr : MonoBehaviour {

    // Start Setting ==========
    public Object heart;
    public int totalHp;
    public RectTransform HeartField;
    public bool isStarted = false;
    // ======================

    public int damage = 1;
    public int heal = 5;
    float currentHp;

	void Start ()
    {
        totalHp = 32;
        currentHp = totalHp;
	}
	
	void Update ()
    {
        if (!isStarted)
        {   // Start
            if (Input.GetKeyDown(KeyCode.P))
                StartCoroutine(MakeHeart(totalHp));
        }
        else
        {
            // Damage , Heal
            if (Input.GetMouseButtonDown(0))
                Damaged(damage);
            if (Input.GetMouseButtonDown(1))
                Healing(heal);
        }
	}

    private void Damaged(int _damage)
    {
        int aliveHeart = 0;
        int damageCounter = 0;

        if (currentHp == 0f)
            return;

        for(int i = 0; i< HeartField.childCount; i++)
        {
            foreach(Transform ob in HeartField.GetChild(i))
            {
                if (ob.GetComponent<Image>().fillAmount != 0f)
                    aliveHeart++;
            }
        }

        for (int i = aliveHeart - 1; i >= 0; i--)
        {
            {
                while (HeartField.GetChild(i).Find("Gauge").GetComponent<Image>().fillAmount > 0f)
                {
                    if (damageCounter == _damage)
                        break;
                    else
                    {
                        HeartField.GetChild(i).Find("Gauge").GetComponent<Image>().fillAmount -= 0.25f;
                        damageCounter++;
                    }
                }
            }
        }
        currentHp = currentHp - _damage;
        currentHp = Mathf.Clamp(currentHp, 0f, totalHp);
        print("currentHp : " + currentHp);
    }

    private void Healing(int _heal)
    {
        int healCounter = 0;
        int lastIndex = 0;
        GameObject lastHeart;

        if (currentHp == totalHp)
            return;

        for (int i = 0; i < HeartField.childCount; i++)
        {
            if (HeartField.GetChild(i).Find("Gauge").GetComponent<Image>().fillAmount == 0f)
                break;
            else
                lastIndex = i;
        }

        while (lastIndex < HeartField.childCount)
        {
            lastHeart = HeartField.GetChild(lastIndex).Find("Gauge").gameObject;

            while (lastHeart.GetComponent<Image>().fillAmount < 1f)
            {
                if (healCounter == _heal)
                    break;
                else
                {
                    lastHeart.GetComponent<Image>().fillAmount += 0.25f;
                    healCounter++;
                }
            }
            lastIndex++;
        }
        currentHp += _heal;
        currentHp = Mathf.Clamp(currentHp, 0f, totalHp);
        print("currentHp : " + currentHp);
    }





    IEnumerator MakeHeart(int myheartgauge)
    {
        GameObject h;
        ArrayList myHearts = new ArrayList();
        float fillGauge = myheartgauge;

        int numOfheart = myheartgauge / 4;
        int remainder = (totalHp % 4);
        int maxHp;

        if (remainder == 0)
            maxHp = numOfheart;
        else
            maxHp = numOfheart + 1;

        for (int i = 0; i < maxHp; i++)
        {
            h = Instantiate(heart, HeartField) as GameObject;
            h.transform.localPosition = new Vector3(i * 70f, 0f, 0f);
            myHearts.Add(h);
            yield return null;
        }

        foreach (GameObject ob in myHearts)
        {
            for (int i = 0; i < 4; i++)
            {
                if (fillGauge > 0f)
                {
                    GameObject ch = ob.transform.Find("Gauge").gameObject;
                    ch.GetComponent<Image>().fillAmount += 0.25f;
                    fillGauge--;
                }
                else
                    break;
            }
            yield return null;
        }
        isStarted = true;
    }
}