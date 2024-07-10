using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BeaconTriggerScript : MonoBehaviour
{
    public static event Action OnRecharge;

    public GameObject flareObj;
    public GameObject beam;
    public Image reloadImage;
    public float rechargeTime = 5f;
    public string nameOfTarget;

    private bool isInZone = false;
    private bool isRecharged = false;
    private AudioSource audioSource;
    private AudioManagerScript audioMScript;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioMScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();

        audioSource.clip = audioMScript.beaconChargingSFX;
        audioSource.volume = audioMScript.beaconChargingVolume;
    }
    private void Start()
    {
        flareObj.GetComponent<Renderer>().material.color = Color.red;
    }

    private void Update()
    {
        if(isInZone && !isRecharged)
        {
            reloadImage.fillAmount = Mathf.MoveTowards(reloadImage.fillAmount, 1, 1/rechargeTime * Time.deltaTime);
            if (reloadImage.fillAmount == 1)
            {
                onFullRecharge();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isRecharged) // Start recharging on player enter
        {
            audioMScript.PlayBeaconChargeSFX(audioSource, true);

            flareObj.GetComponent<Renderer>().material.color = Color.yellow;
            isInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isRecharged) // Stop recharging
        {
            audioMScript.PlayBeaconChargeSFX(audioSource, false);

            flareObj.GetComponent<Renderer>().material.color = Color.red;
            isInZone = false;
            reloadImage.fillAmount = 0;
        }
    }

    private void onFullRecharge()
    {
        isRecharged = true;
        OnRecharge?.Invoke(); // Activates event
        Destroy(reloadImage);
        flareObj.GetComponent<Renderer>().material.color = Color.green;

        audioMScript.PlayOnBeaconFullRecharge(audioSource);

        var beamGO = Instantiate(beam, new Vector3(0, 0, 0), beam.transform.rotation); // Creates beam towards target after recharge
        beamGO.GetComponent<BeamScript>().startPos = transform;
        beamGO.GetComponent<BeamScript>().finishPos = GameObject.Find(nameOfTarget).transform;

        //GameObject.Find("Text - Beacons left").GetComponent<BeaconsLeft>().RechargeBeacon();
    }
    public bool IsRecharged()
    {
        return isRecharged;
    }
}
