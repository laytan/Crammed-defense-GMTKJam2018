using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Vector2 spawnPoint;
    public GameObject[] allEnemies;
    public bool paused = false;
    private float timeToSpawnEnemies = 10f;

    private List<GameObject> spawnableEnemies = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    //Difficulty
    public float timeBetweenSpawns;
    public int maxGroupSize;
    public float groupSpawnPercent;
    public float enemiesRange;

    [Header("Time between every bumb in difficulty")]
    public float difficultyChangeDelay;
    [Header("Seconds between spawning a new wave")]
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float decreaseTimeBetweenSpawns;
    [Header("Maximum enemies spawned in a wave")]
    public int minMaxGroupSize;
    public int maxMaxGroupSize;
    public int increaseMaxGroupSize;
    [Header("A percentage chance of a group spawning instead of a single enemy")]
    public float minGroupSpawnPercent;
    public float maxGroupSpawnPercent;
    public float increaseGroupSpawnPercent;
    [Header("The range of the enemies array the spawner can acces")]
    public int startEnemiesRange;
    public float increaseEnemiesRange;

    // Use this for initialization
    void Start () {
        timeBetweenSpawns = maxTimeBetweenSpawns;
        maxGroupSize = minMaxGroupSize;
        groupSpawnPercent = minGroupSpawnPercent;

        enemiesRange = startEnemiesRange;
        ChangeSpawnableEnemies(enemiesRange);

        StartCoroutine(CreepUpDifficulty());
	}
	
    IEnumerator CreepUpDifficulty()
    {
        yield return new WaitForSeconds(difficultyChangeDelay);

        timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns -= decreaseTimeBetweenSpawns, minTimeBetweenSpawns, maxTimeBetweenSpawns);
        maxGroupSize = Mathf.Clamp(maxGroupSize += increaseMaxGroupSize, minMaxGroupSize, maxMaxGroupSize);
        groupSpawnPercent = Mathf.Clamp(groupSpawnPercent += increaseGroupSpawnPercent, minGroupSpawnPercent, maxGroupSpawnPercent);
        enemiesRange = Mathf.Clamp(enemiesRange += increaseEnemiesRange, 0, allEnemies.Length);
        ChangeSpawnableEnemies(enemiesRange);

        StartCoroutine(CreepUpDifficulty());
    }

    void ChangeSpawnableEnemies(float range)
    {
        float r = Mathf.FloorToInt(range);
        spawnableEnemies.Clear();
        for(int i = 0; i < r; i++)
        {
            spawnableEnemies.Add(allEnemies[i]);
        }
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
                enemiesToSpawn[i] = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
            }
            StartCoroutine(SpawnObjects(enemiesToSpawn));
        }
        else
        {
            Debug.Log("Spawning a single enemy");
            StartCoroutine(SpawnObjects(new GameObject[] { spawnableEnemies[Random.Range(0, spawnableEnemies.Count)] }));
        }
    }
    IEnumerator SpawnObjects(GameObject[] toSpawn)
    {
        foreach(GameObject obj in toSpawn)
        {
            GameObject enemy = Instantiate(obj, spawnPoint, Quaternion.identity);
            enemies.Add(enemy);
            Debug.Log(enemy.name);
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
