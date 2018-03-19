using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AHeartBarTest : MonoBehaviour {

    public GameObject heartPrefab;
    public Transform heartBarHolder;
    public Button takeDamageButton;
    public Button HealButton;

    HeartBar bar;
    Heart heart;

	void Start () {
        bar = new HeartBar(30, 4);

        takeDamageButton.onClick.AddListener(() => bar.TakeDamage(10));
        HealButton.onClick.AddListener(() => bar.Heal(5));

        int i = 0;
        foreach(var h in bar.list)
        {
            GameObject clone = Instantiate(heartPrefab, heartBarHolder.transform);
            RectTransform rect = clone.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(i * 60, 0, 0);
            h.foregroundImage = clone.transform.Find("Background/Foreground").GetComponent<Image>();
            h.foregroundImage.fillAmount = (float)h.currentHealth / h.maxHealth;
            i++;
        }

    }
}

internal class Heart
{
    public int maxHealth;
    public int currentHealth;

    public Image foregroundImage{get; set;}

    public Heart(int _currentHealth, int _maxHealth)
    {
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
    }
}

internal class HeartBar
{
    public List<Heart> list;

    public HeartBar(int totalHealth, int healthPerHeart)
    {
        list = new List<Heart>();
        int remainHealth = totalHealth;
        while(remainHealth > 0)
        {
            if(remainHealth < healthPerHeart)
                list.Add(new Heart(remainHealth, healthPerHeart));
            else
                list.Add(new Heart(healthPerHeart, healthPerHeart));

            remainHealth -= healthPerHeart;
        }
    }

    public int CurrentHealth
    {
        get { return list.Sum<Heart>(h => h.currentHealth); } 
    }

    internal int GetActiveHeartCount()
    {
        return list.FindAll(h => h.currentHealth > 0).Count;
    }

    internal void TakeDamage(int amount)
    {
        int lastIndex = list.FindLastIndex(h => h.currentHealth > 0);
        int i = lastIndex;

        while (i >= 0 && amount > 0)
        {
            if(amount <= list[i].currentHealth)
            {
                list[i].currentHealth -= amount;
                amount = 0;
            }
            else
            {
                amount -= list[i].currentHealth;
                list[i].currentHealth -= list[i].currentHealth;
            }
            list[i].foregroundImage.fillAmount = (float)list[i].currentHealth / list[i].maxHealth;
            i--;
        }
    }

    internal void Heal(int amount)
    {
        int firstIndex = list.FindIndex(h => h.maxHealth - h.currentHealth > 0);
        int i = firstIndex;

        while(i != -1 && i < list.Count && amount > 0)
        {
            if(amount <= list[i].maxHealth - list[i].currentHealth)
            {
                list[i].currentHealth += amount;
                amount = 0;
            }
            else
            {
                amount -= list[i].maxHealth - list[i].currentHealth;
                list[i].currentHealth += list[i].maxHealth - list[i].currentHealth;
            }
            list[i].foregroundImage.fillAmount = (float)list[i].currentHealth / list[i].maxHealth;
            i++;
        }
    }
}