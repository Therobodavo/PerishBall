using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this class handles what happens when a player is hit
 * should respawn them after a delay at a respawn point
 */
public class SpawnManager : MonoBehaviour
{
    //Variables/References
    GameObject[] SpawnPoints; // all spawn objecs
    GameObject[] Players; // players
    public int respawnTime = 6; //how long it takes for player to respawn
    Transform transfrom;

    void Start()
    {
        SpawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        Players = GameObject.FindGameObjectsWithTag("Player");
        transfrom = Players[0].transform;

    }

    void Update()
    {
        //Checks if dead and spawn points
        foreach (GameObject player in Players)
        {
            if (player.GetComponent<Player>().deadTimer > (respawnTime * Time.deltaTime))
            {
                int rgen = Random.Range(0, SpawnPoints.Length);
                player.transform.rotation = transform.rotation;
                player.transform.position = SpawnPoints[rgen].transform.position;
                Destroy(player.GetComponent<Rigidbody>());
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<Player>().isDead = false;
                player.GetComponent<Player>().invTimer = 0; //reseting invTimer, so player is invicble
            }
        }
    }
}

