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
    private List<GameObject> targetedBySniper = new List<GameObject>();

    //Difficulty
    public float timeBetweenSpawns;
    public float maxGroupSize;
    public float groupSpawnPercent;
    public float enemiesRange;

    public int moneyOverTimeAmt;
    [Header("Time between every bumb in difficulty")]
    public float difficultyChangeDelay;
    [Header("Seconds between spawning a new wave")]
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float decreaseTimeBetweenSpawns;
    [Header("Maximum enemies spawned in a wave")]
    public float minMaxGroupSize;
    public float maxMaxGroupSize;
    public float increaseMaxGroupSize;
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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddMoney(moneyOverTimeAmt);

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
            int enemyAmount = Random.Range(0, Mathf.FloorToInt(maxGroupSize));
            GameObject[] enemiesToSpawn = new GameObject[enemyAmount];
            for(int i = 0; i < enemyAmount; i++)
            {
                enemiesToSpawn[i] = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
            }
            StartCoroutine(SpawnObjects(enemiesToSpawn));
        }
        else
        {

            StartCoroutine(SpawnObjects(new GameObject[] { spawnableEnemies[Random.Range(0, spawnableEnemies.Count)] }));
        }
    }
    IEnumerator SpawnObjects(GameObject[] toSpawn)
    {
        foreach(GameObject obj in toSpawn)
        {
            GameObject enemy = Instantiate(obj, spawnPoint, Quaternion.identity);
            enemies.Add(enemy);
            yield return new WaitForSeconds(Random.Range(0.1f,1f));
        }
    }

    public GameObject GetClosestEnemy(GameObject asker)
    {
        GameObject returnEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            //If a sniper has targeted it already
            if(targetedBySniper.Contains(enemy))
            { continue; }
            //If it's the first iteration
            if(returnEnemy == null)
            {
                returnEnemy = enemy;
            }
            //If it's closer then the current returnEnemy
            else if (enemy.transform.position.x < returnEnemy.transform.position.x)
            {
                returnEnemy = enemy;
            }
        }
        if(asker.tag == "Sniper")
        {
            targetedBySniper.Add(returnEnemy);
        }
        return returnEnemy;
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if(enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
        if(targetedBySniper.Contains(enemy))
        {
            targetedBySniper.Remove(enemy);
        }
    } 
}
