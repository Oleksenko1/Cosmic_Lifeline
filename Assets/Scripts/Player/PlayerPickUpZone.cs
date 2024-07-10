using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Gear>(out Gear gear))
        {
            gear.SetTarget(transform.parent.position);
        }
    }
}
