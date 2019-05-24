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
        // 着地SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        FOOTSTEP_01,
        FOOTSTEP_02,
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆を取った時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        HITSTAR_01,
        HITSTAR_02,
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
    [SerializeField]
    SoundID m_soundId;
    ////敵を倒した時のSE
    //public AudioClip m_sound_Enemy;

    ////砂ブロックを破壊した時のSE
    //public AudioClip m_sound_SandBlock;

    ////星を取った時のSE
    //public AudioClip m_sound_Star;

    [SerializeField]
    bool soundFlag = false;

    AudioSource m_audiosource;

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
        m_audiosource = gameObject.GetComponent<AudioSource>();
        if (m_audiosource == null)
        {
            m_audiosource = gameObject.AddComponent<AudioSource>();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        m_soundId = SoundID.NONE;
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
        // 着地SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.FOOTSTEP_01] = Resources.Load<AudioClip>(filepass + "FootStep_SE_01");
        m_sounds[(int)SoundID.FOOTSTEP_02] = Resources.Load<AudioClip>(filepass + "FootStep_SE_02");
        //*|***|***|***|***|***|***|***|***|***|***|***|
        // ☆を取った時SE
        //*|***|***|***|***|***|***|***|***|***|***|***|
        m_sounds[(int)SoundID.HITSTAR_01] = Resources.Load<AudioClip>(filepass + "HitStar_SE_01");
        m_sounds[(int)SoundID.HITSTAR_02] = Resources.Load<AudioClip>(filepass + "HitStar_SE_02");
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

    // Update is called once per frame
    void Update()
    {
        //switch (m_soundId)
        //{
        //    case SoundID.ENEMY:
        //        m_audiosource.PlayOneShot(GetGameSE(m_soundId));
        //        break;
        //    case SoundID.SAND_BLOCK:
        //        m_audiosource.PlayOneShot(m_sound_SandBlock);
        //        break;
        //    case SoundID.STAR:
        //        m_audiosource.PlayOneShot(m_sound_Star);
        //        break;
        //}



        //if (m_soundId != SoundID.NONE)
        //    m_soundId = SoundID.NONE;

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    m_soundId = SoundID.ENEMY;

        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    m_soundId = SoundID.SAND_BLOCK;
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    m_soundId = SoundID.STAR;
        //}

        //if (soundFlag)
        //{
        //    m_audiosource.PlayOneShot(GetGameSE(m_soundId));
        //    soundFlag = false;
        //}
    }

    public void PlaySoundEffect(SoundID id)
    {
        m_audiosource.PlayOneShot(GetGameSE(id));
        soundFlag = true;
    }
}
