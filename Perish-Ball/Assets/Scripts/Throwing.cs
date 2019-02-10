using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour {

    public GameObject ball;
    Rigidbody ballRB;
    public GameObject hoverPoint;
    public bool thrown = false;
    public float initialVelocity = 100.0f;
    float currentVelocity = 0;
    Vector3 throwForward = new Vector3();
    bool isMoving = false;

    Transform hoverTrans;

    // Use this for initialization
    void Start ()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");

        hoverTrans = gameObject.GetComponentInParent<Transform>();
        ballRB = ball.GetComponent<Rigidbody>();
        //this.GetComponent<Rigidbody>().freezeRotation = true;
        //get the hover point


        hoverPoint = GameObject.FindGameObjectWithTag("HoverPoint");
    }
	
	// Update is called once per frame
	void Update ()
    {

        //make sure the ball is staying at the hover point
        if (!thrown)
        {
            //ball.transform.position = hoverPoint.transform.position;
            ball.transform.position = hoverTrans.position + hoverTrans.forward;

            //ball.transform.forward = hoverTrans.forward + ball.transform.position;
        }
        else
        {
            if (isMoving == false)
            {
                throwForward = hoverTrans.forward;

                //throwForward = ball.transform.forward;
                ballRB.AddForce(throwForward * currentVelocity, ForceMode.Impulse);
                //currentVelocity -= .1f;
                isMoving = true;
            }
        }

        //calculate players forward vector

        //check for throw
        if (Input.GetKeyDown(KeyCode.Space) && thrown == false) 
        {
            //throwForward = ball.transform.forward;
            throwForward = hoverTrans.forward;

            //throwForward = new Vector3(throwForward.x, 0, throwForward.z);
            currentVelocity = initialVelocity;
            thrown = true;
        }

        //return it to the player when its done
        //if(ballRB.velocity.sqrMagnitude <= new Vector3(0.2f,0.0f,0.0f).sqrMagnitude)
        {
           // thrown = false;
        }
    }
}
