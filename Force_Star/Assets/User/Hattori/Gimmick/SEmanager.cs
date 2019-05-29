using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    //*|***|***|***|***|***|***|***|***|***|***|***|
    // ゲーム共通ディレクター
    //*|***|***|***|***|***|***|***|***|***|***|***|
    PlaySceneDirectorIndex m_directorIndex;

    //
    public static string music = "music/";
    public static string se = "SE/";

    [SerializeField]
    AudioClip[] m_sounds;

    public enum SoundID
    {
        NONE,
        ENEMY,
        SAND_BLOCK,
        STAR,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メニュー画面を閉じるSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        CLOSEMENU_01,
        CLOSEMENU_02,
        CLOSEMENU_03,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ロケットが墜落する時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        CRASHROCKET_01,
        CRASHROCKET_02,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        DAMAGE_01,
        DAMAGE_02,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 吹き飛び
        //*|***|***|***|***|***|***|***|***|***|***|***|
        HYUN_01,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃をガード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        ENEMYBLOCKING_01,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆が納品しきった時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        DELIVERYSTAR,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ取得取得SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        GETPARTS_01,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 着地SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        FOOTSTEP_01,
        FOOTSTEP_02,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆を取った時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        HITSTAR_01,
        HITSTAR_02,
        HITSTAR_03,
        HITSTAR_04,
        HITSTAR_05,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 壁に当たった時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        HITWALL,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        JUMP,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MOVE_01,
        MOVE_02,
        MOVE_03,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メニューを開くSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        OPENMENU_01,
        OPENMENU_02,
        OPENMENU_03,
        OPENMENU_04,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチスイングSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PUNCHSWING,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ回復SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        RECOVERY,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージが無くなりそうな時の警告SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        SIREN_01,
        SIREN_02,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //  爆発SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        PLAYERFIRE_SE,
        PLAYERBOMB_SE_01,
        PLAYERBOMB_SE_02,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 総数
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MAXNUM
    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // オーディオたちの集会
    //*|***|***|***|***|***|***|***|***|***|***|***|
    AudioSource[] m_audioRoom;

    public enum AudioID
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 共通
        //*|***|***|***|***|***|***|***|***|***|***|***|
        COMMON,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ストップ使用
        //*|***|***|***|***|***|***|***|***|***|***|***|
        STOP_P1,
        STOP_P2,
        STOP_P3,
        STOP_P4,
        STOP_P5,
        STOP_P6,
        STOP_P7,
        STOP_P8,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 音量変化
        //*|***|***|***|***|***|***|***|***|***|***|***|
        VOLUME_P1,
        VOLUME_P2,
        VOLUME_P3,
        VOLUME_P4,
        VOLUME_P5,
        VOLUME_P6,
        VOLUME_P7,
        VOLUME_P8,
        VOLUME_P9,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 総数
        //*|***|***|***|***|***|***|***|***|***|***|***|
        MAXNUM
    }



    void Awake()
    {
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ゲーム共通ディレクター
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_directorIndex = PlaySceneDirectorIndex.GetInstance();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 音のデータ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds = new AudioClip[(int)SoundID.MAXNUM];
        ReadData();
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーディオON!
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_audioRoom = new AudioSource[(int)AudioID.MAXNUM];
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // オーディオの設定ループ
        //*|***|***|***|***|***|***|***|***|***|***|***|
        for (int index = 0; index < m_audioRoom.Length; index++)
        {
            m_audioRoom[index] = gameObject.AddComponent<AudioSource>();
        }
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 音量設定
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_audioRoom[(int)AudioID.VOLUME_P1].volume = 0.1f;
        m_audioRoom[(int)AudioID.VOLUME_P2].volume = 0.2f;
        m_audioRoom[(int)AudioID.VOLUME_P3].volume = 0.3f;
        m_audioRoom[(int)AudioID.VOLUME_P4].volume = 0.4f;
        m_audioRoom[(int)AudioID.VOLUME_P5].volume = 0.5f;
        m_audioRoom[(int)AudioID.VOLUME_P6].volume = 0.6f;
        m_audioRoom[(int)AudioID.VOLUME_P7].volume = 0.7f;
        m_audioRoom[(int)AudioID.VOLUME_P8].volume = 0.8f;
        m_audioRoom[(int)AudioID.VOLUME_P9].volume = 0.9f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    //*|***|***|***|***|***|***|***|***|***|***|***|
    // データの入れどころ
    //*|***|***|***|***|***|***|***|***|***|***|***|
    private void ReadData()
    {
        string filepass;

        filepass = music + se;

        m_sounds[(int)SoundID.NONE] = Resources.Load<AudioClip>(filepass + "bomb");

        m_sounds[(int)SoundID.ENEMY] = Resources.Load<AudioClip>(filepass + "bomb");

        m_sounds[(int)SoundID.SAND_BLOCK] = Resources.Load<AudioClip>(filepass + "impact");

        m_sounds[(int)SoundID.STAR] = Resources.Load<AudioClip>(filepass + "magic");

        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メニュー画面を閉じるSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.CLOSEMENU_01] = Resources.Load<AudioClip>(filepass + "CloseMenu_SE_01");
        m_sounds[(int)SoundID.CLOSEMENU_02] = Resources.Load<AudioClip>(filepass + "CloseMenu_SE_02");
        m_sounds[(int)SoundID.CLOSEMENU_03] = Resources.Load<AudioClip>(filepass + "CloseMenu_SE_03");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ロケットが墜落する時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.CRASHROCKET_01] = Resources.Load<AudioClip>(filepass + "CrashRocket_SE_01");
        m_sounds[(int)SoundID.CRASHROCKET_02] = Resources.Load<AudioClip>(filepass + "CrashRocket_SE_02");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ダメージSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.DAMAGE_01] = Resources.Load<AudioClip>(filepass + "Damage_SE_01");
        m_sounds[(int)SoundID.DAMAGE_02] = Resources.Load<AudioClip>(filepass + "Damage_SE_02");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 吹き飛び
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.HYUN_01] = Resources.Load<AudioClip>(filepass + "hyun");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 攻撃をガード
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.ENEMYBLOCKING_01] = Resources.Load<AudioClip>(filepass + "EnemyBlocking");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆が納品しきった時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.DELIVERYSTAR] = Resources.Load<AudioClip>(filepass + "DeliveryStar_SE");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆が納品しきった時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.GETPARTS_01] = Resources.Load<AudioClip>(filepass + "GetParts01");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 着地SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.FOOTSTEP_01] = Resources.Load<AudioClip>(filepass + "FootStep_SE_01");
        m_sounds[(int)SoundID.FOOTSTEP_02] = Resources.Load<AudioClip>(filepass + "FootStep_SE_02");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆を取った時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.HITSTAR_01] = Resources.Load<AudioClip>(filepass + "HitStar_SE_01");
        m_sounds[(int)SoundID.HITSTAR_02] = Resources.Load<AudioClip>(filepass + "HitStar_SE_02");
        m_sounds[(int)SoundID.HITSTAR_03] = Resources.Load<AudioClip>(filepass + "HitStar_SE_03");
        m_sounds[(int)SoundID.HITSTAR_04] = Resources.Load<AudioClip>(filepass + "HitStar_SE_04");
        m_sounds[(int)SoundID.HITSTAR_05] = Resources.Load<AudioClip>(filepass + "HitStar_SE_05");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 壁に当たった時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.HITWALL] = Resources.Load<AudioClip>(filepass + "HitWall_SE");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ジャンプSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.JUMP] = Resources.Load<AudioClip>(filepass + "Jump_SE");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 移動SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.MOVE_01] = Resources.Load<AudioClip>(filepass + "Move_SE_01");
        m_sounds[(int)SoundID.MOVE_02] = Resources.Load<AudioClip>(filepass + "Move_SE_02");
        m_sounds[(int)SoundID.MOVE_03] = Resources.Load<AudioClip>(filepass + "Move_SE_03");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // メニューを開くSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.OPENMENU_01] = Resources.Load<AudioClip>(filepass + "OpenMenu_SE_01");
        m_sounds[(int)SoundID.OPENMENU_02] = Resources.Load<AudioClip>(filepass + "OpenMenu_SE_02");
        m_sounds[(int)SoundID.OPENMENU_03] = Resources.Load<AudioClip>(filepass + "OpenMenu_SE_03");
        m_sounds[(int)SoundID.OPENMENU_04] = Resources.Load<AudioClip>(filepass + "OpenMenu_SE_04");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パンチスイングSE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.PUNCHSWING] = Resources.Load<AudioClip>(filepass + "PunchSwing_SE");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // パーツ回復SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.RECOVERY] = Resources.Load<AudioClip>(filepass + "Recovery_SE");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // 酸素ゲージが無くなりそうな時の警告SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.SIREN_01] = Resources.Load<AudioClip>(filepass + "Siren_SE_01");
        m_sounds[(int)SoundID.SIREN_02] = Resources.Load<AudioClip>(filepass + "Siren_SE_02");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        //  爆発SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.PLAYERFIRE_SE] = Resources.Load<AudioClip>(filepass + "PlayerFire_SE");
        m_sounds[(int)SoundID.PLAYERBOMB_SE_01] = Resources.Load<AudioClip>(filepass + "PlayerBomb_SE_01");
        m_sounds[(int)SoundID.PLAYERBOMB_SE_02] = Resources.Load<AudioClip>(filepass + "PlayerBomb_SE_02");
    }

    private AudioClip GetGameSE(SoundID textureNum)
    {
        int type = ChangeData.AmongLess((int)textureNum, 0, (int)SoundID.MAXNUM);
        return m_sounds[type];
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  共通でSE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void PlaySoundEffect(SoundID soundId)
    {
        int commonNum = (int)AudioID.COMMON;
        m_audioRoom[commonNum].PlayOneShot(GetGameSE(soundId));
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  指定ルームでSE
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void PlaySoundEffect(SoundID soundId, AudioID audioId)
    {
        int roomNum = (int)audioId;
        m_audioRoom[roomNum].PlayOneShot(GetGameSE(soundId));
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  指定ルームを停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void StopSoundEffect(AudioID audioId)
    {
        int roomNum = (int)audioId;
        m_audioRoom[roomNum].Stop();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  指定ルームを一時停止
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void PauseSoundEffect(AudioID audioId)
    {
        int roomNum = (int)audioId;
        m_audioRoom[roomNum].Pause();
    }
    //*|***|***|***|***|***|***|***|***|***|***|***|
    //  指定ルームを再開
    //*|***|***|***|***|***|***|***|***|***|***|***|
    public void UnPauseSoundEffect(AudioID audioId)
    {
        int roomNum = (int)audioId;
        m_audioRoom[roomNum].UnPause();
    }
}
