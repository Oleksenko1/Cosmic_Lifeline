using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShooterScript : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float reloadTime;
    public float shotSpeed;
    public float range;
    public GameObject bulletPrefab;

    private GameObject playerGO;
    private Rigidbody2D rb2D;
    private AudioSource audioSource;
    private EnemiesHealthDamage EHD; // Enemies health and damage
    private AudioManagerScript AudioMScript;

    private Vector3 moveDirection;
    private void Awake()
    {
        playerGO = GameObject.Find("Player");
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        EHD = GetComponent<EnemiesHealthDamage>();
        AudioMScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(Shooting), reloadTime, reloadTime);
    }
    private void Update()
    {
        moveDirection = playerGO.transform.position - transform.position;
        moveDirection.z = 0;
        
        // Rotates enemy in direction of player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) - 90f, rotationSpeed * Time.deltaTime)));
        
        if(moveDirection.magnitude > 3)
        {
            rb2D.AddForce(moveDirection.normalized * Time.deltaTime * moveSpeed);
        }
        else
        {
            // Stops enemy if it's close to a player
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, Vector2.zero, moveSpeed * Time.deltaTime * 0.06f);
        }
        
    }
    void Shooting() // Method for shooting
    {
        if (moveDirection.magnitude < range) // Shoots only of player in range
        {
            AudioMScript.PlayEnemyShot(audioSource);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            bullet.GetComponent<EnemyBulletScript>().firingShip = gameObject;
            bullet.GetComponent<EnemyBulletScript>().damage = EHD.damage;

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shotSpeed, ForceMode2D.Impulse);
        }
    }
    
}
