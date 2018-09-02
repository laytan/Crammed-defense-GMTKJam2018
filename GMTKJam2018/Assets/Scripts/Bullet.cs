using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int damage;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 4);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, transform.right * 100, Time.deltaTime * speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Enemy en = collision.gameObject.GetComponent<Enemy>();
        if(en != null)
        {
            en.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
