using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconPathwayScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<GameObject> beaconsList = new List<GameObject>();
    private Transform destination;

    public Transform origin;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        foreach(GameObject bo in GameObject.FindGameObjectsWithTag("Beacon")) // Adds all of the beacons to the list
        {
            beaconsList.Add(bo);
        }

        BeaconTriggerScript.OnRecharge += ChangeDestination;
        ChangeDestination();
    }

    private void LateUpdate()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
    }
    public void ChangeDestination()
    {
        float temp = 9999999;
        Transform newDestination = null;


        for(int x = 0; x < beaconsList.Count;) // Loop for finding unrecharged beacon that are closest to a player
        {
            if(beaconsList[x].GetComponentInParent<BeaconTriggerScript>().IsRecharged() == false)
            {
                float distance = Vector2.Distance(origin.position, beaconsList[x].transform.position);
                if (temp > distance)
                {
                    temp = distance;
                    newDestination = beaconsList[x].transform;
                }
                x++;
            }
            else
            {
                beaconsList.RemoveAt(x); // Removes beacon from list if it's already recharged
                continue;
            }
        }

        if (newDestination == null) 
        {
            Destroy(gameObject); // Destroy's this GameObject and stop method execution if all of the beacons are Recharged
            return;
        }
        destination = newDestination;
    }
}
