using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null; // 追従する目標

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, 0.0f, -10.0f);

        if (transform.position.x < 0.0f)
        {
            transform.position = new Vector3(0.0f, 0.0f, -10.0f);
        }

        if (transform.position.x >= 348.4f)
        {
            transform.position = new Vector3(348.4f, 0.0f, -10.0f);
        }
    }
}
