using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this class will go on the player prefab and will let them pick up balls that arent moving
 * should bind a ball that is within a radius, and not moving, to the player scripts ball variable
 * 
 */
public class PickupBall : MonoBehaviour {
    public GameObject player;
    CapsuleCollider pickupCollider;

	// Use this for initialization
	void Start () {
        player = transform.parent.gameObject;
        pickupCollider = player.transform.Find("PickupRadius").gameObject.GetComponent<CapsuleCollider>(); //honestly i have no idea if this even gets used
    }
	

    private void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.tag == "Ball"
			&& collision.gameObject.GetComponent<Ball>().GetThrower().GetComponent<Player>().myBall != collision.gameObject
			&& !collision.gameObject.GetComponent<Ball>().GetThrown())//lmao only pick it up if its slow maybe
        {
            GameObject b = collision.transform.gameObject;
            GiveBall(b);
        }
    }

    void GiveBall(GameObject b)
    {
        if (!player.GetComponent<Player>().hasBall) //so they cant pick up several balls
        {
            Debug.Log("PickupBall script: ball picked up");
            b.GetComponent<Ball>().SetThrower(this.transform.parent.gameObject);
			b.GetComponent<Ball> ().SetStartPosition (this.gameObject.transform.position);
			b.GetComponent<Ball> ().ResetDistance ();
            player.GetComponent<Player>().myBall = b;
            player.GetComponent<Player>().hasBall = true;
        }

    }
}
