using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour {

    public GameObject draggableModule;

    private GameObject keepAtMousePosition;
    private Vector2 mousePos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Handy because we use this alot
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Temporary input
        //TODO: Change to ui elements
		if(Input.GetKeyDown(KeyCode.M))
        {
            DragThis(draggableModule);
            //Debug.Log("Selected module");
        }
        if(Input.GetMouseButtonDown(0))
        {
            //If the draggable script says we can't drop, return
            Draggable draggable = keepAtMousePosition.GetComponent<Draggable>();
            if (draggable != null && draggable.canDrop == false)
            {
                return;
            }
            //If there is nothing we selected
            if(keepAtMousePosition == null)
            {
                //Debug.Log("Nothing to drop");
                return;
            }
            //Make the selected object spawn the real module
            draggable.SpawnModule(mousePos, keepAtMousePosition);
        }

        //Keep the dragging object at our mouse position
        if(keepAtMousePosition != null)
        {
            keepAtMousePosition.transform.position = mousePos;
        }      
	}

    void DragThis(GameObject prefab)
    {
        //If we are dragging something destroy it so we can replace it
        if (keepAtMousePosition != null)
        {
            Debug.Log("Changed selected module");
            Destroy(keepAtMousePosition);
            keepAtMousePosition = null;
        }
        //Instantiate the dragging prefab, and set it as keepatmouseposition so it stays at the mouse
        GameObject dragging = Instantiate(prefab, mousePos, Quaternion.identity);
        keepAtMousePosition = dragging;
    }
}
