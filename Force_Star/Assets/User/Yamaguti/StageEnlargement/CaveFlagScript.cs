using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;


public class CaveFlagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Mask = null;

    [SerializeField]
    private GameObject CaveFloor = null;

    [SerializeField]
    private GameObject CaveBackGround = null;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;

    bool CaveEnterFlag = false;
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        CaveFloor.SetActive(false);
        Mask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CaveEnterFlag)
        {
            Mask.transform.position= m_playerIndex.GetPlayerPosition();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
        {
            CaveEnterFlag = true;
            CaveFloor.SetActive(true);
            Mask.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
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
            if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
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
