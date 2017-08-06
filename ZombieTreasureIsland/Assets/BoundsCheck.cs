using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test");
        //GameObject.Find("player").GetComponent<playerScript>().speed = 0;
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");
        //GameObject.Find("player").GetComponent<playerScript>().speed = 5;
    }
}
