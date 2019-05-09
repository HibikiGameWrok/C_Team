using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DebugPlayer : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 監督へアクセス
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベース
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerDataBace m_dataBace;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベースUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_dataUIObject;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 基本のUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerUI m_dataUI;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 回復のUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerRecoveryUI m_dataRecoveryUI;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayerUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 監督へアクセス
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベース
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace = new DebugPlayerDataBace();
        m_dataBace.ResetAll(10000);
        m_dataBace.ResetStars(0, 2000);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースUI
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUIObject = new GameObject("PLayerUI");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 基本のUI
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUI = m_dataUIObject.AddComponent<DebugPlayerUI>();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 回復のUI
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataRecoveryUI = m_dataUIObject.AddComponent<DebugPlayerRecoveryUI>();


    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void UpdatePlayerUI()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースを更新
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataBace.CatchTimers(-1);

        if (Input.GetKey(KeyCode.L))
        {
            m_dataBace.CatchStars(10);
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーはどこにいますの？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //Vector3 playerPos = m_playerCenter.gameObject.transform.position;
        //Vector3 screenPos = m_directorIndex.GetScreenPos(playerPos);

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースから取得
        //*|***|***|***|***|***|***|***|***|***|***|***|
        int haveStar = m_dataBace.GetStarsNum();
        int needStar = m_dataBace.GetNeedStarsNum();
        float time = m_dataBace.GetTimeParsent();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 計算
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float starParsent = MyCalculator.Division((float)haveStar, (float)needStar);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // データベースUIに反映
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_dataUI.SetAirGaugeNumber(time);
        m_dataUI.SetStarGaugeNumber(starParsent);
        m_dataUI.SetHaveStarNumber(haveStar);
        m_dataUI.SetNeedStarNumber(needStar);
    }
}
