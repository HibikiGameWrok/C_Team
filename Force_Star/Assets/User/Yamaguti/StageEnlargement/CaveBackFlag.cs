using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBackFlag : MonoBehaviour
{
    [SerializeField]
    private GameObject CaveHitObject = null;

    bool CaveFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(CaveHitObject.GetComponent<CaveFlagScript>().GetCaveEnterFlag())
        {
            if (other.gameObject.tag == "Player")
            {
                CaveFlag = true;
            }
        }
       
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            CaveFlag = false;
        }
    }
    public bool GetCaveBackFlag()
    {
        return CaveFlag;
    }

}
