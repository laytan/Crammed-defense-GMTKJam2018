using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float shakeAmt;
    public float shakeTime;
    private bool shake = false;
    private Vector3 pos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(shake)
        {
            transform.position = new Vector3(transform.position.x + (Random.Range(-1,1) * shakeAmt), transform.position.y, transform.position.z);
        }
	}

    public void StartShake()
    {
        if(shake)
        {
            return;
        }
        pos = transform.position;
        shake = true;
        Invoke("StopShake", shakeTime);
    }
    public void StopShake()
    {
        transform.position = pos;
        shake = false;
    }
    
}
