using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartStatus : MonoBehaviour {

    Image Myheart;
    Animator anim;
    public Animation anis;

    int hp = 0;
    int min = 0;
    int max = 4;

	void Start () {
        Myheart = GetComponent<Image>();
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            hp--;
            anis["Hp1"].speed = -2f;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            hp++;
        }

        if (hp < 0)
            hp = 0;
        if (hp > 4)
            hp = 4;

        anim.SetInteger("LifePer", hp);
    }
}
