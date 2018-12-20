using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class synchronisePosition : MonoBehaviour {

    public GameObject Reference;
    //private float posY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //transform.position.Set(transform.position.x, Reference.transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, Reference.transform.position.y, transform.position.z);
	}
}
