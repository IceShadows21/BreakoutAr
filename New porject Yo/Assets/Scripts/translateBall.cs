using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translateBall : MonoBehaviour {
    public Vector3 Direction;
    public createBall Controller;

    private const float EPSILON = 0.001f;
    private float _margin = 0.0000000000000000001f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.Translate(0.1f * Time.deltaTime, 0f, 0f);
        transform.Translate(Direction * Controller.Speed * Time.deltaTime);

        if(transform.position.magnitude >= 0.5)
        {
            Destroy(gameObject);
            Controller.CreateNewBall(2f);
        }

        //Reinitialisation entre chaque niveau
        if (Controller.nbreBriqueLevel == 0)
        {
            Controller.CreateNewBall(3f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Vector3 collisionPosition = collider.contacts[0].point;
            Vector3 collisionPosition = collider.ClosestPointOnBounds(transform.position);
            //transform.position = collisionPosition;

            Vector3 localPosition = collider.transform.InverseTransformPoint(collisionPosition);

            float angle = Mathf.Lerp(135f,45f,localPosition.x + 0.5f);

            

            if (localPosition.z > 0)
            { 
                float rotPad = angle - collider.transform.rotation.eulerAngles.y;

                rotPad *= Mathf.Deg2Rad;

                Direction = new Vector3(Mathf.Cos(rotPad), 0, Mathf.Sin(rotPad));
            }
            else
            {
                float rotPad = angle + collider.transform.rotation.eulerAngles.y;

                rotPad *= Mathf.Deg2Rad;

                Direction = new Vector3(Mathf.Cos(rotPad), 0, -Mathf.Sin(rotPad));
            }
        }
        if (collider.gameObject.CompareTag("Wall"))
        {
            if (Mathf.Abs(collider.transform.rotation.eulerAngles.y-90) < EPSILON 
                || Mathf.Abs(collider.transform.rotation.eulerAngles.y - 270) < EPSILON)
            {
                Direction = new Vector3(Direction.x, 0, -Direction.z);
            }
            else
            {
                Direction = new Vector3(-Direction.x, 0, Direction.z);
            }
        }
        if (collider.gameObject.CompareTag("DeadWall"))
        {
            Destroy(gameObject);
            //Controller.CreateNewBall();
            //Controller.SendMessage("Start");
            Controller.CreateNewBall(2f);
        }
        if (collider.gameObject.CompareTag("Brique"))
        {
            Vector3 collisionPosition = collider.ClosestPointOnBounds(transform.position);

            Vector3 localPosition = collider.transform.InverseTransformPoint(collisionPosition);

            if (localPosition.x > -0.5 + _margin && localPosition.x < 0.5 - _margin)
            {
                Direction = new Vector3(Direction.x, 0, -Direction.z);
            }
            else
            {
                Direction = new Vector3(-Direction.x, 0, Direction.z);
            }
            Controller.nbreBriqueLevel -= 1;
            Controller.ScoreValue += 1;

            Destroy(collider.gameObject);
        }
    }
}
