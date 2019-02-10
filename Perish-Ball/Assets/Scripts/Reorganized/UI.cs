using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public float totalTime;
    public float currentTime;
    public float scoreTime;
	// Use this for initialization
	void Start ()
    {
        totalTime = 300.0f;

	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime += Time.deltaTime;
        scoreTime = totalTime - currentTime;
	}

    private void OnGUI()
    {
        //GUI.Box(new Rect(0, 0, 125, 30), "Time left : " + scoreTime);
    }
}
