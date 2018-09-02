using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health;
    public float speed;
    private EnemyController ec;
    private GameController gc;
    public int moneyReward;
    public AudioClip die;
    // Use this for initialization
    void Start () {
        ec = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-20, transform.position.y), Time.deltaTime * speed);
        if(transform.position.x < -5)
        {
            ec.RemoveEnemyFromList(gameObject);
            gc.TakeDamage();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GetComponentInChildren<ParticleSystem>().Play();
        if (health < 0)
        {
            GetComponent<AudioSource>().PlayOneShot(die);
            gc.AddMoney(moneyReward);
            ec.RemoveEnemyFromList(gameObject);
            transform.GetChild(0).gameObject.SetActive(false);
            Invoke("Die", .3f);

        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
