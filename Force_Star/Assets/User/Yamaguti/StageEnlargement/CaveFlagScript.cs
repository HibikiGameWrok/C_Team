using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveFlagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Mask = null;

    [SerializeField]
    private GameObject CaveFloor = null;

    [SerializeField]
    private GameObject CaveBackGround = null;

    bool CaveEnterFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        CaveFloor.SetActive(false);
        Mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ok");
        if (other.gameObject.tag == "Player")
        {
            CaveEnterFlag = true;
            CaveFloor.SetActive(true);
            Mask.SetActive(true);
        }
    }
    //void OnTriggerStay2D(Collision2D other)
    //{
    //    Debug.Log("ok");
    //    if (other.gameObject.tag == "Player")
    //    {
    //        CaveEnterFlag = true;
    //        CaveFloor.SetActive(true);
    //        Mask.SetActive(true);
    //    }
    //}
    void OnTriggerExit2D(Collider2D other)
    {
        if (!CaveBackGround.GetComponent<CaveBackFlag>().GetCaveBackFlag())
        {
            if (other.gameObject.tag == "Player")
            {
                CaveEnterFlag = false;
                CaveFloor.SetActive(false);
                Mask.SetActive(false);
            }
        }
    }
    public bool GetCaveEnterFlag()
    {
        return CaveEnterFlag;
    }
}
