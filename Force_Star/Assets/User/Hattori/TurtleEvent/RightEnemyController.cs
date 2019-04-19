using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightEnemyController : MonoBehaviour
{
    private float moveSpeed = 0.2f;

    float moveTimer = 0.0f;

    bool moveFlag = false;

    Vector3 keepPos;

    float battleTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        keepPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        battleTimer++;

        if (battleTimer > 75)
        {
            moveFlag = true;
        }

        if (moveFlag == true)
        {
            moveTimer += 0.1f;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - (Mathf.Sin(moveTimer * moveSpeed)), this.transform.localPosition.y, this.transform.localPosition.z);
        }
        else
        {
            moveTimer = 0;
            this.transform.localPosition = new Vector3(keepPos.x, this.transform.localPosition.y, this.transform.localPosition.z);
        }

        Debug.Log(Mathf.Sin(this.transform.localPosition.x));

        if (((Mathf.Sin(moveTimer * moveSpeed)) > 0.4f))
        {
            moveFlag = false;
            battleTimer = 0.0f;
        }
    }
}