using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldScript : MonoBehaviour
{
    [SerializeField] private Image shieldBar;
    [SerializeField] private Text shieldText;

    private float maxShield;
    private float shieldRegen;
    private float shieldRechargeTime;
    private float shieldActivatingTime;
    private float currentPoints;

    private float rechargeTime;
    private void Start()
    {
        currentPoints = maxShield;
        UpdateShieldUI();
    }
    private void Update()
    {
        if(currentPoints < maxShield && rechargeTime <= Time.time)
        {
            RechargeShield();
        }
    }
    public float GetCurrentPoints()
    {
        return currentPoints;
    }
    public void DamageShield(float dmg)
    {
        RestartActivating();

        currentPoints -= dmg;
        currentPoints = currentPoints < 0 ? 0 : currentPoints; // Sets shield point to zero if they're lower than zero

        UpdateShieldUI();
    }
    public float ShieldOverDamage(float dmg) // Returns over damage that was done to a shield
    {
        float overDamage = dmg - currentPoints;
        overDamage = overDamage > 0 ? overDamage : 0;
        return overDamage;
    }
    public void RestartActivating() // Restarts activation on getting hit
    {
        rechargeTime = Time.time + shieldActivatingTime; // Cooldown, after getting hit
    }
    private void RechargeShield()
    {
        rechargeTime = Time.time + shieldRechargeTime;

        currentPoints += shieldRegen;
        currentPoints = currentPoints > maxShield ? maxShield : currentPoints;

        UpdateShieldUI();
    }
    private void UpdateShieldUI() // Updates shiled UI indicators
    {
        shieldText.text = currentPoints.ToString("F1") + " / " + maxShield.ToString("F1");
        shieldBar.fillAmount = currentPoints / maxShield;
    }
    public void SetShieldParametrs(float max, float regen, float rechargeSpeed, float activatingSpeed)
    {
        maxShield = max;
        shieldRegen = regen;
        shieldRechargeTime = rechargeSpeed;
        shieldActivatingTime = activatingSpeed;
    }
}
