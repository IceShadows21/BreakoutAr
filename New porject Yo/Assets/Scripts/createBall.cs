using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class createBall : MonoBehaviour {

    public GameObject ballInit;

    public float Speed = 0.1f;

    public int nbreBriqueLevel = 12;
    private bool waitForBall = false;

    public GameObject[] child;

    //UI
    public int ScoreValue = 0;

    private Text _scoreTxt;
    private GameObject _great;
    private Vector3 posGreatInit;

    private GameObject _end;
    private GameObject _restart;

    private float _time = 2.0f;
    private float _time2 = 2.0f;

    // Use this for initialization
    void Start () {
        Invoke("CreateNewBall", 2.0f);

        //UI
        _scoreTxt = GameObject.Find("score").GetComponent<Text>();

        _great = GameObject.Find("Great");
        _great.SetActive(false);

        posGreatInit = _great.transform.localScale;

        _end = GameObject.Find("End");
        _end.SetActive(false);

        _restart = GameObject.Find("Restart");
        _restart.SetActive(false);        
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(nbreBriqueLevel);

        if(nbreBriqueLevel == 0 && waitForBall == false)
        {
            for(int i = 0; i< child.Length; i++)
            {
                if (child[i].activeSelf)
                {
                    child[i + 1].SetActive(true);

                    waitForBall = true;
                    child[i].SetActive(false); 
                    
                    break;
                }
            }
        }

        _scoreTxt.text = "Score: " + ScoreValue;

        if (nbreBriqueLevel == 0 && child[child.Length - 1].activeSelf != true)
        {

            if(_time > 0f)
            {
                _great.SetActive(true);
                _great.transform.localScale += new Vector3(0.01f, 0.01f, 0);
                _time -= Time.deltaTime;
            }
            else
            {
                _great.SetActive(false);
                _great.transform.localScale = posGreatInit;
                waitForBall = false;
                nbreBriqueLevel = 12;
                _time = 2f;
            }
        }
        if(child[child.Length - 1].activeSelf == true)
        {
            Speed = 0f;

            if (_time2 > 0f)
            {
                _end.SetActive(true);
                _end.transform.localScale += new Vector3(0.005f, 0.005f, 0);
                _time2 -= Time.deltaTime;
            }
            else
            {
                _restart.SetActive(true);
            }
        }
    }

    public void CreateNewBall()
    {
        GameObject ball = Instantiate(ballInit);
        translateBall s = ball.GetComponent<translateBall>();
        s.Controller = this;
        s.Direction = Vector3.back;
    }

    public void CreateNewBall(float delay)
    {
        Invoke("CreateNewBall", delay);
    }

    //Relancer le jeu
    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
