using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class EnemiesHealthDamage : MonoBehaviour
{
    public float health;
    public float damage;
    public int gearDrop;
    public GameObject gearSpreadPrefab;
    public GameObject damagePopup;
    
    private  AudioManagerScript audioMScript;
    
    private void Start()
    {
        audioMScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
    }
    public void TakeDamage(float dmg, bool crit, Color critColor) // On damage taken
    {
        health -= dmg;

        

        var popup = Instantiate(damagePopup, transform.position, Quaternion.Euler(Vector3.zero)); // Spawns damage popup
        popup.GetComponentInChildren<Text>().color = crit? critColor: popup.GetComponentInChildren<Text>().color; // Changes color if its critical hit
        popup.GetComponentInChildren<Text>().text = dmg.ToString();

        if (health <= 0)
        {
            OnDeath(true);
        }
    }
    public void OnDeath(bool dropGears) // Invokes on enemy's death
    {
        CancelInvoke();
        Destroy(gameObject);

        audioMScript.PlayEnemyExplode(transform.position);
        if (dropGears)
        {
            var spread = Instantiate(gearSpreadPrefab, transform.position, gearSpreadPrefab.transform.rotation); // Invokes Gear drop
            spread.GetComponent<GearSpread>().SpreadGears(gearDrop);
        }
    }

}
