using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartChanger : MonoBehaviour {

    Image myIm;
    float per = 0.1f;
    float dist;

    float hp = 0f;
    public float dam = 0.25f;

	void Start () {
        myIm = GetComponent<Image>();
        myIm.fillAmount = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hp += dam;
            StartCoroutine(filling());
        }

        else if (Input.GetMouseButtonDown(1))
        {
            hp -= dam;
            StartCoroutine(unfilling());
        }
    }

    IEnumerator filling()
    {
        if (hp > 1f)
            hp = 1f;
        dist = 1 - per;

        while (myIm.fillAmount < 1f)
        {
            myIm.fillAmount = Mathf.Lerp(myIm.fillAmount, hp, 1 / dist * per);
            dist = (1f - per) * dist;
            yield return null;
        }
    }

    IEnumerator unfilling()
    {
        if (hp < 0f)
            hp = 0f;
        dist = 1 - per;
  
        while (myIm.fillAmount > 0f)
        {
            myIm.fillAmount = Mathf.Lerp(myIm.fillAmount, hp, 1 / dist * per);
            dist = (1f - per) * dist;
            yield return null;
        }
    }
}
