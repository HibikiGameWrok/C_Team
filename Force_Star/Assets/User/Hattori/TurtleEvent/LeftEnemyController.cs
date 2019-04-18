using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftEnemyController : MonoBehaviour
{
    private float moveSpeed = 0.5f;

    float moveTimer = 0.0f;

    bool moveFlag = true;

    Vector3 keepPos;

    // Start is called before the first frame update
    void Start()
    {
        keepPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveFlag == true)
        {
            moveTimer++;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - (Mathf.Sin(moveTimer * moveSpeed) * 3.0f), this.transform.localPosition.y, this.transform.localPosition.z);
        }
        else
        {
            moveTimer = 0;
            this.transform.localPosition = new Vector3(keepPos.x, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        if (((Mathf.Sin(moveTimer * moveSpeed) * 3.0f) < -2.8f))
        {
            moveFlag = false;
        }
    }
}
