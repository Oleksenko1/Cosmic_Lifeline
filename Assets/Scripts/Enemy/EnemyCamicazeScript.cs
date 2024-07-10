using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCamicazeScript : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float dashPower;

    private bool deathDash = true;
    private Vector3 moveDirection;
    private AudioSource audioSource;

    private GameObject playerGO;
    private Rigidbody2D rb2D;
    private EnemiesHealthDamage EHD; // Enemies health and damage
    private AudioManagerScript AudioMScript;
    private void Awake()
    {
        playerGO = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        EHD = GetComponent<EnemiesHealthDamage>();

        AudioMScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
    }
    private void Update()
    {
        moveDirection = playerGO.transform.position - transform.position;
        moveDirection.z = 0;

        // Rotates enemy in direction of player
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) - 90f, rotationSpeed * Time.deltaTime)));

        
        if(moveDirection.magnitude > 5 && deathDash == false)// Recharge death dash ability
        {
            AudioMScript.PlayCamicazeRecharged(audioSource); // Plays SFX of recharged camicaze

            deathDash = true;
        }

        if (moveDirection.magnitude > 3 && deathDash == true) // Basic movement
        {
            rb2D.AddForce(moveDirection.normalized * Time.deltaTime * moveSpeed);
        }
        else if(deathDash == true) // Death dash 
        {
            rb2D.velocity = moveDirection * dashPower;

            AudioMScript.PlayCamicazeDashing(audioSource); // Plays SFX of death dash
            deathDash = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collided");
            collision.gameObject.GetComponentInParent<PlayerController>().TakeDamage(EHD.damage);
            EHD.OnDeath(false);
        }
    }
}
