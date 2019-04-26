using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DebugPlayer : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベース
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private DebugPlayerDataBace m_dataBace;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データベースUI
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private GameObject m_dataUIObject;
    private DebugPlayerUI m_dataUI;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー情報
    //*|***|***|***|***|***|***|***|***|***|***|***|
    void AwakePlayerUI()
    {
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
        m_dataUI = m_dataUIObject.AddComponent<DebugPlayerUI>();






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
