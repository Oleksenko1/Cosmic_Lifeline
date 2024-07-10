using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float[] chance; // Needs to be sorted from highest to lowest
    public float delay;
    private List<GameObject> spawnPoints = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), delay, delay);

        for(int i = 0; i < transform.childCount; i++) // Writes all spawn points in needed List
        {
            spawnPoints.Add(transform.GetChild(i).gameObject);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab[CalculateChance()], spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.Euler(Vector3.zero));
    }
    int CalculateChance() // Returning random integer
    {
        int randInt = Random.Range(0, 100);
        float sumChance = 0;

        //Debug.Log("Random number is: " + randInt);

        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            sumChance += chance[i];
            if(randInt < sumChance)
            {
                //Debug.Log("Returned num is: " + i);
                return i;
            }
        }
        //Debug.Log("Error in calculating random chance");
        return 0;
    }
}
