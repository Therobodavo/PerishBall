using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour {


   public GameObject path1;

    public Text title;
    public Text title2;
    public GameObject[] menuUi;


   public int stage; // current stage for the scene
	void Start () {
        stage = 0;

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(-1, 0, 1)); // rotating the ball
        if (stage == 0)
        {
          //the ball is sent to the center
            Vector3 distance = (path1.transform.position - transform.position).normalized;

            transform.position += distance * 1000 * Time.deltaTime;

            // if play hits trigger then menu starts
            if (Input.GetAxis("P1_Throw_L") > 0.0f || Input.GetAxis("P1_Throw_R") > 0.0f || Input.GetAxis("P2_Throw_L") > 0.0f || Input.GetAxis("P2_Throw_R") > 0.0f || Input.GetKeyUp(KeyCode.Space))
                stage = 2;
        }
        if (stage == 1)
        {
            // if play hits trigger then menu starts
            if (Input.GetAxis("P1_Throw_L") > 0.0f || Input.GetAxis("P1_Throw_R") > 0.0f || Input.GetAxis("P2_Throw_L") > 0.0f || Input.GetAxis("P2_Throw_R") > 0.0f || Input.GetKeyUp(KeyCode.Space))
                stage = 2;

        }
        if(stage == 2)
        {
            //menu is now enabled
            if (title.enabled)
            {
                title.enabled = false;
                title2.enabled = false;
            }
              
            foreach(GameObject obj in menuUi)
            {
                obj.SetActive(true);
            }
            stage = 3;
        }
    

    }

    //method for loading new scene
    public void loadMap(string map)
    {
        //in inspector, a string is made for map name
        SceneManager.LoadScene(map, LoadSceneMode.Single);
    }

    //collider for controling the ball
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == path1)
            stage = 1;

  

    }
}
