using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCount : MonoBehaviour {

    GameObject starCounter;

    public ParticleSystem particle;

    public GameObject panele;

    int starCount;

    int clearStarCount;

    public bool escapeFlag = false;

    bool efectFlag = false;

    Color panelColor;

    // Use this for initialization
    void Start () {
        starCount = 0;

        this.starCounter = GameObject.Find("StarCounter");

        clearStarCount = 100;
        particle.Stop();
        panelColor = panele.GetComponent<SpriteRenderer>().color;
        panele.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.0f);
       
    }
	
    public void AddCount(int count){
        starCount += count;
        //Debug.Log("atatta");
    }

	// Update is called once per frame
	void Update () {
        this.starCounter.GetComponent<Text>().text = "×" + starCount.ToString();

        if(starCount >= clearStarCount)
        {
            escapeFlag = true;
        }

        if (!efectFlag&& escapeFlag)
        {
            Debug.Log("ok");
            panele.GetComponent<SpriteRenderer>().color = panelColor;
            particle.Play();
            efectFlag = true;
        }
          
	}
}
