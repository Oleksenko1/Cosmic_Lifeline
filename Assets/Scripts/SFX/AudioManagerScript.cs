using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    //[SerializeField] private AudioClip mainMusic;
    [SerializeField] private GameObject sfxPrefab;

    private AudioSource playerAS;

    [SerializeField] private AudioSource mainMusicAS;
    [SerializeField] private AudioClip mainMusicSFX;
    [SerializeField] private float mainMusicVolume = 1f;
    
    [SerializeField] private AudioClip playerShotSFX;
    [SerializeField] private float playerShotVolume = 1f;
    [SerializeField] private AudioClip playerDamagedSFX;
    [SerializeField] private float playerDamagedVolume = 1f;
    [SerializeField] private AudioClip playerShieldDamagedSFX;
    [SerializeField] private float playerShieldDamagedVolume = 1f;
    public AudioClip playerEngineSFX;
    public float playerEngineVolume = 1f;
    private bool repeat = true;

    [SerializeField] private AudioClip playerHitmarkSFX;
    [SerializeField] private float playerHitmarkVolume = 1f;

    [SerializeField] private AudioClip gearPickUpSFX;
    [SerializeField] private float gearPickUpVolume = 1f;

    [SerializeField] private AudioClip enemyShotSFX;
    [SerializeField] private float enemyShotVolume = 1f;
    [SerializeField] private AudioClip[] enemyExplodeSFX;

    [SerializeField] private AudioClip camicazeDashingSFX;
    [SerializeField] private float camicazeDashingVolume = 1f;
    [SerializeField] private AudioClip camicazeRechargedSFX;
    [SerializeField] private float camicazeRechargedVolume = 1f;

    public AudioClip beaconChargingSFX;
    public float beaconChargingVolume = 1f;
    [SerializeField] private AudioClip beaconRechargedSFX;
    [SerializeField] private float beaconRechargedVolume = 1f;

    private void Awake()
    {
        playerAS = GameObject.Find("Player").GetComponent<AudioSource>();
        
        PlayerController.OnPlayerFire += PlayPlayerShot;
        PlayerController.OnPlayerDamaged += PlayPlayerDamaged;
        PlayerController.OnPlayerShieldDamaged += PlayPlayerShieldDamaged;
        PlayerCollectorScript.OnGearCollected += PlayGearPickUp;
        PlayerBulletScript.OnEnemyHitted += PlayePlayerHitmark;
    }
    private void Start()
    {
        mainMusicAS.clip = mainMusicSFX;
        mainMusicAS.volume = mainMusicVolume;
    }
    public void PlayEnemyShot(AudioSource audioSource)
    {
        audioSource.PlayOneShot(enemyShotSFX, enemyShotVolume);
    }
    public void PlayEnemyExplode(Vector3 pos) //  Spawns gameObject, that plays explode sfx
    {
        AudioClip tempSFX = enemyExplodeSFX[Random.Range(0, enemyExplodeSFX.Length)];

        var tempPrefab = Instantiate(sfxPrefab, pos, sfxPrefab.transform.rotation);
        tempPrefab.GetComponent<AudioSource>().PlayOneShot(tempSFX);

        Destroy(tempPrefab, tempSFX.length);
    }
    public void PlayePlayerHitmark()
    {
        playerAS.PlayOneShot(playerHitmarkSFX, playerHitmarkVolume);
    }
    public void PlayPlayerShot()
    {
        playerAS.PlayOneShot(playerShotSFX, playerShotVolume);
    }
    public void PlayPlayerDamaged()
    {
        playerAS.PlayOneShot(playerDamagedSFX, playerDamagedVolume);
    }
    public void PlayPlayerShieldDamaged()
    {
        playerAS.PlayOneShot(playerShieldDamagedSFX, playerShieldDamagedVolume);
    }
    public void PlayPlayerEngineSFX(float verInput, AudioSource audioSource)
    {
        audioSource.volume = Mathf.Sqrt(Mathf.Abs(verInput));
    }
    public void PlayGearPickUp()
    {
        playerAS.PlayOneShot(gearPickUpSFX, gearPickUpVolume);
    }
    public void PlayCamicazeDashing(AudioSource audioSource)
    {
        audioSource.PlayOneShot(camicazeDashingSFX, camicazeDashingVolume);
    }
    public void PlayCamicazeRecharged(AudioSource audioSource)
    {
        audioSource.PlayOneShot(camicazeRechargedSFX, camicazeRechargedVolume);
    }
    public void PlayBeaconChargeSFX(AudioSource audioSource, bool b) // Start or stop playing recharge SFX of beacon
    {
        if (b) { audioSource.Play(); }
        else { audioSource.Stop(); }
    }
    public void PlayOnBeaconFullRecharge(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(beaconRechargedSFX, beaconRechargedVolume);
    }
    
}
