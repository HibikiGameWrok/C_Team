using System.Collections;
using System.Collections.Generic;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;
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
            if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
            {
                CaveFlag = true;
            }
        }
       
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (CaveHitObject.GetComponent<CaveFlagScript>().GetCaveEnterFlag())
        {
            if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
            {
                CaveFlag = true;
            }
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            CaveFlag = false;
        }
    }
    public bool GetCaveBackFlag()
    {
        return CaveFlag;
    }

}
