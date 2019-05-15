using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // プレイヤーの場所を取る
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public Vector3 GetPlayerPosition()
    {
        return m_playerDirector.GetPlayerPositon();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // 酸素ゲージはどのくらい残っている？
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public float GetAirParsent()
    {
        return m_playerDirector.GetAirParsent();
    }

}
