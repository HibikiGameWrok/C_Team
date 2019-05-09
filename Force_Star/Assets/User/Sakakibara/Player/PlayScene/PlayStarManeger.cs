using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

//*|***|***|***|***|***|***|***|***|***|***|***|
// オブジェクト倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehouseObject = WarehouseData.WarehouseObject;

public class PlayStarManeger : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星オブジェクト(隠し)
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_starObjOrigin = null;
    private StarPieceMove m_starObjOriginMove = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星オブジェクト
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星のレイヤーに変更
        //*|***|***|***|***|***|***|***|***|***|***|***|
        gameObject.layer = 12;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星オブジェクト(隠し)
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starObjOrigin = new GameObject("starOrigin");
        m_starObjOriginMove = m_starObjOrigin.AddComponent<StarPieceMove>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ここにしまう
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_starObjOrigin.transform.parent = gameObject.transform;
        m_starObjOrigin.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 指定位置に星のピースの生成関数
    // 引数(出す場所,星の取得数)
    public void CreateStarPisce(Vector3 pos, int max)
    {
        GameObject newStarObj = null;
        StarPieceMove newStarObjMove = null;
        for (int i = 0; i < max; i++)
        {
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 生成
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj = Instantiate(m_starObjOrigin) as GameObject;
            newStarObj.SetActive(true);
            newStarObjMove = newStarObj.GetComponent<StarPieceMove>();
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // ここにしまう
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObj.transform.parent = transform;
            //*|***|***|***|***|***|***|***|***|***|***|***|
            // 飛んでいく角度をランダムに決める
            //*|***|***|***|***|***|***|***|***|***|***|***|
            newStarObjMove.SetVec(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
            newStarObjMove.SetPosition(pos);
            newStarObjMove.SetSpeed(0.01f, 0.01f);
        }
    }
}
