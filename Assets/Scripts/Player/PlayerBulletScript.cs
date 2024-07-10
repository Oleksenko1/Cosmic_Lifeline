using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBulletScript : MonoBehaviour
{
    [NonSerialized] public GameObject firingShip;
    [NonSerialized] public float damage;
    [NonSerialized] public bool crit;
    [SerializeField] private Color critColor;

    public static event Action OnEnemyHitted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Projectile") || collision.CompareTag("Collectible")) // Doesn't destroy on collision with these objects
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemiesHealthDamage>().TakeDamage(damage, crit, critColor);

            OnEnemyHitted?.Invoke(); // Activates event on enemy getting hit
        }
        Destroy(gameObject);
    }
}
