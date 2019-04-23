using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellController : MonoBehaviour
{
    //public GameObject starDirec;

    //private StarDirector starCreate;

    [SerializeField]
    private float shakeSize;

    private float shakeSpeed = 0.5f;

    private float shakeRote;

    [SerializeField]
    private float maxShakeRote;

    private bool shakeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //starCreate = starDirec.GetComponent<StarDirector>();
        shakeRote = this.transform.localRotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeFlag)
        {
            Debug.Log(shakeRote);
            
            if (this.transform.localRotation.z < -maxShakeRote)
            {
                shakeRote *= -1;
            }
            if (this.transform.localRotation.z > maxShakeRote)
            {
                shakeRote *= -1;
            }
            transform.Rotate(new Vector3(0.0f, 0.0f, shakeRote));
            shakeRote = Mathf.Sin(shakeSize);
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
