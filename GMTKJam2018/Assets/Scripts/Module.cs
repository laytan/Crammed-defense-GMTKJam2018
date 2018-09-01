using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
    [HideInInspector]
    public EnemyController ec;

    //Used to call the OnAwake and OnUpdate functions, don't add to these
    void Awake()
    {
        OnAwake();
    }

    void Update()
    {
        OnUpdate();
    }

    //Also gets called from inheriting classes
    public void OnAwake()
    {
        //Get the enemy controller
        ec = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
    }
    //Also gets called from inheriting classes
    public void OnUpdate()
    {
        //Deactivate all our functionality when we are out of the tile
        if(transform.position.x > 0.5 && transform.position.x > -0.5)
        {
            Destroy(this);
        }
    }
}
