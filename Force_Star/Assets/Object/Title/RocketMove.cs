using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour
{
    //速さ
    [SerializeField]
    float speed = 0.2f;

    // 間隔
    [SerializeField]
    float interval = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        //ロケットの移動
        this.transform.position = new Vector3(this.transform.position.x, (Mathf.Sin(Time.frameCount * speed) * interval), this.transform.position.z);

    }
}
