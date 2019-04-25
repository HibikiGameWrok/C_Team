using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBottleController : MonoBehaviour
{
    public GameObject[] bottle;

    // Start is called before the first frame update
    void Start()
    {
        //int number = Random.Range(0, bottle.Length);
        //Instantiate(bottle[number], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }
}
