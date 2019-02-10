using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuControl : MonoBehaviour {
    //script to control buttons
  public  GameObject lastSelected; //last selected button

	// Use this for initialization
	void Start () {
        //if the last selected button is null then it equals a new game object
        if(lastSelected == null)
        lastSelected = new GameObject();

	}
	
	// Update is called once per frame
	void Update () {
        //updatting event systems
		if(EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
        else
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
	}
}
