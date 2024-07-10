using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gear : MonoBehaviour, ICollectible
{
    public float movespeed;

    public static event Action OnCoinCollected;
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        Debug.Log("You picked up a gear");
        OnCoinCollected?.Invoke();
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if(hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * movespeed;
        }
    }
    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
