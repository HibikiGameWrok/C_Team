using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCange : MonoBehaviour
{
    [SerializeField]
    float s = 1.0f;

    [SerializeField]
    float pos_X = -6.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos_X += 6.0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos_X += -6.0f;
        }

        if(pos_X < -6.0f)
        {
            pos_X = 6.0f;
        }

        if (pos_X > 6.0f)
        {
            pos_X = -6.0f;
        }


        //大きさの変更
        this.transform.localScale = new Vector3(1.5f + (Mathf.Sin(Time.frameCount * s)/4), 1.5f + (Mathf.Sin(Time.frameCount * s) / 4), 1);
        //位置の移動
        transform.position = new Vector3(pos_X, transform.position.y, transform.position.z);
    }
}
