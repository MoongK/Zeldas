using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartChanger : MonoBehaviour {

    Image myIm;

	void Start () {
        myIm = GetComponent<Image>();
        StartCoroutine(filling());
        myIm.fillAmount = 1f;
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
            StartCoroutine(filling());
        else if (Input.GetMouseButtonDown(1))
            StartCoroutine(unfilling());
	}

    IEnumerator filling()
    {
        while (myIm.fillAmount > 0f)
            myIm.fillAmount = Mathf.Lerp(myIm.fillAmount, 0f, 0.1f);

        yield return null;
    }
    IEnumerator unfilling()
    {
        while(myIm.fillAmount <1f)
            myIm.fillAmount = Mathf.Lerp(myIm.fillAmount, 1f, 0.1f);

        yield return null;
    }
}
