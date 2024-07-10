using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectorScript : MonoBehaviour
{
	public static event Action OnGearCollected; 
	public Text gearCounter;
    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Collectible")) // On collectible pickUp
		{
			ICollectible collectible = collision.gameObject.GetComponent<ICollectible>();
			collectible.Collect();

			OnGearCollected?.Invoke(); // Calling an event

			gearCounter.GetComponent<GearCounter>().AddGears(1);
		}
	}
}
