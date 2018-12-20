using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetActiveAllChildren(this.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
        int iterationCount = 0;
        while (true && iterationCount < 50)
        {
            iterationCount++;
        }
	}

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }
}
