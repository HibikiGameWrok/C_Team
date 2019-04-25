using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellController : MonoBehaviour
{
    //public GameObject starDirec;

    //private StarDirector starCreate;

    [SerializeField]
    private float shakeSpeed = 0.5f;

    private float shakeRote;

    [SerializeField]
    private float maxShakeRote = 0.3f;

    private bool shakeFlag = false;

    private bool moveFlag = false;

    //振れ幅の初期値記録用の値
    private float defoltShakeSpeed;



    // Start is called before the first frame update
    void Start()
    {
        //starCreate = starDirec.GetComponent<StarDirector>();
        shakeRote = this.transform.localRotation.z;
        defoltShakeSpeed = shakeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeFlag)
        {
            shakeRote = shakeSpeed;

            if (this.transform.localRotation.z < -maxShakeRote&& !moveFlag)
            {
                moveFlag = true;
                shakeSpeed *= -1;
                shakeSpeed -= 0.1f;
                maxShakeRote -= 0.01f;
            }
            if (this.transform.localRotation.z > maxShakeRote&& !moveFlag)
            {
                moveFlag = true;
                shakeSpeed *= -1;
                shakeSpeed += 0.1f;
                maxShakeRote -= 0.01f;
            }
            if (this.transform.localRotation.z > -maxShakeRote&& this.transform.localRotation.z < maxShakeRote&& moveFlag)
            {
                moveFlag = false;
            }
            if (maxShakeRote < 0.01f)
            {
                shakeFlag = false;
                shakeSpeed = defoltShakeSpeed;
                maxShakeRote = 0.3f;
            }
            Debug.Log(maxShakeRote);
            transform.Rotate(new Vector3(0.0f, 0.0f, shakeRote));
        }
        else
        {
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
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
