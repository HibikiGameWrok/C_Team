using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCount : MonoBehaviour {

    GameObject starCounter;

    int starCount;

	// Use this for initialization
	void Start () {
        starCount = 0;

        this.starCounter = GameObject.Find("StarCounter");
	}
	
    public void AddCount(int count){
        starCount += count;
        Debug.Log("atatta");
    }

	// Update is called once per frame
	void Update () {
        this.starCounter.GetComponent<Text>().text = "×" + starCount.ToString();
	}
}
