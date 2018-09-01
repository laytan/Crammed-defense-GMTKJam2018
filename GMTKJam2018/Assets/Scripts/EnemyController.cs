using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Vector2 spawnPoint;
    public float timeBetweenSpawns;
    public int maxGroupSize;
    public float groupSpawnPercent;
    public GameObject[] spawnableEnemies;
    public bool paused = false;

    private float timeToSpawnEnemies = 0f;
    private List<GameObject> enemies = new List<GameObject>();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(paused)
        {
            return;
        }
		if(Time.time > timeToSpawnEnemies)
        {
            Spawn();
            timeToSpawnEnemies = Time.time + timeBetweenSpawns;
        }
	}

    void Spawn()
    {
        if (Random.Range(0, 100) < groupSpawnPercent)
        {
            Debug.Log("Spawning a group");
            int enemyAmount = Random.Range(0, maxGroupSize);
            GameObject[] enemiesToSpawn = new GameObject[enemyAmount];
            for(int i = 0; i < enemyAmount; i++)
            {
                enemiesToSpawn[i] = spawnableEnemies[Random.Range(0, spawnableEnemies.Length)];
            }
            StartCoroutine(SpawnObjects(enemiesToSpawn));
        }
        else
        {
            Debug.Log("Spawning a single enemy");
            StartCoroutine(SpawnObjects(new GameObject[] { spawnableEnemies[Random.Range(0, spawnableEnemies.Length)] }));
        }
    }
    IEnumerator SpawnObjects(GameObject[] toSpawn)
    {
        foreach(GameObject obj in toSpawn)
        {
            GameObject enemy = Instantiate(obj, spawnPoint, Quaternion.identity);
            enemies.Add(enemy);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public GameObject GetClosestEnemy()
    {
        GameObject closestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            if(closestEnemy == null)
            {
                closestEnemy = enemy;
            }
            else
            {
                if (enemy != null)
                {
                    if (enemy.transform.position.x < closestEnemy.transform.position.x)
                    {
                        //It's closer
                        closestEnemy = enemy;
                    }
                }
            }
        }
        return closestEnemy;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if(enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    } 
}
