using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thisdel : MonoBehaviour
{
    private GameObject Roket;
    private RoketClearMove clearmove;

    // Start is called before the first frame update
    void Start()
    {
        Roket = GameObject.Find("Rocket_1");
        clearmove = Roket.GetComponent<RoketClearMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clearmove.GetdelUIFlag() == true)
        {
            Destroy(this.gameObject);
        }
    }
}
