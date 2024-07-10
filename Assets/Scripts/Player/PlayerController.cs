using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerFire;
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerShieldDamaged;

    public float accelerationAmount = 1f;
    public float rotationSpeed = 1f;
    public float turretRotationSpeed = 3f;

    public float maxHealth = 40f;
    private float healthPoints;
    public float HealthPoints
    {
        get { return healthPoints; }
        set { healthPoints = value; 
              healthBar.GetComponent<Healthbar>().SetCurrentHealth(healthPoints); } // Sets UI healthbar text;
    }
    public float shieldCapacity;
    public float shieldRegen;
    public float shieldRechargeTime;
    public float shieldActivatingTime;
    public float damage;
    public float critChance;
    public float critMult;
    public float shotSpeed;
    public float reloadTime;

    public Text healthBar;
    public Camera sceneCamera;
    public GameObject bulletPrefab;
    public Transform[] turretTransform;
    public GameObject spawnManager;
    public AudioSource engineAudioSource;

    private bool isAlive = true;
    private float nextShot = 0f;
    private int nextTurret = 0;

    private AudioManagerScript audioMScript;
    private PlayerShieldScript shieldScript;
    private Vector2 mousePosition;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioMScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        shieldScript = GetComponent<PlayerShieldScript>();

        engineAudioSource.clip = audioMScript.playerEngineSFX;

        shieldScript.SetShieldParametrs(shieldCapacity, shieldRegen, shieldRechargeTime, shieldActivatingTime);
    }
    private void Start()
    {
        HealthPoints = maxHealth;

        engineAudioSource.Play();
    }
    void Update()
    {
        Move(); // Moves the player

        ShootProcess(); // Process of shooting

        spawnManager.transform.position = transform.position; // Draging Spawn manager to player
    }
    public void TakeDamage(float dmg) // On damage taken
    {
        if (shieldScript.GetCurrentPoints() > 0) // If shield is activated - damage would transport to it
        {
            HealthPoints -= shieldScript.ShieldOverDamage(dmg); // Player gets damaged if there it's not enough points
            shieldScript.DamageShield(dmg);

            OnPlayerShieldDamaged?.Invoke(); // Calling an event
        }
        else // If shield isn't activated - player will get damaged
        {
            shieldScript.RestartActivating();

            HealthPoints -= dmg;

            OnPlayerDamaged?.Invoke(); // Calling an event

            if (HealthPoints <= 0) // Death after reaching 0 points of health
            {
                HealthPoints = 0;
                isAlive = false;
            }
        }
    }
    private void Move() // Method that reprsent plyaer's movement
    {
        float verInput = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().AddForce(transform.up * accelerationAmount * verInput * Time.deltaTime); // Moving forwards and backwards

        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0 && Input.GetKey(KeyCode.LeftShift)) // Rotation and moving left/right
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * accelerationAmount * horInput * 0.6f * Time.deltaTime);
        }
        else if (horInput != 0)
        {
            GetComponent<Rigidbody2D>().AddTorque(-rotationSpeed * horInput * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.C)) // Stoping
        {
            GetComponent<Rigidbody2D>().angularVelocity = Mathf.Lerp(GetComponent<Rigidbody2D>().angularVelocity, 0, rotationSpeed * 0.06f * Time.deltaTime);
            GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, accelerationAmount * 0.06f * Time.deltaTime);
        }

        audioMScript.PlayPlayerEngineSFX(verInput, engineAudioSource); // Play engine SFX sound
    }

    private void ShootProcess()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < turretTransform.Length; i++) // Rotates turrets to mouse position
        {
            Vector2 aimDirectionOne = mousePosition - new Vector2(turretTransform[i].position.x, turretTransform[i].position.y);

            float aimAngleOne = Mathf.Atan2(aimDirectionOne.y, aimDirectionOne.x) * Mathf.Rad2Deg - 90f;

            turretTransform[i].rotation = Quaternion.Euler(0, 0, aimAngleOne);
        }

        if (Input.GetMouseButton(0) && Time.time > nextShot) // Fires on left mouse button click AND if turrets are reloaded
        {
            Fire();
            nextShot = Time.time + reloadTime;
        }
    }
    private void Fire() // Fire bullets from turrets
    {
        OnPlayerFire?.Invoke(); // Calling an event

        GameObject bullet = Instantiate(bulletPrefab, turretTransform[nextTurret].position, turretTransform[nextTurret].rotation);

        // Calculating of crit or not crit Damage
        float rand = UnityEngine.Random.Range(0, 100);
        bool isCrit = rand <= critChance;
        float bulletDamage = isCrit ? Mathf.Round(damage * critMult * 100.0f) * 0.01f : Mathf.Round(damage * 100.0f) * 0.01f;

        bullet.GetComponent<PlayerBulletScript>().firingShip = gameObject;
        bullet.GetComponent<PlayerBulletScript>().damage = bulletDamage;
        bullet.GetComponent<PlayerBulletScript>().crit = isCrit;

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * shotSpeed, ForceMode2D.Impulse);

        nextTurret++;
        nextTurret %= turretTransform.Length;
    }
}
