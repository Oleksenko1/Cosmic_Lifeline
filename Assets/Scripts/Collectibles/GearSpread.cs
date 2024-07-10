using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSpread : MonoBehaviour
{
    public GameObject gearPrefab;
    public float minForce;
    public float maxForce;
    public float randomAngle;
    public float minTorque;
    public float maxTorque;

    public Sprite[] gearSprite;

    public void SpreadGears(int amount)
    {
        float rotation = 360 / amount;

        for (int x = 1; x < amount + 1; x++)
        {
            Vector3 gearRotation = new Vector3(0, 0, rotation * x + Random.Range(-randomAngle, randomAngle)); // Rotation of gear

            var gear = Instantiate(gearPrefab, transform.position, Quaternion.Euler(gearRotation));

            gear.GetComponent<SpriteRenderer>().sprite = gearSprite[Random.Range(0, gearSprite.Length)]; // Adding random texture to a gear

            float force = Random.Range(minForce, maxForce);
            gear.GetComponent<Rigidbody2D>().AddForce(gear.transform.up * force, ForceMode2D.Impulse); // Applying force to a gear

            float torque = Random.Range(minTorque, maxTorque);
            gear.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-torque, torque)); // Applying torque force to a gear
        }
        Destroy(gameObject);
    }
}
