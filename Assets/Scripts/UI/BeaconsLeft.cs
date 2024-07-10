using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeaconsLeft : MonoBehaviour
{
    public Text beaconsText;

    private int maxBeacons;
    private int leftBeacons;

    private void Start()
    {
        maxBeacons = GameObject.FindGameObjectsWithTag("Beacon").Length;
        leftBeacons = maxBeacons;
        BeaconTriggerScript.OnRecharge += RechargeBeacon;
        WriteBeacons();
    }
    
    public void RechargeBeacon() // Reduces amount of left beacons
    {
        leftBeacons--;
        WriteBeacons();
    }
    private void WriteBeacons() // Writes info in UI
    {
        beaconsText.text = $"Beacons left: {leftBeacons}/{maxBeacons}";
    }
}
