using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    //Variables Used
    public float totalTime;
    public float currentTime;
    public float scoreTime;

	void Start ()
    {
        totalTime = 300.0f;
	}
	
	void Update ()
    {
        currentTime += Time.deltaTime;
        scoreTime = totalTime - currentTime;
	}
}
