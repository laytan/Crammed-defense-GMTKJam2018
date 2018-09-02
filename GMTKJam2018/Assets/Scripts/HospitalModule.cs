using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalModule : Module {

    public int timePerHealth;
    private GameController gc;

    void Awake()
    {
        base.OnAwake();
    }
    void Update()
    {
        base.OnUpdate();
    }

    // Use this for initialization
    void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(heal());
	}

    IEnumerator heal()
    {
        if(gc.health >= gc.maxHealth)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(heal());
        }
        else
        {
            gc.Heal();
            GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(timePerHealth);
            StartCoroutine(heal());
        }
    }

}
