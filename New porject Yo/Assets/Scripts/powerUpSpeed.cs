using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpeed : MonoBehaviour {

    public Vector3 Direction;
    public float Speed = 0.1f;

    public string objectExist;

    public createBall ballScript;

    // Use this for initialization
    void Start () {
        Direction = Vector3.back;
    }
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find(objectExist) == null)
        {
            transform.Translate(Direction * Speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("DeadWall")){
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            ballScript.Speed = 0.2f;
            Destroy(gameObject);
        }
    }
}
