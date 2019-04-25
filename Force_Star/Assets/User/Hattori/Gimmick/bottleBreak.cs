using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottleBreak : MonoBehaviour
{
    public GameObject[] bottle;

    private int number;

    // Start is called before the first frame update
    void Start()
    {
        number = Random.Range(0, bottle.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {

            Instantiate(bottle[number], transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
