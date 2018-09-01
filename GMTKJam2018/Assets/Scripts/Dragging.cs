using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour {

    private GameObject keepAtMousePosition;
    private Vector2 mousePos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Handy because we use this alot
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       


        if (Input.GetMouseButtonDown(0))
        {
            //If we can drop the module
            if (CanWeDrop())
            {
                //Make the selected object spawn the real module
                keepAtMousePosition.GetComponent<Draggable>().SpawnModule(mousePos, keepAtMousePosition);
            }
        }

        //Keep the dragging object at our mouse position
        if(keepAtMousePosition != null)
        {
            keepAtMousePosition.transform.position = mousePos;
        }      
	}

    public void DragThis(GameObject prefab)
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

    bool CanWeDrop()
    {
        if(keepAtMousePosition == null)
        {
            return false;
        }
        Draggable draggable = keepAtMousePosition.GetComponent<Draggable>();
        if (draggable == null)
        {
            return false;
        }
        if (keepAtMousePosition == null || draggable.canDrop == false)
        {
            return false;
        }
        return true;
    }
}
