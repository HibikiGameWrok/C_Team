using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*|***|***|***|***|***|***|***|***|***|***|***|
// パーツ言い換え
//*|***|***|***|***|***|***|***|***|***|***|***|
using PartsID = PlayStaticData.PartsID;

public class PlayerDirectorIndex
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private static PlayerDirectorIndex director = null;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン生成
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static PlayerDirectorIndex GetInstance()
    {
        if (director == null)
        {
            director = new PlayerDirectorIndex();
        }
        return director;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // シングルトン消滅
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public static void Remove()
    {
        if (director != null)
        {
            director = null;
        }
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの元締め
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirector m_playerDirector;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの最後の床
    //*|***|***|***|***|***|***|***|***|***|***|***|
    FloorTreasure m_playerLastPanel;
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤー
    //*|***|***|***|***|***|***|***|***|***|***|***|

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ初期化
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlayerDirectorIndex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの元締め
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerDirector = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerLastPanel = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ終了
    //*|***|***|***|***|***|***|***|***|***|***|***|
    ~PlayerDirectorIndex()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの元締め
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerDirector = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerLastPanel = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データ終わりか？再生か？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void AllReset()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの元締め
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerDirector = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_playerLastPanel = null;
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤー
        //*|***|***|***|***|***|***|***|***|***|***|***|

    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ポインター受付
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerPlayDirector(PlayerDirector director)
    {
        m_playerDirector = director;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ダメージを与える
    // IgnoreInvincibility 無敵を無視する。
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void ExecutionDamageArm(float damage, bool IgnoreInvincibility)
    {
        m_playerDirector.DamageArm(damage, IgnoreInvincibility);
    }
    public void ExecutionDamageBody(float damage, bool IgnoreInvincibility)
    {
        m_playerDirector.DamageBody(damage, IgnoreInvincibility);
    }
    public void ExecutionDamageHead(float damage, bool IgnoreInvincibility)
    {
        m_playerDirector.DamageHead(damage, IgnoreInvincibility);
    }
    public void ExecutionDamageLeg(float damage, bool IgnoreInvincibility)
    {
        m_playerDirector.DamageLeg(damage, IgnoreInvincibility);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 星の数を足す
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void GetStar(int starNum)
    {
        m_playerDirector.GetStar(starNum);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // パーツ獲得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void GetParts(PartsID partsID)
    {
        m_playerDirector.GetParts(partsID);
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの場所を取る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerPosition()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerPositon();
        }
        return point;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーパーツ位置情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerArmRightPositon()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerPositon();
        }
        return point;
    }
    public Vector3 GetPlayerArmLeftPositon()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerPositon();
        }
        return point;
    }
    public Vector3 GetPlayerBodyPositon()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerPositon();
        }
        return point;
    }
    public Vector3 GetPlayerHeadPositon()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerPositon();
        }
        return point;
    }
    public Vector3 GetPlayerLegRightPositon()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerLegRightPositon();
        }
        return point;
    }
    public Vector3 GetPlayerLegLeftPositon()
    {
        Vector3 point = Vector3.zero;
        if (m_playerDirector)
        {
            point = m_playerDirector.GetPlayerLegLeftPositon();
        }
        return point;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージはどのくらい残っている？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetAirParsent()
    {
        float data = 1.0f;
        if (m_playerDirector)
        {
            data = m_playerDirector.GetAirParsent();
        }
        return data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持数は目標量の何割か？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetHaveStarParsent()
    {
        float data = 0;
        if (m_playerDirector)
        {
            data = m_playerDirector.GetHaveStarParsent();
        }
        return data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 所持パーツ情報取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetHaveAllPartsFlag()
    {
        bool data = false;
        if (m_playerDirector)
        {
            data = m_playerDirector.GetHaveAllPartsFlag();
        }
        return data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 大きさ取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetGroundFlag()
    {
        bool data = false;
        if (m_playerDirector)
        {
            data = m_playerDirector.GetGroundFlag();
        }
        return data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 大きさ取得
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetScale()
    {
        float data = 0.0f;
        if (m_playerDirector)
        {
            data = m_playerDirector.GetScale();
        }
        return data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // プレイヤーの最後の床
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void SetPointerLastPanel(FloorTreasure panel)
    {
        m_playerLastPanel = panel;
    }
    public bool GetPointerLastPanel(FloorTreasure panel)
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床は同じ？
        //*|***|***|***|***|***|***|***|***|***|***|***|
        if (m_playerLastPanel == panel)
        {
            return true;
        }
        return false;
    }
    public FloorTreasure GetPointerLastPanel()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        return m_playerLastPanel;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 当たったフラグ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public bool GetLastPanel_Arm()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool data = false;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetArm();
        }
        return data;
    }
    public bool GetLastPanel_Body()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool data = false;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetBody();
        }
        return data;
    }
    public bool GetLastPanel_Head()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool data = false;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetHead();
        }
        return data;
    }
    public bool GetLastPanel_Leg()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        bool data = false;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetLeg();
        }
        return data;
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 連続制限時間
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetLastPanel_TimeWall()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float data = 0.0f;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetTimeWall();
        }
        return data;
    }
    public float GetLastPanel_TimeHead()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float data = 0.0f;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetTimeHead();
        }
        return data;
    }
    public float GetLastPanel_TimeLeg()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // プレイヤーの最後の床
        //*|***|***|***|***|***|***|***|***|***|***|***|
        float data = 0.0f;
        if (m_playerLastPanel)
        {
            data = m_playerLastPanel.GetTimeLeg();
        }
        return data;
    }
}
