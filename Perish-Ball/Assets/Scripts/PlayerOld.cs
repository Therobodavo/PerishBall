using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOld : MonoBehaviour {

    //Variables 
    Rigidbody rb;
    CharacterController charCont;
    public int playNum;
    int rotNum = 0;
    bool hasControl = true;

    //KeyCode kc = KeyCode.A;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        charCont = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        /*
	    if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0, 0, 1));
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0, 0, -1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(1, 0, 0));
        }
        */
        
        if (playNum == 1)
        {
            if (hasControl)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    //charCont.Move(new Vector3(0, 0, 1));
                    charCont.SimpleMove(new Vector3(0, 0, 5));

                }

                if (Input.GetKey(KeyCode.S))
                {
                    charCont.SimpleMove(new Vector3(0, 0, -5));
                }
                if (Input.GetKey(KeyCode.A))
                {
                    charCont.SimpleMove(new Vector3(-5, 0, 0));
                }
                if (Input.GetKey(KeyCode.D))
                {
                    charCont.SimpleMove(new Vector3(5, 0, 0));
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    //Quaternion rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 1, transform.rotation.z);
                    rotNum--;
                }

                if (Input.GetKey(KeyCode.E))
                {
                    //Quaternion rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y + 1, transform.rotation.z);
                    rotNum++;
                }
            }
        }
        if (playNum == 2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //charCont.Move(new Vector3(0, 0, 1));
                charCont.SimpleMove(new Vector3(0, 0, 5));

            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                charCont.SimpleMove(new Vector3(0, 0, -5));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                charCont.SimpleMove(new Vector3(-5, 0, 0));
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                charCont.SimpleMove(new Vector3(5, 0, 0));
            }
        }

        transform.rotation = Quaternion.Euler(transform.rotation.x, rotNum, transform.rotation.z);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(collision.transform.forward * 100f);
            hasControl = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce(other.transform.forward * 100f);
            hasControl = false;
        }
    }
}
