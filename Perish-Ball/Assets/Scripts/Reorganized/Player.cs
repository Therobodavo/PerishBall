using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * this class handles player movement and general things about the player
 * should store what controls are for that player
 * have a refrence to a ball object
 * should call ball.throw() to throw ball
 */
public class Player : MonoBehaviour {

    //player info
    public int playerID;
    CharacterController charCont;
    GameObject playerObject;
    GameObject hoverPoint;
	private int score;
    float rotation; //rotation in degrees of the player
    float moveAmount = 10.0f; //controls how fast the player can move
    float rotAmount = 0.15f; // percent of difference between current and desired look to rotate by per frame
    public bool isDead = false;
    public float deadTimer; //Timer counting how long a player remians dead
    public float invTimer; //Timer for invisbility frames
    public Material mat1; //Defult material
    public Material mat2; //Invicbility material

    //controls
    public string moveVertical;
    public string moveHorizontal;
    public string rot_X;
    public string rot_Y;
    public string throwButtonL;
    public string throwButtonR;

    //ball info
    public GameObject myBall; //should be set to the empty not a ball object in the scene to make it not throw an error (which is done in Start())
    public GameObject noBall;
    public bool hasBall = false;

	// Use this for initialization
	void Start () {
		score = 0;
        deadTimer = 0;
        invTimer = 100;
        //setup stuff with the player
        playerObject = transform.gameObject;

        charCont = GetComponent<CharacterController>();
        hoverPoint = playerObject.transform.Find("HoverPoint").gameObject;

        //setup stuff with the ball
        noBall = playerObject.transform.Find("ThisIsNotABall").gameObject; //ok so this is kind hacky but its so that the player object is ok without a ball  object which will let us swap balls between players
        myBall = noBall;

        //Setting the interal rotation value to be the value in the editor
        rotation = transform.eulerAngles.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead)
        {
            deadTimer += 1 * Time.deltaTime;
            return;
        }
        else
            deadTimer = 0;

        invTimer  += 1 * Time.deltaTime; // incrmenting incivility timer

        if(invTimer < 60 * Time.deltaTime)
        {
            gameObject.GetComponent<Renderer>().material = mat2;
        }
        else
            gameObject.GetComponent<Renderer>().material = mat1;

        //if theres a ball hold it in place
        if (hasBall)
        {
            myBall.transform.position = hoverPoint.transform.position; //set position

            //myBall.transform.forward = hoverPoint.transform.forward; //set the forward vector
			myBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
            myBall.transform.rotation = transform.rotation;
			myBall.GetComponent<Ball> ().SetStartPosition (myBall.transform.position);
        }

        //movement
        Move(new Vector3(Input.GetAxis(moveHorizontal) * moveAmount, 0.0f, -Input.GetAxis(moveVertical) * moveAmount));
        

        //rotation
        float goalRot = (Mathf.Atan2(Input.GetAxis(rot_X), Input.GetAxis(rot_Y)) * Mathf.Rad2Deg) + 90.0f;
        if(Mathf.Abs(Input.GetAxis(rot_X))<=0.0001f|| Mathf.Abs(Input.GetAxis(rot_Y)) <= 0.0001f)
        {
            goalRot = rotation;
        }

        rotation = Mathf.LerpAngle(rotation, goalRot, rotAmount);
        Rotate(rotation);

        //throwing
        if (Input.GetAxis(throwButtonL) > 0.0f|| Input.GetAxis(throwButtonR) > 0.0f) 
        {
            ThrowBall();
        }
    }

    //moves the player 
    void Move(Vector3 direction)
    {
        charCont.SimpleMove(direction);
    }

    //rotates the player
    void Rotate(float amount)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, amount, transform.rotation.z);
    }

    void ThrowBall()
    {
        //only throw a ball if the player has one
        //the ball pickup script will reset these values when the player picks up a ball
        if (hasBall)
        {
            myBall.transform.rotation = transform.rotation;
            myBall.GetComponent<Ball>().throwVector = myBall.transform.forward; //give the ball the id of the player who threw it to track scoring later     

            //myBall.GetComponent<Ball>().throwVector = transform.forward; //give the ball the id of the player who threw it to track scoring later     

            myBall.GetComponent<Ball>().Throw(playerID); //give the ball the id of the player who threw it to track scoring later   

            //myBall.GetComponent<Ball>().throwVector = myBall.transform.forward; //give the ball the id of the player who threw it to track scoring later     


            Debug.Log("Throw vector: " + myBall.GetComponent<Ball>().throwVector + " Ball Forward vector: " + myBall.transform.forward);
            hasBall = false; //mark that the player no longer has a ball
            myBall = noBall; //set the players ball to the empty ball
        }
    }
	public int GetScore()
	{
		return score;
	}
	public void AddScore(int addition)
	{
		score += addition;
	}
    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "Ball" && invTimer > 60 * Time.deltaTime && collision.gameObject.GetComponent<Ball>().GetThrown())
        {
            myBall = noBall;
            Debug.Log("Player script: player was hit");
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            gameObject.gameObject.GetComponent<CharacterController>().enabled = false;
            rb.AddForce(collision.transform.forward  * 10,ForceMode.Impulse);
            isDead = true;
            hasBall = false;
        }
    }
}
