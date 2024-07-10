using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Text HealthbarText;
    public Image healthImage;
    private GameObject playerGO;

    private float maxHealth = 0;
    private float currentHealth;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            ChangeHealthcount(currentHealth, maxHealth);
        }
    }

    // Runs at the start of a program
    private void Start()
    {
        playerGO = GameObject.Find("Player");
        maxHealth = playerGO.GetComponent<PlayerController>().maxHealth;
        CurrentHealth = playerGO.GetComponent<PlayerController>().HealthPoints;
    }
    public void SetCurrentHealth(float cur)
    {
        CurrentHealth = cur;
    }
    private void ChangeHealthcount(float cur, float max) // Changes health in UI
    {
        HealthbarText.text = cur.ToString("F1") + " / " + max.ToString("F1");
        healthImage.fillAmount = cur/maxHealth;
    }
}
