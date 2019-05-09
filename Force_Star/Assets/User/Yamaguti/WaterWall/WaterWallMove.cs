using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWallMove : MonoBehaviour
{
    public GameObject Top;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y+-0.1f, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ok");
        if (other.gameObject.tag == "WaterWall")
        {
            Top.GetComponent<WaterWall>().WaterList(gameObject);
            //Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Top.GetComponent<WaterWall>().WaterListAt(gameObject);
            //Destroy(this.gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ok");
        if (other.gameObject.tag == "WaterWall")
        {
            Top.GetComponent<WaterWall>().WaterList(gameObject);
           // Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Top.GetComponent<WaterWall>().WaterListAt(gameObject);
           // Destroy(this.gameObject);
        }

    }
}
