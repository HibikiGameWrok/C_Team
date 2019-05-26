using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSearch : MonoBehaviour
{
    [SerializeField]
    private GameObject m_roket = null;
    StartRoket m_startRokets;
    bool m_createPlayr;

    CircleCollider2D col;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        m_startRokets = m_roket.GetComponent<StartRoket>();
        col = gameObject.GetComponent<CircleCollider2D>();
        col.radius = 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            col.radius = 100;
        }


        if (m_createPlayr)
        {

            this.transform.position = m_playerIndex.GetPlayerPosition();
        }
        
    }
    public void CheckFlag()
    {
        if (m_createPlayr)
            m_createPlayr = false;
        else
            m_createPlayr = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
  
    }
}
