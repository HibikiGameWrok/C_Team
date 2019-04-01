using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{

    [SerializeField]
    float speed = -0.1f;
    [SerializeField]
    Vector2 deletePos = new Vector2(-20.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0);

        if(transform.position.x < deletePos.x)
        {
            //Destroy(gameObject);
            gameObject.transform.position = new Vector2(18.0f, 0.0f);
         }


    }
}
