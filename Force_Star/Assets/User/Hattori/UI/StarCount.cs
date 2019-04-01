using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCount : MonoBehaviour {

    GameObject starCounter;

    int starCount;

    public int maxStarCount;

	// Use this for initialization
	void Start () {
        starCount = 0;

        this.starCounter = GameObject.Find("StarCounter");
	}
	
	// Update is called once per frame
	void Update () {
        
        if(starCount > maxStarCount)
        {
            starCount = maxStarCount;
        }

        this.starCounter.GetComponent<Text>().text = "×" + starCount.ToString();
	}
}
