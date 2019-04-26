using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*|***|***|***|***|***|***|***|***|***|***|***|
// 主人公と周りの環境のデータ
//*|***|***|***|***|***|***|***|***|***|***|***|
public class DebugPlayerDataBace
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int m_haveStarsNum = 0;
    [SerializeField]
    private int m_needStarsNum = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private int m_havePartsNum = 0;
    [SerializeField]
    private uint m_havePartsId = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間であらわされる命
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_airTimer = 0;
    [SerializeField]
    private float m_airTimerMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public DebugPlayerDataBace()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarsNum = 0;
        m_needStarsNum = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_havePartsNum = 0;
        m_havePartsId = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間であらわされる命
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airTimer = 0;
        m_airTimerMax = 0;
    }

    public DebugPlayerDataBace(DebugPlayerDataBace baseData)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_haveStarsNum = baseData.m_haveStarsNum;
        m_needStarsNum = baseData.m_needStarsNum;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_havePartsNum = baseData.m_havePartsNum;
        m_havePartsId = baseData.m_havePartsId;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間であらわされる命
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airTimer = baseData.m_airTimer;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // リセットの権限
    // m_airTimerが０だと即死するので注意
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetAll()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ResetStars();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ResetParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間であらわされる命
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ResetTimes();
    }
    public void ResetAll(float timer)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 星収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ResetStars();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ収集度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ResetParts();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 時間であらわされる命
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ResetTimes(timer);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetStars()
    {
        m_haveStarsNum = 0;
        m_needStarsNum = 0;
    }
    public void ResetStars(int starsNum, int needStars)
    {
        m_haveStarsNum = starsNum;
        m_needStarsNum = needStars;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetParts()
    {
        m_havePartsNum = 0;
        m_havePartsId = 0;
    }
    public void ResetPartsNum(int num)
    {
        m_havePartsNum = num;
    }
    public void ResetPartsId(uint id)
    {
        m_havePartsId = id;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間であらわされる命
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetTimes()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 即死
        // 状況起因処理でリザルトへ送られる
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_airTimer = 0;
        m_airTimerMax = 0;
    }
    public void ResetTimes(float num)
    {
        m_airTimer = num;
        m_airTimerMax = num;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 外からの介入
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CatchStar()
    {
        m_haveStarsNum += 1;
    }
    public void CatchStars(int addCount)
    {
        m_haveStarsNum += addCount;
    }
    public void CatchStarRare(int addRare)
    {
        m_haveStarsNum += addRare;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CatchPart()
    {
        m_havePartsNum += 1;
    }
    public void CatchParts(int addCount)
    {
        m_havePartsNum += addCount;
    }
    public void CatchPartID(int number)
    {
        uint numberToID = MyCalculator.MultiplicationBinary((uint)number);
        m_havePartsId = MyCalculator.DigitOR(numberToID, m_havePartsId);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間であらわされる命
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void CatchTimer()
    {
        m_airTimer += 1;
    }
    public void CatchTimers(int addCount)
    {
        m_airTimer += addCount;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 外への介入
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public int GetStarsNum()
    {
        return m_haveStarsNum;
    }
    public int GetNeedStarsNum()
    {
        return m_needStarsNum;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ収集度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public int GetPartsNum()
    {
        return m_havePartsNum;
    }
    public bool GetPartsId(uint id)
    {
        return MyCalculator.DigitBoolean(m_havePartsId, id);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 時間であらわされる命
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetTimeParsent()
    {
        float parsent = MyCalculator.Division(m_airTimer, m_airTimerMax);
        return parsent;
    }
    public float GetTimeParsent100()
    {
        float parsent = MyCalculator.Division(m_airTimer, m_airTimerMax);
        parsent = parsent * 100.0f;
        return parsent;
    }


}
