using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイシーン共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_playIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;

    [SerializeField]
    public float power = 0.0f;


    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイシーン共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
    }



    void LateUpdate()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの地点入手
        //*|***|***|***|***|***|***|***|***|***|***|***|
        Vector3 playerPos = m_playerIndex.GetPlayerPosition();

        Vector2 direction = new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
     
        GetComponent<Rigidbody2D>().velocity = (direction * power);
    }

}

