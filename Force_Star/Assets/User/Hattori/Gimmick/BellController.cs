using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellController : MonoBehaviour
{
    //public GameObject starDirec;

    //private StarDirector starCreate;

    [SerializeField]
    private float shakeSize;

    private float shakeRote = 1.0f;

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
            Debug.Log(shakeRote);
            if(0.0f < shakeSize && this.transform.localRotation.z < -shakeSize)
            {
                shakeRote += 0.1f;
            }
            if (this.transform.localRotation.z > shakeSize && 0.0f > shakeSize)
            {
                shakeRote -= 0.1f;
            }
         transform.Rotate(new Vector3(0.0f, 0.0f, shakeRote));   
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
