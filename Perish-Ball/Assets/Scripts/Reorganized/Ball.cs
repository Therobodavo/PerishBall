using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this script goes on the ball and will control its movement
 * will have a throw method that will be called by a player
 * will track who threw it, distance traveled, and number of bounces
 * should stop moving when it hits someone
 */
public class Ball : MonoBehaviour {

    //Variables
    public Vector3 throwVector;
    public int forcethrow = 10;

    Rigidbody ballRB;
	bool thrown = false;
    int playID = 0;
	private int totalDistance;
	private Vector3 startThrowPos;

    //References
    private GameObject thrower;
    public Material mat1; //In Flight material
    public Material mat2; //PickUp material
    public Material mat3; //Inflight material
                          // Use this for initialization
    void Start () {
		totalDistance = 0;
		startThrowPos = new Vector3 ();
		ballRB = transform.GetComponent<Rigidbody>();
		thrower = GameObject.Find ("Player_Prefab");
    }
	
	void Update () {

		if (gameObject.GetComponent<Rigidbody> ().velocity.sqrMagnitude <= new Vector3 (5.0f, 0.0f, 0.0f).sqrMagnitude) {
			gameObject.GetComponent<Renderer> ().material = mat2;
			thrown = false;
		} else if (thrown)
		{
            switch (playID)
            {
                case 1:
                    gameObject.GetComponent<Renderer>().material = mat1;

                    break;
                case 2:
                    gameObject.GetComponent<Renderer>().material = mat3;

                    break;
                default:
                    break;
            }
		}

    }
	public GameObject GetThrower()
	{
		return thrower;
	}
	public void SetThrower(GameObject t)
	{
		thrower = t;
	}
	public void SetStartPosition(Vector3 start)
	{
		startThrowPos = start;
	}
	public void ResetDistance()
	{
		totalDistance = 0;
	}
    public void Throw(int playerID)
    {
		startThrowPos = this.gameObject.transform.position;
		totalDistance = 0;
        ballRB.velocity = throwVector * forcethrow;
		thrown = true;
        playID = playerID;
    }
	public void SetThrown(bool isThrown)
	{
		thrown = isThrown;
	}
	public bool GetThrown()
	{
		return thrown;
	}

	//Main collision with ball
	private void OnCollisionEnter(Collision collision)
	{
		//If the ball hits a wall or another ball
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ball")
		{
			//keep track of how far the ball has traveled since thrown
			totalDistance += (int)(this.gameObject.transform.position - startThrowPos).magnitude;
			startThrowPos = this.gameObject.transform.position;
		}

		//If the ball hits a player and that player DIDN'T throw the ball
		else if (collision.gameObject.tag == "Player" && collision.gameObject != thrower && thrown && !(collision.gameObject.GetComponent<Player>().isDead))
		{
			//get total distance traveled
			totalDistance += (int)(this.gameObject.transform.position - startThrowPos).magnitude;

			//If the score gained is under 100, add 100
			if (totalDistance <= 2)
			{
				thrower.GetComponent<Player> ().AddScore (100);
			}

			//Otherwise... add distance times 50
			else
			{
				thrower.GetComponent<Player> ().AddScore (totalDistance * 50);
			}
			//Reset total distance
			totalDistance = 0;
			//Set start distance to here
			startThrowPos = this.gameObject.transform.position;
		}
		//If the ball hits a player and that player DID throw the ball
		else if (collision.gameObject.tag == "Player" && collision.gameObject == thrower && thrown && !(collision.gameObject.GetComponent<Player>().isDead))
		{
			//if the total score gained was over -100, subtract 100
			if (-totalDistance * 50 > -100)
			{
				thrower.GetComponent<Player> ().AddScore (-100);
			}

			//Otherwise... subtract distance times 50
			else
			{
				thrower.GetComponent<Player> ().AddScore (-totalDistance * 50);
			}
			//Reset total distance
			totalDistance = 0;
			//Set start distance to here
			startThrowPos = this.gameObject.transform.position;
		}

	}
}
