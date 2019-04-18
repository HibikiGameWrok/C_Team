using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillCollisionScript : MonoBehaviour
{
    bool hitFlag;
    bool windMillFlag;
    public GameObject punchObject;

    // Start is called before the first frame update
    void Start()
    {
        hitFlag = false;
        windMillFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool CheckHitFlag()
    {
        return hitFlag;
    }
    public void SetCheckHitFlag(bool flag)
    {
        hitFlag = flag;
    }

    public bool CheckWindMillFlag()
    {
        return windMillFlag;
    }
    public void SetCheckWindMillFlag(bool flag)
    {
        windMillFlag = flag;
    }
}
