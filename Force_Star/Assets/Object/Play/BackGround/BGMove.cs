using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float power = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.frameCount * power) / 10.0f, transform.position.z);  
    }
}
