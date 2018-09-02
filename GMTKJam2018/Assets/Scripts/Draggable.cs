using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {

    public GameObject module;
    public bool canDrop = true;
    public int cost;
    private GameController gc;
    public AudioClip drop;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>(); 
    }

    public void SpawnModule(Vector3 pos, GameObject uiGO)
    {
        if (gc.TakeMoney(cost))
        {
            GameObject.FindGameObjectWithTag("UIAudio").GetComponent<AudioSource>().PlayOneShot(drop);
            //Destroy the ui object and instantiate a real one
            Instantiate(module, pos, Quaternion.identity);
            Destroy(uiGO);
        }
        else
        {
            Debug.Log("Out of money");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Module" || collision.gameObject.tag == "Ground")
        {
            canDrop = false;
            //TODO: Change sprite to show that you cant drop it here
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Module" || collision.gameObject.tag == "Ground")
        {
            canDrop = true;
            //Change back the sprite
        }
    }
}
