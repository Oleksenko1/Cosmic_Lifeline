using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [NonSerialized] public GameObject firingShip;
    [NonSerialized] public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == firingShip || collision.CompareTag("Projectile") || collision.CompareTag("Collectible")) // Doesn't destroy on collision with these objects
        {
            return;
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
