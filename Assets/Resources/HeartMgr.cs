using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMgr : MonoBehaviour {

    // Start Setting =========
    public Object heart;
    public int totalHp;
    public RectTransform HeartField;
    public bool isStarted = false;
    // ======================

    public int damage;
    public int heal;
    float currentHp;

    float dist;

	void Awake ()
    {
        totalHp = 32;
        currentHp = totalHp;
        damage = 7;
        heal = 5;
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
        int lastIndex = 0;
        int damageCounter = 0;
        Image redHeart, whiteHeart;

        if (currentHp == 0f)
            return;

        foreach(Transform ob in HeartField)
        {
            if (ob.Find("Gauge").GetComponent<Image>().fillAmount == 0f)
                break;
            lastIndex++;
        }

        for (int i = lastIndex - 1; i >= 0; i--)
        {
            redHeart = HeartField.GetChild(i).Find("Gauge").GetComponent<Image>();
            whiteHeart = HeartField.GetChild(i).Find("Shine").GetComponent<Image>();

            if (damageCounter == _damage)
                break;

            while (redHeart.fillAmount > 0f)
            {
                if (damageCounter == _damage)
                    break;
                else
                {
                    redHeart.fillAmount -= 0.25f;
                    damageDirectionCheck(redHeart.fillAmount, i);

                    whiteHeart.fillClockwise = false;
                    whiteHeart.fillAmount += 0.25f;

                    StartCoroutine(UnfillingShine(whiteHeart.gameObject));

                    damageCounter++;
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
        Image redHeart, whiteHeart;

        if (currentHp == totalHp)
            return;

        for (int i = 0; i < HeartField.childCount; i++)
        {
            if (HeartField.GetChild(i).Find("Gauge").GetComponent<Image>().fillAmount == 0f)
                break;
            lastIndex = i;
        }

        while (lastIndex < HeartField.childCount)
        {
            if (healCounter == _heal)
                break;

            redHeart = HeartField.GetChild(lastIndex).Find("Gauge").GetComponent<Image>();
            whiteHeart = HeartField.GetChild(lastIndex).Find("Shine").GetComponent<Image>();

            while (redHeart.fillAmount < 1f)
            {
                if (healCounter == _heal)
                    break;
                else
                {
                    healDirectionCheck(redHeart.fillAmount, lastIndex);
                    whiteHeart.fillClockwise = true;

                    redHeart.fillAmount += 0.25f;
                    whiteHeart.fillAmount += 0.25f;

                    StartCoroutine(UnfillingShine(whiteHeart.gameObject)); // UnfillShine
                    healCounter++;
                }
            }
            lastIndex++;
        }
        currentHp += _heal;
        currentHp = Mathf.Clamp(currentHp, 0f, totalHp);
        print("currentHp : " + currentHp);
    }

    private void damageDirectionCheck(float redFillAmount, int i)
    {
        Image whiteHeart = HeartField.GetChild(i).Find("Shine").GetComponent<Image>();
        whiteHeart.color = Color.white;

        if (redFillAmount == 0f)
            whiteHeart.fillOrigin = 2;

        else if (redFillAmount == 0.25f)
            whiteHeart.fillOrigin = 3;

        else if (redFillAmount == 0.5f)
            whiteHeart.fillOrigin = 0;

        else if (redFillAmount == 0.75f)
            whiteHeart.fillOrigin = 1;    
    }

    private void healDirectionCheck(float redFillAmount, int i)
    {
        Image whiteHeart = HeartField.GetChild(i).Find("Shine").GetComponent<Image>();
        whiteHeart.color = Color.green;

        if (redFillAmount == 0f)
            whiteHeart.fillOrigin = 3;

        else if (redFillAmount == 0.25f)
            whiteHeart.fillOrigin = 0;

        else if (redFillAmount == 0.5f)
            whiteHeart.fillOrigin = 1;

        else if (redFillAmount == 0.75f)
            whiteHeart.fillOrigin = 2;
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
            h = (GameObject)Instantiate(heart, HeartField);
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

    IEnumerator UnfillingShine(GameObject _white)
    {
        dist = 1 - 0.1f;

        yield return new WaitForSeconds(0.2f);
        while (_white.GetComponent<Image>().fillAmount != 0f)
        {
            _white.GetComponent<Image>().fillAmount = Mathf.Lerp(_white.GetComponent<Image>().fillAmount, 0f, 1 / dist * 0.1f);
            dist = (1 - 0.1f) * dist;
            yield return new WaitForSeconds(0.02f);
        }

    }
}