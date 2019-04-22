using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellController : MonoBehaviour
{
    //public GameObject starDirec;

    //private StarDirector starCreate;

    [SerializeField]
    private float shakeSize = 0.0f;

    private float shakeRote = 0.0f;

    private bool shakeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //starCreate = starDirec.GetComponent<StarDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeFlag)
        {
            if(0.0f < shakeRote && shakeRote < shakeSize)
            {
                shakeRote++;
            }
         transform.Rotate(0.0f, 0.0f, shakeRote);   
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "AttackBoal")
        {
            shakeFlag = true;
        }
    }
}
