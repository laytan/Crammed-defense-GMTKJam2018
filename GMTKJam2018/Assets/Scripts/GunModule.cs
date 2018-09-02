using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModule : Module {

    private GameObject gun;
    public GameObject bullet;
    private GameObject bulletSpawnPoint;
    public float heightDamageMultiplier;
    public float rotateSpeed;
    private bool aim = true;
    public float timeBetweenShots;
    private float nextShotTime = 0f;
    public float bulletSpeed;
    public int bulletDamage;
    public float range;
    private Animator anim;
    private int startBulletDamage;
    //Call the awake function in Module class
    void Awake()
    {
        base.OnAwake();    
    }

    // Use this for initialization
    void Start () {
        startBulletDamage = bulletDamage;
        anim = GetComponentInChildren<Animator>();
        if(anim == null)
        {

        }
        gun = transform.GetChild(1).gameObject;
        bulletSpawnPoint = gun.transform.GetChild(0).gameObject;
	}
	bool AreAimingAtEnemy()
    {
        Vector2 dir = gun.transform.right;
        bool toReturn = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(gun.transform.position, dir, range);
        if(hit.Length > 0)
        {
            foreach(RaycastHit2D h in hit)
            {
                if(h.collider.gameObject.tag == "Enemy")
                {
                    toReturn = true;
                }
            }
        }
        return toReturn;
    }
	// Update is called once per frame
	void Update () {
        //Call the update function in module class
        base.OnUpdate();
        bulletDamage = Mathf.RoundToInt(startBulletDamage + (transform.position.y * heightDamageMultiplier));
        //Checks if we are allowed to shoot again, if so, shoot and bump up the time fir next shot
        if(Time.time > nextShotTime)
        {
                if(AreAimingAtEnemy())
                {
                    nextShotTime = Time.time + timeBetweenShots;
                    Shoot();
                }
            
            
        }

        //If we want to aim
        if (aim == true)
        {
            //Get the closest enemy from the enemy controller
            GameObject closestEnemy = ec.GetClosestEnemy(gameObject);
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

        if (anim != null)
        {
            anim.SetBool("Shoot", true);
        }
        Invoke("StopAnim", 1);
        GetComponentInChildren<ParticleSystem>().Play();
        GameObject bulletObj = Instantiate(bullet, bulletSpawnPoint.transform.position, gun.transform.rotation);
        bulletObj.GetComponent<Bullet>().speed = bulletSpeed;
        bulletObj.GetComponent<Bullet>().damage = bulletDamage;
    }
    void StopAnim()
    {
        if (anim != null)
        {
            anim.SetBool("Shoot", false);
        }
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
