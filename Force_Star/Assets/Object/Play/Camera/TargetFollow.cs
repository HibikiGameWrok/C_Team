using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null; // 追従する目標

    [SerializeField]
    private float MinimumLimit = 0.0f;  // 最低の範囲

    [SerializeField]
    private float HighLimit = 0.0f;  // 最高の範囲


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, 0.0f, -10.0f);

        if (transform.position.x < MinimumLimit)
        {
            transform.position = new Vector3(MinimumLimit, 0.0f, -10.0f);
        }

        if (transform.position.x >= HighLimit)
        {
            transform.position = new Vector3(HighLimit, 0.0f, -10.0f);
        }
    }
}
