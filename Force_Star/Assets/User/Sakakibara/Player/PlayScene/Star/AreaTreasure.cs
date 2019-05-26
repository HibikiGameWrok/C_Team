using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// プレイヤー倉庫言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using WarehousePlayer = WarehouseData.PlayerData.WarehousePlayer;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 音楽
//*|***|***|***|***|***|***|***|***|***|***|***|
using SoundID = SEManager.SoundID;

public class AreaTreasure : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    PlayerDirectorIndex m_playerIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 持っている財宝
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    int m_haveTreasure = 1000;
    //[SerializeField]
    //float m_outTreasureParsent = 0.25f;
    [SerializeField]
    float m_outTreasure = 4.0f;
    [SerializeField]
    float m_outTreasureAttenuation = 0.05f;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 持っているSE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    public SoundID m_hitWall;
    [SerializeField]
    public SoundID m_hitHead;
    [SerializeField]
    public SoundID m_hitLeg;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // これが出来たときに
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        m_playerIndex = PlayerDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 持っている財宝
        //*|***|***|***|***|***|***|***|***|***|***|***|
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の出現量
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public int AppStarNum()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float starNumF;
        int starNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        starNumF = m_outTreasure;
        m_outTreasure -= m_outTreasureAttenuation;
        m_outTreasure = ChangeData.Among(m_outTreasure, 0, m_haveTreasure);
        starNum = Mathf.CeilToInt(starNumF);
        starNum = ChangeData.Among(starNum, 0, m_haveTreasure);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 資源の減少
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveTreasure -= starNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 財宝崩
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return starNum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // SEの番号
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public SoundID GetWallSE()
    {
       return m_hitWall;
    }
    public SoundID GetHeadSE()
    {
        return m_hitHead;
    }
    public SoundID GetFloorSE()
    {
        return m_hitLeg;
    }

}


////*|***|***|***|***|***|***|***|***|***|***|***|
//// プレイヤーの一部に当たったか？
////*|***|***|***|***|***|***|***|***|***|***|***|
//if (WarehousePlayer.BoolTagIsPlayer(other.gameObject.tag))
//{
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝崩
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    float starNumF;
//    int starNum;
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝崩1
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    starNumF = m_haveTreasure * m_outTreasureParsent;
//    starNum = Mathf.CeilToInt(starNumF);
//    starNum = ChangeData.Among(starNum, 0, m_haveTreasure);
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝崩2
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    starNumF = m_outTreasure;
//    m_outTreasure -= m_outTreasureAttenuation;
//    starNum = Mathf.CeilToInt(starNumF);
//    starNum = ChangeData.Among(starNum, 0, m_haveTreasure);
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    // 財宝あらわる
//    //*|***|***|***|***|***|***|***|***|***|***|***|
//    if (starNum > 0)
//    {
//        Vector3 point = other.gameObject.transform.position;
//        float scale = m_playerIndex.GetScale();
//        Vector3 pointLeft = other.gameObject.transform.position;
//        Vector3 pointRight = other.gameObject.transform.position;
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 位置移動
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        pointLeft.x = pointLeft.x - scale;
//        pointRight.x = pointRight.x + scale;
//        Vector2 leftAngleV = new Vector2(-1, 1);
//        Vector2 rightAngleV = new Vector2(1, 1);
//        float leftAngle = ChangeData.Vector2ToAngleDeg(leftAngleV);
//        float rightAngle = ChangeData.Vector2ToAngleDeg(rightAngleV);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 星が出る * starNum
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        m_directorIndex.ApplyStarBounce(pointLeft, leftAngle, 30, 0.3f, 0.1f, starNum);
//        m_directorIndex.ApplyStarBounce(pointRight, rightAngle, 30, 0.3f, 0.1f, starNum);
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        // 資源の減少
//        //*|***|***|***|***|***|***|***|***|***|***|***|
//        m_haveTreasure -= starNum;
//    }
//}
