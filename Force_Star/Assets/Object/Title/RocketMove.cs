using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    //速さ
    [SerializeField]
    float speed= 0.2f;

    [SerializeField]
    float s = 1.0f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        //ロケットの移動
        transform.position = new Vector3(s, Mathf.Sin(Time.frameCount * speed), transform.position.z);

    }
}
