using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this class will bind the controls for the player objects in the game
 * each player needs 4 directions, 2 rotation directions, and a throw button
 * can hardcode number of players now but should plan ahead for being able to add more
 */
public class InputManager : MonoBehaviour
{
    //List<GameObject> players;
    GameObject[] players;
	// Use this for initialization
	void Start ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in players)
        {
            switch (go.GetComponent<Player>().playerID)
            {
                case 1:
                    go.GetComponent<Player>().moveVertical = "P1_Movement_Vertical";
                    go.GetComponent<Player>().moveHorizontal = "P1_Movement_Horizontal";
                    go.GetComponent<Player>().rot_X = "P1_Rot_X";
                    go.GetComponent<Player>().rot_Y = "P1_Rot_Y";
                    go.GetComponent<Player>().throwButtonL = "P1_Throw_L";
                    go.GetComponent<Player>().throwButtonR = "P1_Throw_R";

                    break;
                case 2:
                    go.GetComponent<Player>().moveVertical = "P2_Movement_Vertical";
                    go.GetComponent<Player>().moveHorizontal = "P2_Movement_Horizontal";
                    go.GetComponent<Player>().rot_X = "P2_Rot_X";
                    go.GetComponent<Player>().rot_Y = "P2_Rot_Y";
                    go.GetComponent<Player>().throwButtonL = "P2_Throw_L";
                    go.GetComponent<Player>().throwButtonR = "P2_Throw_R";
                    break;
                default:
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
