using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModule : Module {

    private GameObject gun;
    public GameObject bullet;
    private GameObject bulletSpawnPoint;
    public float rotateSpeed;
    private bool aim = true;
    public float timeBetweenShots;
    private float nextShotTime = 0f;

    //Call the awake function in Module class
    void Awake()
    {
        base.OnAwake();    
    }

    // Use this for initialization
    void Start () {
        gun = transform.GetChild(1).gameObject;
        bulletSpawnPoint = gun.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //Call the update function in module class
        base.OnUpdate();

        //Checks if we are allowed to shoot again, if so, shoot and bump up the time fir next shot
        if(Time.time > nextShotTime)
        {
            nextShotTime = Time.time + timeBetweenShots;
            Shoot();
        }

        //If we want to aim
        if (aim == true)
        {
            //Get the closest enemy from the enemy controller
            GameObject closestEnemy = ec.GetClosestEnemy();
            if(closestEnemy == null)
            { return; }
            //Get the direction that we need to rotate to
            Vector3 dir = closestEnemy.transform.position - gun.transform.position;
            //Math i don't understand
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //
            //Smoothly lerp to the new rotation
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, q, Time.deltaTime * rotateSpeed);
        }
	}
    void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, bulletSpawnPoint.transform.position, gun.transform.rotation);
    }
}
