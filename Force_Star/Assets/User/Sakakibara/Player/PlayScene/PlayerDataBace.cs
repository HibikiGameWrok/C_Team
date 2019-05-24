using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;

//*|***|***|***|***|***|***|***|***|***|***|***|
// 主人公と周りの環境のデータ
//*|***|***|***|***|***|***|***|***|***|***|***|
[Serializable]
public class PlayerDataBace
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
    // パーツの耐久度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_armDurable = 0;
    [SerializeField]
    private float m_armDurableMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_bodyDurable = 0;
    [SerializeField]
    private float m_bodyDurableMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_headDurable = 0;
    [SerializeField]
    private float m_headDurableMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_legDurable = 0;
    [SerializeField]
    private float m_legDurableMax = 0;

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの強化時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_armStrong = 0;
    [SerializeField]
    private float m_armStrongMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_bodyStrong = 0;
    [SerializeField]
    private float m_bodyStrongMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_headStrong = 0;
    [SerializeField]
    private float m_headStrongMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    [SerializeField]
    private float m_legStrong = 0;
    [SerializeField]
    private float m_legStrongMax = 0;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // コンストラクタ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public PlayerDataBace()
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの耐久度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDurable = 0;
        m_armDurableMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyDurable = 0;
        m_bodyDurableMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headDurable = 0;
        m_headDurableMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legDurable = 0;
        m_legDurableMax = 0;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの強化時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrong = 0;
        m_armStrongMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyStrong = 0;
        m_bodyStrongMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headStrong = 0;
        m_headStrongMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legStrong = 0;
        m_legStrongMax = 0;
    }

    public PlayerDataBace(PlayerDataBace baseData)
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
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの耐久度
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDurable = baseData.m_armDurable;
        m_armDurableMax = baseData.m_armDurableMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyDurable = baseData.m_bodyDurable;
        m_bodyDurableMax = baseData.m_bodyDurableMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headDurable = baseData.m_headDurable;
        m_headDurableMax = baseData.m_headDurableMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legDurable = baseData.m_legDurable;
        m_legDurableMax = baseData.m_legDurableMax;

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツの強化時間
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrong = baseData.m_armStrong;
        m_armStrongMax = baseData.m_armStrongMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyStrong = baseData.m_bodyStrong;
        m_bodyStrongMax = baseData.m_bodyStrongMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headStrong = baseData.m_headStrong;
        m_headStrongMax = baseData.m_headStrongMax;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legStrong = baseData.m_legStrong;
        m_legStrongMax = baseData.m_legStrongMax;

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
    // パーツの耐久度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetPartsDurable()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDurable = 0;
        m_armDurableMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyDurable = 0;
        m_bodyDurableMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headDurable = 0;
        m_headDurableMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legDurable = 0;
        m_legDurableMax = 0;
    }
    public void ResetPartsDurable(float num)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armDurable = num;
        m_armDurableMax = num;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyDurable = num;
        m_bodyDurableMax = num;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headDurable = num;
        m_headDurableMax = num;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legDurable = num;
        m_legDurableMax = num;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの強化時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ResetPartsStrong()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrong = 0;
        m_armStrongMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyStrong = 0;
        m_bodyStrongMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headStrong = 0;
        m_headStrongMax = 0;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legStrong = 0;
        m_legStrongMax = 0;
    }
    public void ResetPartsStrong(float num)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_armStrong = 0;
        m_armStrongMax = num;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_bodyStrong = 0;
        m_bodyStrongMax = num;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_headStrong = 0;
        m_headStrongMax = num;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_legStrong = 0;
        m_legStrongMax = num;
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
    public void CatchPartID(PartsID number)
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
    public void CatchTimers(float addCount)
    {
        m_airTimer += addCount;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの耐久度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void DamageArmDurable()
    {
        m_armDurable -= 1;
    }
    public void DamageArmDurable(float damage)
    {
        m_armDurable -= damage;
    }
    public void RecoveryArmDurable(float damage)
    {
        m_armDurable += damage;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void DamageBodyDurable()
    {
        m_bodyDurable -= 1;
    }
    public void DamageBodyDurable(float damage)
    {
        m_bodyDurable -= damage;
    }
    public void RecoveryBodyDurable(float damage)
    {
        m_bodyDurable += damage;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void DamageHeadDurable()
    {
        m_headDurable -= 1;
    }
    public void DamageHeadDurable(float damage)
    {
        m_headDurable -= damage;
    }
    public void RecoveryHeadDurable(float damage)
    {
        m_headDurable += damage;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void DamageLegDurable()
    {
        m_legDurable -= 1;
    }
    public void DamageLegDurable(float damage)
    {
        m_legDurable -= damage;
    }
    public void RecoveryLegDurable(float damage)
    {
        m_legDurable += damage;
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの強化時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 全パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TimePartsStrong(float time)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 腕パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeArmStrong(time);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 体パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeBodyStrong(time);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 頭パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeHeadStrong(time);
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 脚パーツ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        TimeLegStrong(time);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TimeArmStrong(float time)
    {
        m_armStrong -= time;
    }
    public void StartArmStrong()
    {
        m_armStrong = m_armStrongMax;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TimeBodyStrong(float time)
    {
        m_bodyStrong -= time;
    }
    public void StartBodyStrong()
    {
        m_bodyStrong = m_bodyStrongMax;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TimeHeadStrong(float time)
    {
        m_headStrong -= time;
    }
    public void StartHeadStrong()
    {
        m_headStrong = m_headStrongMax;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void TimeLegStrong(float time)
    {
        m_legStrong -= time;
    }
    public void StartLegStrong()
    {
        m_legStrong = m_legStrongMax;
    }


    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データを確認
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ChackUpdate()
    {
        m_armDurable = ChangeData.Among(m_armDurable, 0, m_armDurableMax);
        m_bodyDurable = ChangeData.Among(m_bodyDurable, 0, m_bodyDurableMax);
        m_headDurable = ChangeData.Among(m_headDurable, 0, m_headDurableMax);
        m_legDurable = ChangeData.Among(m_legDurable, 0, m_legDurableMax);

        m_armStrong = ChangeData.Among(m_armStrong, 0, m_armStrongMax);
        m_bodyStrong = ChangeData.Among(m_bodyStrong, 0, m_bodyStrongMax);
        m_headStrong = ChangeData.Among(m_headStrong, 0, m_headStrongMax);
        m_legStrong = ChangeData.Among(m_legStrong, 0, m_legStrongMax);

        m_airTimer = ChangeData.Among(m_airTimer, 0, m_airTimerMax);
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
    public bool GetHavePartsId(uint id)
    {
        return MyCalculator.DigitBoolean(m_havePartsId, id);
    }
    public uint GetHavePartsId()
    {
        return m_havePartsId;
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
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの耐久度
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetArmDurable()
    {
        return m_armDurable;
    }
    public float GetArmDurableParsent()
    {
        float parsent = MyCalculator.Division(m_armDurable, m_armDurableMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetBodyDurable()
    {
        return m_bodyDurable;
    }
    public float GetBodyDurableParsent()
    {
        float parsent = MyCalculator.Division(m_bodyDurable, m_bodyDurableMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetHeadDurable()
    {
        return m_headDurable;
    }
    public float GetHeadDurableParsent()
    {
        float parsent = MyCalculator.Division(m_headDurable, m_headDurableMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetLegDurable()
    {
        return m_legDurable;
    }
    public float GetLegDurableParsent()
    {
        float parsent = MyCalculator.Division(m_legDurable, m_legDurableMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツの強化時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 腕パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetArmStrong()
    {
        if (m_armStrong > 0)
        {
            return true;
        }
        return false;
    }
    public float GetArmStrongParsent()
    {
        float parsent = MyCalculator.Division(m_armStrong, m_armStrongMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 体パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetBodyStrong()
    {
        if (m_bodyStrong > 0)
        {
            return true;
        }
        return false;
    }
    public float GetBodyStrongParsent()
    {
        float parsent = MyCalculator.Division(m_bodyStrong, m_bodyStrongMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 頭パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHeadStrong()
    {
        if (m_headStrong > 0)
        {
            return true;
        }
        return false;
    }
    public float GetHeadStrongParsent()
    {
        float parsent = MyCalculator.Division(m_headStrong, m_headStrongMax);
        return parsent;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 脚パーツ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetLegStrong()
    {
        if(m_legStrong > 0)
        {
            return true;
        }
        return false;
    }
    public float GetLegStrongParsent()
    {
        float parsent = MyCalculator.Division(m_legStrong, m_legStrongMax);
        return parsent;
    }



}
