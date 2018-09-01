using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //TODO: Implement
    public GameObject GetClosestEnemy()
    {
        return GameObject.Find("Enemy");
    }
}
