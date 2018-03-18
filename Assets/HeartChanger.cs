using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartChanger : MonoBehaviour {

    Image myIm;
    float per = 0.1f;
    float dist;

	void Start () {
        myIm = GetComponent<Image>();
        myIm.fillAmount = 0f;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(filling());

        else if (Input.GetMouseButtonDown(1))
            StartCoroutine(unfilling());
    }

    IEnumerator filling()
    {
        dist = 1 - per;
        while (myIm.fillAmount < 1f)
        {
            myIm.fillAmount = Mathf.Lerp(myIm.fillAmount, 1f, 1 / dist);
            dist = 0.9f * dist;
            yield return new WaitForSeconds(1f);
        }

    }

    IEnumerator unfilling()
    {
        dist = 1 - per;
        while (myIm.fillAmount > 0f)
        {
            myIm.fillAmount = Mathf.Lerp(myIm.fillAmount, 0f, 1 / dist);
            dist = 0.9f * dist;
            yield return new WaitForSeconds(1f);
        }
    }
}
